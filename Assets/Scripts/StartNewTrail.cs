using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartNewTrail : MonoBehaviour {

	// Use this for initialization
//	void Start(){
//		DontDestroyOnLoad (GameObject.Find("Trails"));
//	}

	void OnMouseDown(){
		if (!gameControll.redTruck && !gameControll.greenTruck && !gameControll.blueTruck) {
			SceneManager.LoadScene ("start");
//			Node.redAl.Clear;
//			Node.blueAl.Clear;
//			Node.greenAl.Clear;
//			Node.redTruckCap.Clear;
//			Node.blueTruckCap.Clear;
//			Node.greenTruckCap.Clear;
//			Node.intersection = 0;
//			Debug.Log ("the number of redAl "+Node.redAl.Count);
//			Debug.Log ("intersections " + Node.intersection);
			//Debug.Log ("cap array " + gameControll.capArray [1, 2]);
		}
	}
}
