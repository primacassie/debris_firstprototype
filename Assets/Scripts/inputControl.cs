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
				Debug.Log("you collect "+ capVal +" the capacity of this path "+Node.passNode1
					+ Node.passNode2+" remains: " +  gameControll.capArray[Node.passNode1,Node.passNode2]);
				//if the node is the depot then reset it.
				if (Node.passNode2 == 1 ) {
					if (gameControll.blueTruck) {
						gameControll.blueTruck = false;
					}
					if (gameControll.redTruck) {
						gameControll.redTruck = false;
					}
					if (gameControll.greenTruck) {
						gameControll.greenTruck = false;
					}
					Debug.Log (" You have finish a cycle, please start another one!");

					//reset most of the things to the beginning here
					gameControll.twoNode.Clear ();
					GameObject.Find ("GameController").GetComponent<gameControll> ().resetCursor ();
					GameObject.Find ("GameController").GetComponent<gameControll> ().resetDepot ();
				}

				GameObject.Find("InputTab").SetActive(false);
			}
		}catch(FormatException){
			Debug.Log ("please input a number!");
		}
		//still might need function to catch exception such like out of capacity
	}
}
