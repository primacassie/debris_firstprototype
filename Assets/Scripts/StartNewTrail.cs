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
            int[,] arr = gameControll.capArray;
            int sum = 0;
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    sum += arr[i, j];
                    //                if (arr[i, j] != 0)
                    //                {
                    //                    node1 = i;
                    //                    node2 = j;
                    //                    cap = arr[i, j];
                    //                }
                }
            }
            if (sum == 0)
            {
                SceneManager.LoadScene("start");
            }
//			Node.redAl.Clear;
//			Node.blueAl.Clear;
//			Node.greenAl.Clear;
//			Node.redTruckCap.Clear;
//			Node.blueTruckCap.Clear;
//			Node.greenTruckCap.Clear;
//			Node.intersection = 0;
//			Debug.Log ("the number of redAl "+Node.redAl.Count);
//			Debug.Log ("intersections " + Node.intersection);
//			Debug.Log ("cap array " + gameControll.capArray [1, 2]);
		}
	}
}
