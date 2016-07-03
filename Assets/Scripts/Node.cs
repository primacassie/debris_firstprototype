using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Node : MonoBehaviour {
	public int num;

	private DisplayManager displayManager;

	public Node(int n){
		this.num = n;
	}

	public int get(){
		return num;
	}

	private static int size2lastNode;

	//create two globle number to pass two nodes number to gameContrll.cs
	public static int passNode1;
	public static int passNode2;

	//this array store the gameobject represent the capacity of each path.
	private static GameObject[] capPath;


	void Awake(){
		capPath = GameObject.FindGameObjectsWithTag ("cap");
		foreach (GameObject obj in capPath) {
			//obj.GetComponent<Text> ().text = "50";
			obj.GetComponentInChildren<Text> ().text = "50";
		}
	}
	void OnMouseDown(){
		//Debug.Log (num);
		//int temp;
		Queue<int> t = gameControll.twoNode;
		int size = t.Count;
		if (size == 0) {
			if (num != 1 && (gameControll.redTruck|| gameControll.blueTruck || gameControll.greenTruck)) {
				Debug.Log ("Please click depot firstly to start!");
				GameObject.Find ("ModalControl").GetComponent<testWindow> ().takeAction ("Please select depot as the start!");
			} else if(num == 1 && !(gameControll.redTruck|| gameControll.blueTruck || gameControll.greenTruck)) {
				Debug.Log("please select a truck!");
				GameObject.Find ("ModalControl").GetComponent<testWindow> ().takeAction ("Please select a truck!");
			} else if((gameControll.redTruck|| gameControll.blueTruck || gameControll.greenTruck) && num==1){
				gameControll.twoNode.Enqueue (num);
				Debug.Log ("let's start!");
				gameControll.saveToFile ("start from depot!");

				//try to make a fade text here
				displayManager = DisplayManager.Instance ();
				displayManager.DisplayMessage ("Start!!!");
			}
		} else if (size ==1) {
			int firstOfSize1=t.Peek ();
			if (gameControll.validPath (firstOfSize1, num)) {
				gameControll.twoNode.Enqueue (num);
				Debug.Log ("this is the node" + num);
				size2lastNode = num;
				string processtoSave = "connect depot to node " + num.ToString ();
				gameControll.saveToFile (processtoSave);
				//Debug.Log (size2lastNode);

				//here get the capacity of the path
				passNode1 = firstOfSize1;
				passNode2 = num;

				GameObject findInactive = gameControll.myGameObject;
				findInactive.SetActive(true);

//				modifyCap (inputControl.capVal, firstOfSize1, num);
//				Debug.Log("you collect "+ inputControl.capVal
//					+" the capacity of this path "+firstOfSize1+ num+" remains: " +  gameControll.capArray[firstOfSize1,num]);
			} else {
				string toSave = "this node is not connected with the node " + firstOfSize1 + " please select a valid one! ";
				Debug.Log (toSave); 
				GameObject.Find ("ModalControl").GetComponent<testWindow> ().takeAction (toSave);
			}
		} else if (size == 2) {
			//temp = gameControll.twoNode.Dequeue ();
			//Debug.Log ("remove" + temp);
			//Debug.Log(size2lastNode);

			if (gameControll.validPath (size2lastNode, num)) {
				gameControll.twoNode.Dequeue ();
				//Debug.Log ("remove " + temp);
				gameControll.twoNode.Enqueue (num);
				passNode1 = size2lastNode;
				passNode2 = num;
//				GameObject inputTab=GameObject.Find("InputTab");
//				inputTab.SetActive (true);
				string toSave="connect node " + passNode1 + " and node " + passNode2;
				gameControll.saveToFile (toSave);
				GameObject findInactive = gameControll.myGameObject;
				findInactive.SetActive(true);

//				modifyCap (inputControl.capVal, size2lastNode, num);
//				Debug.Log ("you collect "+ inputControl.capVal
//					+" the capacity of this path "+size2lastNode+ num+" remains: " +  gameControll.capArray[size2lastNode,num]);
				size2lastNode = num;
				//Debug.Log (size2lastNode);
				//here I need to add a few lines to process the depot
			} else {
				string toSave = "this node is not connected with the node " + size2lastNode + " please select a valid one! ";
				Debug.Log (toSave);
				GameObject.Find ("ModalControl").GetComponent<testWindow> ().takeAction (toSave);
			}
			//gameControll.twoNode.Enqueue (num);
			//Debug.Log ("this is the second node"+num);
			//gameControll.validPath (firstNode, num);
			//if(gameControll.validPath (firstNode, num) && num==1)

		}
	}

	//function to modify capacity of truck and path;
	public static void modifyCap(int num,int node1,int node2){
		gameControll.carCap -= num;
		gameControll.capArray [node1, node2] -= num;
		gameControll.capArray [node2, node1] -= num;
		//string toSave = " collect " + num + " units of debris.";
		//modify the text in path of the UI
		foreach (GameObject obj in capPath) {
			int num1 = obj.GetComponent<pathCap> ().node [0];
			int num2 = obj.GetComponent<pathCap> ().node [1];
			if ((num1 == node1 && num2 == node2) || (num1 == node2 && num2 == node1)) {
				obj.GetComponentInChildren<Text> ().text = gameControll.capArray [node1, node2].ToString ();
				break;
			}
		}

		if (gameControll.redTruck) {
			gameControll.redProfitTotal += num * 10 - gameControll.timeArray [node1, node2] / 2 * 7;
			gameControll.redProfitOnce += num * 10 - gameControll.timeArray [node1, node2] / 2 * 7;
			gameControll.redTimeTotal += gameControll.timeArray [node1, node2]+num*10;
			gameControll.redTimeOnce += gameControll.timeArray [node1, node2] + num * 10;
			string findName = "redTruckText" + (gameControll.redTruckNum-1).ToString();
			GameObject.Find (findName).GetComponent<Text> ().text = gameControll.carCap.ToString ();
			//Debug.Log (gameControll.redDebrisTotal);
			//Debug.Log (panelController.redText);
			panelController.redText.text = gameControll.redProfitTotal.ToString ();
			panelController.redTime.text = gameControll.redTimeTotal.ToString ();
			panelController.redTextOnce.text = gameControll.redProfitOnce.ToString ();
			panelController.redTimeOnce.text = gameControll.redTimeOnce.ToString ();
		}

		if (gameControll.blueTruck) {
			gameControll.blueProfitTotal += num * 10 - gameControll.timeArray [node1, node2] / 2 * 7;
			gameControll.blueProfitOnce += num * 10 - gameControll.timeArray [node1, node2] / 2 * 7;
			gameControll.blueTimeTotal += gameControll.timeArray [node1, node2]+num*10;
			gameControll.blueTimeOnce += gameControll.timeArray [node1, node2] + num * 10;
			string findName = "blueTruckText" + (gameControll.blueTruckNum-1).ToString ();
			GameObject.Find (findName).GetComponent<Text> ().text = gameControll.carCap.ToString ();
			panelController.blueText.text = gameControll.blueProfitTotal.ToString ();
			panelController.blueTime.text = gameControll.blueTimeTotal.ToString ();
			panelController.blueTextOnce.text = gameControll.blueProfitOnce.ToString ();
			panelController.blueTimeOnce.text = gameControll.blueTimeOnce.ToString ();
		}

		if (gameControll.greenTruck) {
			gameControll.greenProfitTotal += num * 10 - gameControll.timeArray [node1, node2] / 2 * 7;
			gameControll.greenProfitOnce += num * 10 - gameControll.timeArray [node1, node2] / 2 * 7;
			gameControll.greenTimeTotal += gameControll.timeArray [node1, node2]+num*10;
			gameControll.greenTimeOnce += gameControll.timeArray [node1, node2] + num * 10;
			string findName = "greenTruckText" + (gameControll.greenTruckNum-1).ToString ();
			GameObject.Find (findName).GetComponent<Text> ().text = gameControll.carCap.ToString ();
			panelController.greenText.text = gameControll.greenProfitTotal.ToString ();
			panelController.greenTime.text = gameControll.greenTimeTotal.ToString ();
			panelController.greenTextOnce.text = gameControll.greenProfitOnce.ToString ();
			panelController.greenTimeOnce.text = gameControll.greenTimeOnce.ToString ();
		}
	}
}
