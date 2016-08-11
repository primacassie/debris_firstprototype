using UnityEngine;
using System.Collections;

public class generateMaps : MonoBehaviour {
//	private bool[,] initialArray=new bool[21,21];
//	private float[,] timeArray = new float[21, 21];
	// Use this for initialization
	void Awake () {
		GameObject obj1 = GameObject.Find ("node1");
		GameObject obj2 = GameObject.Find ("node2");
		Vector3 p1 = (obj1.transform.position+obj2.transform.position)/2;
		float length = Mathf.Sqrt (Mathf.Pow ((obj2.transform.position.y - obj1.transform.position.y), 2) + Mathf.Pow ((obj2.transform.position.x - obj1.transform.position.x), 2));
		GameObject obj = Instantiate (Resources.Load ("Prefabs/path")) as GameObject;
		obj.transform.position = p1;
//		int nodeNum = 20;
//		for (int i = 1; i < 20; i++) {
//			GameObject obj = Instantiate (Resources.Load ("Prefabs/Node")) as GameObject;
//			obj.name = "node" + i.ToString ();
//		}
//		initialArray [1, 2] = true;
//		initialArray [1, 5] = true;
//		initialArray [1, 6] = true;
//		initialArray [1, 7] = true;
//		initialArray [1, 9] = true;
//		initialArray [1, 10] = true;
//		initialArray [2, 3] = true;
//		initialArray [2, 8] = true;
//		initialArray [2, 9] = true;
//		initialArray [3, 4] = true;
//		initialArray [3, 5] = true;
//		initialArray [4, 5] = true;
//		initialArray [4, 6] = true;
//		initialArray [6, 7] = true;
//		initialArray [7, 14] = true;
//		initialArray [7, 16] = true;
//		initialArray [9, 10] = true;
//		initialArray [9, 11] = true;
//		initialArray [10, 11] = true;
//		initialArray [10, 13] = true;
//		initialArray [11, 12] = true;
//		initialArray [13, 14] = true;
//		initialArray [13, 15] = true;
//		initialArray [14, 15] = true;
//		initialArray [15, 16] = true;
//		initialArray [16, 17] = true;
//		initialArray [16, 18] = true;
//		initialArray [17, 19] = true;
//		initialArray [17, 20] = true;
//		initialArray [18, 19] = true;
//		timeArray [1, 2] = 3.1f;
//		timeArray [1, 5] = 2.6f;
//		timeArray [1, 6] = 4;
//		timeArray [1, 7] = 3.1f;
//		timeArray [1, 9] = 3.5f;
//		timeArray [1, 10] = 2.8f;
//		timeArray [2, 3] = 3.5f;
//		timeArray [2, 8] = 3.1f;
//		timeArray [2, 9] = 1.7f;
//		timeArray [3, 4] = 4;
//		timeArray [3, 5] = 2.8f;
//		timeArray [4, 5] = 3;
//		timeArray [4, 6] = 1.9f;
//		timeArray [6, 7] = 4.4f;
//		timeArray [7, 14] = 2.7f;
//		timeArray [7, 16] = 2.6f;
//		timeArray [9, 10] = 1.9f;
//		timeArray [9, 11] = 1.6f;
//		timeArray [10, 11] = 2.2f;
//		timeArray [10, 13] = 2.8f;
//		timeArray [11, 12] = 2.8f;
//		timeArray [13, 14] = 1.9f;
//		timeArray [13, 15] = 4;
//		timeArray [14, 15] = 2.5f;
//		timeArray [15, 16] = 4.7f;
//		timeArray [16, 17] = 3;
//		timeArray [16, 18] = 4.2f;
//		timeArray [17, 19] = 3.8f;
//		timeArray [17, 20] = 2;
//		timeArray [18, 19] = 3.8f;
//		reverseIsTrue (initialArray,timeArray);
//		int n = initialArray.Length;
//		for (int i = 0; i < n - 1; i++) {
//			for (int j = 0; j < n - 1; j++) {
//				
//			}
//		}
	}

//	void reverseIsTrue(bool[,] arr,float[,] arr1){
//		int n = arr.Length;
//		for (int i = 0; i < n-1; i++) {
//			for(int j=0;j<n-1;j++){
//				if (arr [i, j] == true) {
//					arr [j, i] = true;
//					arr1 [j, i] = arr1 [i, j];
//				}
//			}
//		}
//	}
//	
//	// Update is called once per frame
//	void Update () {
//	
//	}
}
