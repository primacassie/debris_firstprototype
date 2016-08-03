﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class cancelMark : MonoBehaviour {
	void OnMouseDown(){
		indicatorFunction.hasCancelMark = false;
		StoreTruckClick.hasCancelMark = false;
		int[] arr = StoreTruckClick.arrayForChosenPath.ToArray();
		if (StoreTruckClick.red) {
			Node.redAl.Remove (StoreTruckClick.arrayForChosenPath);
		} else if (StoreTruckClick.green) {
			Node.greenAl.Remove (StoreTruckClick.arrayForChosenPath);
		} else if (StoreTruckClick.blue) {
			Node.blueAl.Remove (StoreTruckClick.arrayForChosenPath);
		}
		for (int j = 0; j < arr.Length-1; j++) {
			int num1 = arr [j];
			int num2 = arr [j + 1];
			string pathString = "pathAnim" + num1.ToString () + num2.ToString ();
			GameObject obj = GameObject.Find (pathString);
			//					obj.GetComponent<LineRenderer> ().material = Resources.Load<Material> ("Materials/greenAnim") as Material;
			//					obj.GetComponent<LineRenderer> ().SetWidth (0.25f, 0.25f);
			//string materialName=StoreTruckClick.materialQueue.Dequeue();
			obj.GetComponent<LineRenderer> ().SetWidth (0.15f, 0.15f);
			removeIndicatorAndPath (num1, num2, StoreTruckClick.red, StoreTruckClick.green, StoreTruckClick.blue);
		}
		if (StoreTruckClick.red) {
			string goName = "redTruckImage" +(gameControll.redTruckNum-1).ToString ();
			//string goText = "redTruckText" + gameControll.redTruckNum.ToString ();
			Destroy (GameObject.Find (goName));
			StoreTruckClick.red = false;
			gameControll.redTruckNum--;
		}
		if (StoreTruckClick.green) {
			string goName = "greenTruckImage" +(gameControll.greenTruckNum-1).ToString ();
			//string goText = "redTruckText" + gameControll.redTruckNum.ToString ();
			Destroy (GameObject.Find (goName));
			StoreTruckClick.green = false;
			gameControll.greenTruckNum--;
		}
		if (StoreTruckClick.blue) {
			string goName = "blueTruckImage" +(gameControll.blueTruckNum-1).ToString ();
			//string goText = "redTruckText" + gameControll.redTruckNum.ToString ();
			Destroy (GameObject.Find (goName));
			StoreTruckClick.blue = false;
			gameControll.blueTruckNum--;
		}
		Destroy (this.gameObject);
	}

	private void removeIndicatorAndPath(int num1,int num2,bool r, bool g, bool b){
		string strNode1 = "node" + num1;
		if (num1 == 1)
		{
			strNode1 = "depot";
		}
		string strNode2 = "node" + num2;
		if (num2 == 1)
		{
			strNode2 = "depot";
		}
		GameObject node1G = GameObject.Find(strNode1);
		GameObject node2G = GameObject.Find(strNode2);
		Vector3 origin = node1G.transform.position;
		Vector3 destination = node2G.transform.position;
		float slope = (origin.y - destination.y) / (origin.x - destination.x);
		float d = 0.165f;
		if (origin.x > destination.x)
		{
			origin = new Vector3(origin.x - d * Mathf.Sin(Mathf.Atan(slope)), origin.y + d * Mathf.Cos(Mathf.Atan(slope)), origin.z);
			destination = new Vector3(destination.x - d * Mathf.Sin(Mathf.Atan(slope)), destination.y + d * Mathf.Cos(Mathf.Atan(slope)), destination.z);
		}
		if (origin.x < destination.x)
		{
			origin = new Vector3(origin.x + d * Mathf.Sin(Mathf.Atan(slope)), origin.y - d * Mathf.Cos(Mathf.Atan(slope)), origin.z);
			destination = new Vector3(destination.x + d * Mathf.Sin(Mathf.Atan(slope)), destination.y - d * Mathf.Cos(Mathf.Atan(slope)), destination.z);
		}
		if (origin.y == destination.y)
		{
			if (origin.x < destination.x)
			{
				origin = new Vector3(origin.x, origin.y - d, origin.z);
				destination = new Vector3(destination.x, destination.y - d, destination.z);
			}
			else if (origin.x > destination.x)
			{
				origin = new Vector3(origin.x, origin.y + d, origin.z);
				destination = new Vector3(destination.x, destination.y + d, destination.z);
			}
		}

		if (r) {
			Node.redPathNum [num1, num2]--;
			Debug.Log ("redNum"+Node.redPathNum [num1, num2]);
			int redNum = Node.redPathNum [num1, num2];
			int greenNum = Node.greenPathNum [num1, num2];
			int blueNum = Node.bluePathNum [num1, num2];
			string pathString = "pathAnim" + num1.ToString () + num2.ToString ();
			GameObject pathObj= GameObject.Find (pathString);
			string indicatorName = "redIndicator1" + num1.ToString() + num2.ToString();
			GameObject indicatorObj = GameObject.Find (indicatorName);
			if (redNum == 0) {
				Node.redLineArray [num1, num2] = false;
				Node.redLineArray [num2, num1] = false;
				Node.redPathArray [num1, num2] = false;
				string strNode = "node" + num2;
				if (num2 != 1) {
					GameObject node = GameObject.Find(strNode);
					if ((node.GetComponent<Node> ().RGN || node.GetComponent<Node> ().RBN)&& !node.GetComponent<Node>().RGBN) {
						Node.intersection--;
					}

					node.GetComponent<Node> ().RedN = false;
					node.GetComponent<Node> ().RGN = false;
					node.GetComponent<Node> ().RBN = false;
					node.GetComponent<Node> ().RGBN = false;
				}
				if (greenNum == 0 && blueNum == 0 ) {
					//Destroy (pathObj);
					pathObj.GetComponent<LineRenderer>().enabled=false;
					Destroy (indicatorObj);
					if (num2 != 1) {
						node2G.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Node/node") as Sprite;
					}
				} else if (greenNum > 0 && blueNum == 0 ) {
					if (num2 != 1) {
						node2G.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Node/G") as Sprite;
					}
					pathObj.GetComponent<LineRenderer> ().material = Resources.Load<Material> ("Materials/greenAnim") as Material;
					Destroy (indicatorObj);
					string greenName = "greenIndicator1" + num1.ToString () + num2.ToString ();
					Vector3 place = origin + new Vector3 (1.0f / 3.0f * (destination.x - origin.x), 1.0f / 3.0f * (destination.y - origin.y), 0f);
					GameObject.Find (greenName).transform.position = place;
				} else if (greenNum == 0 && blueNum > 0 ) {
					if (num2 != 1) {
						node2G.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Node/B") as Sprite;
					}
					pathObj.GetComponent<LineRenderer> ().material = Resources.Load<Material> ("Materials/blueAnim") as Material;
					Destroy (indicatorObj);
					string blueName = "blueIndicator1" + num1.ToString () + num2.ToString ();
					Vector3 place = origin + new Vector3 (1.0f / 3.0f * (destination.x - origin.x), 1.0f / 3.0f * (destination.y - origin.y), 0f);
					GameObject.Find (blueName).transform.position = place;
				} else if (greenNum > 0 && blueNum > 0 ) {
					if (num2 != 1) {
						node2G.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Node/GB") as Sprite;
					}
					pathObj.GetComponent<LineRenderer> ().material = Resources.Load<Material> ("Materials/GradientBG") as Material;
					Destroy (indicatorObj);
					string blueName = "blueIndicator1" + num1.ToString () + num2.ToString ();
					string greenName = "greenIndicator1" + num1.ToString () + num2.ToString ();
					Vector3 place1 = origin + new Vector3 (1.0f / 6.0f * (destination.x - origin.x), 1.0f / 6.0f * (destination.y - origin.y), 0f);
					Vector3 place2 = origin + new Vector3 (4.0f / 6.0f * (destination.x - origin.x), 4.0f / 6.0f * (destination.y - origin.y), 0f);
					GameObject.Find (greenName).transform.position = place1;
					GameObject.Find (blueName).transform.position = place2;
				}
			}
		}

		if (g) {
			Node.greenPathNum [num1, num2]--;
			Debug.Log ("greenNum"+Node.greenPathNum [num1, num2]);
			int redNum = Node.redPathNum [num1, num2];
			int greenNum = Node.greenPathNum [num1, num2];
			int blueNum = Node.bluePathNum [num1, num2];
			string pathString = "pathAnim" + num1.ToString () + num2.ToString ();
			GameObject pathObj= GameObject.Find (pathString);
			string indicatorName = "greenIndicator1" + num1.ToString() + num2.ToString();
			GameObject indicatorObj = GameObject.Find (indicatorName);
			if (greenNum == 0) {
				Node.greenLineArray [num1, num2] = false;
				Node.greenLineArray [num2, num1] = false;
				Node.greenPathArray [num1, num2] = false;
				string strNode = "node" + num2;
				if (num2 != 1) {
					GameObject node = GameObject.Find(strNode);
					if ((node.GetComponent<Node> ().RGN || node.GetComponent<Node> ().GBN)&& !node.GetComponent<Node>().RGBN) {
						Node.intersection--;
					}
					node.GetComponent<Node> ().GreenN = false;
					node.GetComponent<Node> ().RGN = false;
					node.GetComponent<Node> ().GBN = false;
					node.GetComponent<Node> ().RGBN = false;
				}
				if (blueNum == 0 && redNum == 0) {
					pathObj.GetComponent<LineRenderer>().enabled=false;
					Destroy (indicatorObj);
					if (num2 != 1) {
						node2G.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Node/node") as Sprite;
					}
				} else if (redNum > 0 && blueNum == 0 ) {
					if (num2 != 1) {
						node2G.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Node/R") as Sprite;
					}
					pathObj.GetComponent<LineRenderer> ().material = Resources.Load<Material> ("Materials/redAnim") as Material;
					Destroy (indicatorObj);
					string redName = "redIndicator1" + num1.ToString () + num2.ToString ();
					Vector3 place = origin + new Vector3 (1.0f / 3.0f * (destination.x - origin.x), 1.0f / 3.0f * (destination.y - origin.y), 0f);
					GameObject.Find (redName).transform.position = place;
				} else if (redNum == 0 && blueNum > 0 ) {
					if (num2 != 1) {
						node2G.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Node/B") as Sprite;
					}
					pathObj.GetComponent<LineRenderer> ().material = Resources.Load<Material> ("Materials/blueAnim") as Material;
					Destroy (indicatorObj);
					string blueName = "blueIndicator1" + num1.ToString () + num2.ToString ();
					Vector3 place = origin + new Vector3 (1.0f / 3.0f * (destination.x - origin.x), 1.0f / 3.0f * (destination.y - origin.y), 0f);
					GameObject.Find (blueName).transform.position = place;
				} else if (redNum > 0 && blueNum > 0) {
					if (num2 != 1) {
						node2G.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Node/RB") as Sprite;
					}
					pathObj.GetComponent<LineRenderer> ().material = Resources.Load<Material> ("Materials/GradientRB") as Material;
					Destroy (indicatorObj);
					string blueName = "blueIndicator1" + num1.ToString () + num2.ToString ();
					string redName = "redIndicator1" + num1.ToString () + num2.ToString ();
					Vector3 place1 = origin + new Vector3 (1.0f / 6.0f * (destination.x - origin.x), 1.0f / 6.0f * (destination.y - origin.y), 0f);
					Vector3 place2 = origin + new Vector3 (4.0f / 6.0f * (destination.x - origin.x), 4.0f / 6.0f * (destination.y - origin.y), 0f);
					GameObject.Find (redName).transform.position = place1;
					GameObject.Find (blueName).transform.position = place2;
				}	
			}
		}

		if (b) {
			Node.bluePathNum [num1, num2]--;
			int redNum = Node.redPathNum [num1, num2];
			int greenNum = Node.greenPathNum [num1, num2];
			int blueNum = Node.bluePathNum [num1, num2];
			string pathString = "pathAnim" + num1.ToString () + num2.ToString ();
			GameObject pathObj= GameObject.Find (pathString);
			string indicatorName = "blueIndicator1" + num1.ToString() + num2.ToString();
			GameObject indicatorObj = GameObject.Find (indicatorName);
			if (blueNum == 0) {
				Node.blueLineArray [num1, num2] = false;
				Node.blueLineArray [num2, num1] = false;
				Node.bluePathArray [num1, num2] = false;
				string strNode = "node" + num2;
				if (num2 != 1) {
					GameObject node = GameObject.Find(strNode);
					if ((node.GetComponent<Node> ().RBN || node.GetComponent<Node> ().GBN)&& !node.GetComponent<Node>().RGBN) {
						Node.intersection--;
					}
					node.GetComponent<Node> ().BlueN = false;
					node.GetComponent<Node> ().RBN = false;
					node.GetComponent<Node> ().GBN = false;
					node.GetComponent<Node> ().RGBN = false;
				}
				if (greenNum == 0 && redNum == 0) {
					pathObj.GetComponent<LineRenderer>().enabled=false;
					Destroy (indicatorObj);
					if (num2 != 1) {
						node2G.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Node/node") as Sprite;
					}
				} else if (redNum > 0 && greenNum == 0 ) {
					if (num2 != 1) {
							node2G.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Node/R") as Sprite;
					}
					pathObj.GetComponent<LineRenderer> ().material = Resources.Load<Material> ("Materials/redAnim") as Material;
					Destroy (indicatorObj);
					string redName = "redIndicator1" + num1.ToString () + num2.ToString ();
					Vector3 place = origin + new Vector3 (1.0f / 3.0f * (destination.x - origin.x), 1.0f / 3.0f * (destination.y - origin.y), 0f);
					GameObject.Find (redName).transform.position = place;
				} else if (redNum == 0 && greenNum > 0 ) {
					if (num2 != 1) {
						node2G.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Node/G") as Sprite;
					}
					pathObj.GetComponent<LineRenderer> ().material = Resources.Load<Material> ("Materials/greenAnim") as Material;
					Destroy (indicatorObj);
					string greenName = "greenIndicator1" + num1.ToString () + num2.ToString ();
					Vector3 place = origin + new Vector3 (1.0f / 3.0f * (destination.x - origin.x), 1.0f / 3.0f * (destination.y - origin.y), 0f);
					GameObject.Find (greenName).transform.position = place;
				} else if (redNum > 0 && greenNum > 0 ) {
					if (num2 != 1) {
						node2G.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Node/RG") as Sprite;
					}
					pathObj.GetComponent<LineRenderer> ().material = Resources.Load<Material> ("Materials/GradientRG") as Material;
					Destroy (indicatorObj);
					string greenName = "greenIndicator1" + num1.ToString () + num2.ToString ();
					string redName = "redIndicator1" + num1.ToString () + num2.ToString ();
					Vector3 place1 = origin + new Vector3 (1.0f / 6.0f * (destination.x - origin.x), 1.0f / 6.0f * (destination.y - origin.y), 0f);
					Vector3 place2 = origin + new Vector3 (4.0f / 6.0f * (destination.x - origin.x), 4.0f / 6.0f * (destination.y - origin.y), 0f);
					GameObject.Find (redName).transform.position = place1;
					GameObject.Find (greenName).transform.position = place2;
				}	
			}
		}
	}
}