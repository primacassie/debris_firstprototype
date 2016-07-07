using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class inputControl : MonoBehaviour {

	private InputField input;
	public static int capVal;
	public static bool valCorrect;


	public void getInput(string cap){
		Debug.Log ("get the input " + cap);
		input = GameObject.Find ("InputField").GetComponent<InputField> ();
		try{
			capVal=int.Parse(cap);
			valCorrect=false;
			gameControll.inputIsRight(capVal);
			if(valCorrect){
				input.text = "";
				Node.modifyCap (capVal, Node.passNode1, Node.passNode2);
				Debug.Log("the car capacity now is " + gameControll.carCap);
				Debug.Log("you collect "+ capVal +" the capacity of this path "+Node.passNode1
					+ Node.passNode2+" remains: " +  gameControll.capArray[Node.passNode1,Node.passNode2]);
				//if the node is the depot then reset it.
				if (Node.passNode2 == 1 ) {
					if (gameControll.blueTruck) {
						gameControll.blueTruck = false;
						gameControll.blueProfitOnce=0;
						gameControll.blueTimeOnce=0;
					}
					if (gameControll.redTruck) {
						gameControll.redTruck = false;
						gameControll.redProfitOnce=0;
						gameControll.redTimeOnce=0;
					}
					if (gameControll.greenTruck) {
						gameControll.greenTruck = false;
						gameControll.greenTimeOnce=0;
						gameControll.greenProfitOnce=0;
					}
					Destroy(GameObject.FindGameObjectWithTag("truckText"));
					Debug.Log (" You have finish a cycle, please start another one!");
					gameControll.saveToFile("a cycle is finished.");
					GameObject.Find ("ModalControl").GetComponent<testWindow> ().takeAction("You have finish a cycle!");

					//add truck scripts here
				//	GameObject.Find("storeTruck").GetComponent<storeTruck>().addTruck(0);

					//reset most of the things to the beginning here
					gameControll.twoNode.Clear ();
					GameObject.Find ("GameController").GetComponent<gameControll> ().resetCursor ();
					GameObject.Find ("GameController").GetComponent<gameControll> ().resetDepot ();
					int i=0;
					panelController.blueTextOnce.text=i.ToString();
					panelController.redTextOnce.text=i.ToString();
					panelController.greenTextOnce.text=i.ToString();
					panelController.blueTimeOnce.text=i.ToString();
					panelController.redTimeOnce.text=i.ToString();
					panelController.greenTimeOnce.text=i.ToString();

				}
				//set input tab inactive
				//GameObject.Find("InputTab").SetActive(false);
			}
		}catch(FormatException){
			Debug.Log ("please input a number!");
			GameObject.Find ("ModalControl").GetComponent<testWindow> ().takeAction ("Please input a number!");
		}
		//still might need function to catch exception such like out of capacity
	}

//	public void modifyCap(){
//		
//	}

	//control the submit button
	public void submit(){
		int node1 = 0;
		int node2 = 0;
		int cap = 0;
		int[,] arr = gameControll.capArray;
		int sum = 0;
		for (int i = 0; i < arr.GetLength (0); i++) {
			for (int j = 0; j < arr.GetLength (1); j++) {
				sum += arr [i, j];
				if (arr [i, j] != 0) {
					node1 = i;
					node2 = j;
					cap = arr [i, j];
				}
			}
		}
		if (sum == 0) {
			Debug.Log ("congratulations! you finish this game!");
			GameObject.Find ("ModalControl").GetComponent<testWindow> ().takeAction ("Congratulations! You finish this round!");
		} else {
			string temp = "there are still " + cap + " debris in the path " + node1 + node2 + ", please clean it!";
			Debug.Log ("there are still " + cap + " debris in the path " + node1 + node2 + ", please clean it!");
			GameObject.Find ("ModalControl").GetComponent<testWindow> ().takeAction (temp);
		}
	}
}
