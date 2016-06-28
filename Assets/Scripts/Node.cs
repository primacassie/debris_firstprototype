using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Node : MonoBehaviour {
	public int num;
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

	void OnMouseDown(){
		//Debug.Log (num);
		//int temp;
		Queue<int> t = gameControll.twoNode;
		int size = t.Count;
		if (size == 0) {
			if (num != 1 && (gameControll.redTruck|| gameControll.blueTruck || gameControll.greenTruck)) {
				Debug.Log ("Please click depot firstly to start!");
			} else if(num == 1 && !(gameControll.redTruck|| gameControll.blueTruck || gameControll.greenTruck)) {
				Debug.Log("please select a truck!");
			} else if((gameControll.redTruck|| gameControll.blueTruck || gameControll.greenTruck) && num==1){
				gameControll.twoNode.Enqueue (num);
				Debug.Log ("let's start!");
			}
		} else if (size ==1) {
			int firstOfSize1=t.Peek ();
			if (gameControll.validPath (firstOfSize1, num)) {
				gameControll.twoNode.Enqueue (num);
				Debug.Log ("this is the node" + num);
				size2lastNode = num;
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
				Debug.Log ("this node is not connected with the node " + firstOfSize1 + " please select a valid one! "); 
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
				GameObject findInactive = gameControll.myGameObject;
				findInactive.SetActive(true);

//				modifyCap (inputControl.capVal, size2lastNode, num);
//				Debug.Log ("you collect "+ inputControl.capVal
//					+" the capacity of this path "+size2lastNode+ num+" remains: " +  gameControll.capArray[size2lastNode,num]);
				size2lastNode = num;
				//Debug.Log (size2lastNode);
				//here I need to add a few lines to process the depot
			} else {
				Debug.Log ("this node is not connected with the node " + size2lastNode + " please select a valid one! ");
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
		if (gameControll.redTruck) {
			gameControll.redDebrisTotal += num;
			gameControll.redTimeTotal += gameControll.timeArray [node1, node2];
			string findName = "redTruckText" + (gameControll.redTruckNum-1).ToString();
			GameObject.Find (findName).GetComponent<Text> ().text = gameControll.carCap.ToString ();
			panelController.redText.text = gameControll.redDebrisTotal.ToString ();
			panelController.redTime.text = gameControll.redTimeTotal.ToString ();
		}

		if (gameControll.blueTruck) {
			gameControll.blueDebrisTotal += num;
			gameControll.blueTimeTotal += gameControll.timeArray [node1, node2];
			string findName = "blueTruckText" + (gameControll.blueTruckNum-1).ToString ();
			GameObject.Find (findName).GetComponent<Text> ().text = gameControll.carCap.ToString ();
			panelController.blueText.text = gameControll.blueDebrisTotal.ToString ();
			panelController.blueTime.text = gameControll.blueTimeTotal.ToString ();
		}

		if (gameControll.greenTruck) {
			gameControll.greenDebrisTotal += num;
			gameControll.greenTimeTotal += gameControll.timeArray [node1, node2];
			string findName = "greenTruckText" + (gameControll.greenTruckNum-1).ToString ();
			GameObject.Find (findName).GetComponent<Text> ().text = gameControll.carCap.ToString ();
			panelController.greenText.text = gameControll.greenDebrisTotal.ToString ();
			panelController.greenTime.text = gameControll.greenTimeTotal.ToString ();
		}
	}
}
