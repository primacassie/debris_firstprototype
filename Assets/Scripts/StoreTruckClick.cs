using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine.UI;

public class StoreTruckClick : MonoBehaviour {

	//public static Queue<string> materialQueue = new Queue<string> ();
	public static bool hasCancelMark;
	public static List<int> arrayForChosenPath;
	public static bool red;
	public static bool blue;
	public static bool green;
	public static int theTruckNum;
	//public static Queue<GameObject> objQueue=new Queue<GameObject>();
	void OnMouseDown(){
		//materialQueue.Clear ();
		if (!(gameControll.redTruck || gameControll.greenTruck || gameControll.blueTruck)) {
			//indicatorFunction.setCancelPathButton ();
			string truckName = this.gameObject.name;
			string resultString = Regex.Match(truckName, @"\d+").Value;
			int truckNum = int.Parse (resultString);
			theTruckNum = truckNum;
			//objQueue.Enqueue (this.gameObject);
			List<int> nodePathL = new List<int> ();
			if (truckName [0] == 'r') {
				red = true;
				green = false;
				blue = false;
				int i = 0;
				foreach (List<int> l in Node.redAl) {
					if (i == truckNum) {
						nodePathL = l;
						//Debug.Log (truckNum);
						break;
					}
					i++;
				}
				int[] nodeArr1 = nodePathL.ToArray ();
				for (int j = 0; j < nodeArr1.Length-1; j++) {
					int num1 = nodeArr1 [j];
					int num2 = nodeArr1 [j + 1];
					string pathString = "pathAnim" + num1.ToString () + num2.ToString ();
					GameObject obj = GameObject.Find (pathString);
//					obj.GetComponent<LineRenderer> ().material = Resources.Load<Material> ("Materials/redAnim") as Material;
//					obj.GetComponent<LineRenderer> ().SetWidth (0.25f, 0.25f);
					//materialQueue.Enqueue(obj.GetComponent<LineRenderer>().material.name);
					obj.GetComponent<LineRenderer> ().SetWidth (0.25f, 0.25f);
				}
			}
			if (truckName [0] == 'b') {
				blue = true;
				red = false;
				green = false;
				int i = 0;
				foreach (List<int> l in Node.blueAl) {
					if (i == truckNum) {
						nodePathL = l;
						break;
					}
					i++;
				}
				int[] nodeArr1 = nodePathL.ToArray ();
				for (int j = 0; j < nodeArr1.Length-1; j++) {
					int num1 = nodeArr1 [j];
					int num2 = nodeArr1 [j + 1];
					string pathString = "pathAnim" + num1.ToString () + num2.ToString ();
					GameObject obj = GameObject.Find (pathString);
//					obj.GetComponent<LineRenderer> ().material = Resources.Load<Material> ("Materials/blueAnim") as Material;
//					obj.GetComponent<LineRenderer> ().SetWidth (0.25f, 0.25f);
					//materialQueue.Enqueue(obj.GetComponent<LineRenderer>().material.name);
					obj.GetComponent<LineRenderer> ().SetWidth (0.25f, 0.25f);
				}
			}
			if (truckName [0] == 'g') {
				green = true;
				red = false;
				blue = false;
				int i = 0;
				foreach (List<int> l in Node.greenAl) {
					if (i == truckNum) {
						nodePathL = l;
						break;
					}
					i++;
				}
				int[] nodeArr1 = nodePathL.ToArray ();
				for (int j = 0; j < nodeArr1.Length-1; j++) {
					int num1 = nodeArr1 [j];
					int num2 = nodeArr1 [j + 1];
					string pathString = "pathAnim" + num1.ToString () + num2.ToString ();
					GameObject obj = GameObject.Find (pathString);
//					obj.GetComponent<LineRenderer> ().material = Resources.Load<Material> ("Materials/greenAnim") as Material;
//					obj.GetComponent<LineRenderer> ().SetWidth (0.25f, 0.25f);
					//materialQueue.Enqueue(obj.GetComponent<LineRenderer>().material.name);
					obj.GetComponent<LineRenderer> ().SetWidth (0.25f, 0.25f);
				}
			}

			arrayForChosenPath= nodePathL;
			if (!hasCancelMark) {
				setCancelPathButton (arrayForChosenPath.ToArray());
				hasCancelMark = true;
			}

//			for (int i = 0; i < nodeArr1.Length-1; i++) {
//				int num1 = nodeArr1 [i];
//				int num2 = nodeArr1 [i + 1];
//				string pathString = "pathAnim" + num1.ToString () + num2.ToString ();
//				GameObject.Find (pathString).GetComponent<LineRenderer> ().SetWidth (0.25f, 0.25f);
//			}


		}
	}

	private static void setCancelPathButton(int[] nodeArr)
	{
		if (!hasCancelMark) {
			float x = 0.0f;
			float y = 0.0f;
			HashSet<int> set = new HashSet <int> ();
			foreach (int num in nodeArr) {
				if (set.Add (num)) {
					string numStr = "node" + num.ToString ();
					if (num == 1) {
						numStr = "depot";
					}
					x += GameObject.Find (numStr).transform.position.x;
					y += GameObject.Find (numStr).transform.position.y;
				}
			}
			//int i=0;
			int count=set.Count;
			x = x / count;
			y = y / count;
			float z = 100f;
			Vector2 v4 = getSmallest (Node.v1, Node.v2, Node.v3, x, y);
			x = v4.x;
			y = v4.y;
			GameObject cancelMark = new GameObject ();
			cancelMark.AddComponent<cancelMark> ();
			cancelMark.name = "cancelMark";
			Transform parentTransform = GameObject.Find ("gamePanel").GetComponent<Transform> ();
			cancelMark.transform.SetParent (parentTransform);
			cancelMark.transform.position = new Vector3 (x, y, z);
			cancelMark.transform.localScale = new Vector3 (0.3f, 0.3f, 1f);
			BoxCollider2D collider = cancelMark.AddComponent<BoxCollider2D> ();
			collider.enabled = true;
			collider.size = new Vector2 (100, 100);
			Image cm = cancelMark.AddComponent<Image> ();
			cm.sprite = Resources.Load<Sprite> ("Image/cancel") as Sprite;
			//cm.color = new Color (1, 0, 0);
			hasCancelMark=true;
		}
	}


	private static Vector2 getSmallest(Vector2 f1,Vector2 f2,Vector2 f3,float x, float y){
		if (Mathf.Abs(f1.x-x)+ Mathf.Abs(f1.y-y)<= Mathf.Abs(f2.x-x)+ Mathf.Abs(f2.y-y) && Mathf.Abs(f1.x-x)+ Mathf.Abs(f1.y-y) <=Mathf.Abs(f3.x-x)+ Mathf.Abs(f3.y-y)) {
			return f1;
		} else if (Mathf.Abs(f2.x-x)+ Mathf.Abs(f2.y-y) <= Mathf.Abs(f3.x-x)+ Mathf.Abs(f3.y-y)) {
			return f2;
		} else {
			return f3;
		}
	}
}
