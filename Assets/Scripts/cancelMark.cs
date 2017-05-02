using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using SimpleJSON;

public class cancelMark : MonoBehaviour {
	void OnMouseDown(){
		GetComponent<AudioSource> ().Play ();
		indicatorFunction.hasCancelMark = false;
		StoreTruckClick.hasCancelMark = false;
		int[] arr = StoreTruckClick.arrayForChosenPath.ToArray();
		float[] arr1 = StoreTruckClick.arrayForTruckCap.ToArray ();
		//Debug.Log ("test time" + StoreTruckClick.numForTime);
		if (StoreTruckClick.red) {
			//Node.redAl.Remove (StoreTruckClick.arrayForChosenPath);
			Node.redAl.RemoveAt (StoreTruckClick.theTruckNum);
			StoreTruckClick.arrayForChosenPath.Clear ();
			//Node.redTruckCap.Remove (StoreTruckClick.arrayForTruckCap);
			Node.redTruckCap.RemoveAt(StoreTruckClick.theTruckNum);
			StoreTruckClick.arrayForTruckCap.Clear ();
			Node.redProfitAl.Remove (StoreTruckClick.numForProfit);
			Node.redTimeAl.Remove (StoreTruckClick.numForTime);
			gameControll.redProfitTotal -= StoreTruckClick.numForProfit;
			decimal.Round ((decimal)gameControll.redProfitTotal);
            if (Mathf.Abs(gameControll.redProfitTotal) < 0.01f)
                gameControll.redProfitTotal = 0f;
            //Debug.Log ("redtime" + gameControll.redTimeTotal);
            gameControll.redTimeTotal -= StoreTruckClick.numForTime;

			decimal.Round ((decimal)gameControll.redTimeTotal);
			if (gameControll.redTimeTotal < 0.01f)
				gameControll.redTimeTotal = 0f;
			panelController.redText.text = gameControll.redProfitTotal.ToString();
			panelController.redTime.text = gameControll.redTimeTotal.ToString();
			JSONClass details = new JSONClass ();
			string s = "remove red truck " + StoreTruckClick.theTruckNum;
			details ["ClickCancelMark"] = s;
			TheLogger.instance.TakeAction (7, details);
		} else if (StoreTruckClick.green) {
			Node.greenAl.RemoveAt (StoreTruckClick.theTruckNum);
			StoreTruckClick.arrayForChosenPath.Clear ();
			Node.greenTruckCap.RemoveAt (StoreTruckClick.theTruckNum);
			StoreTruckClick.arrayForTruckCap.Clear ();
			Node.greenProfitAl.Remove (StoreTruckClick.numForProfit);
			Node.greenTimeAl.Remove (StoreTruckClick.numForTime);
			gameControll.greenProfitTotal -= StoreTruckClick.numForProfit;
			decimal.Round ((decimal)gameControll.greenProfitTotal);
            if (Mathf.Abs(gameControll.greenProfitTotal )< 0.01f)
                gameControll.greenProfitTotal = 0;
            //Debug.Log ("greentime" + gameControll.greenTimeTotal);
            gameControll.greenTimeTotal -= StoreTruckClick.numForTime;
			decimal.Round ((decimal)gameControll.greenTimeTotal);
			if (gameControll.greenTimeTotal < 0.01f)
				gameControll.greenTimeTotal = 0f;
			//Debug.Log ("greentime" + gameControll.greenTimeTotal);
			panelController.greenText.text = gameControll.greenProfitTotal.ToString();
			panelController.greenTime.text = gameControll.greenTimeTotal.ToString();
			JSONClass details = new JSONClass ();
			string s = "remove green truck " + StoreTruckClick.theTruckNum;
			details ["ClickCancelMark"] = s;
			TheLogger.instance.TakeAction (7, details);
		} else if (StoreTruckClick.blue) {
			Node.blueAl.RemoveAt (StoreTruckClick.theTruckNum);
			StoreTruckClick.arrayForChosenPath.Clear ();
			Node.blueTruckCap.RemoveAt (StoreTruckClick.theTruckNum);
			StoreTruckClick.arrayForTruckCap.Clear ();
			Node.blueProfitAl.Remove (StoreTruckClick.numForProfit);
			Node.blueTimeAl.Remove (StoreTruckClick.numForTime);
			gameControll.blueProfitTotal -= StoreTruckClick.numForProfit;
			decimal.Round ((decimal)gameControll.blueProfitTotal);
            if (Mathf.Abs(gameControll.blueProfitTotal) < 0.01f)
                gameControll.blueProfitTotal = 0f;
            //Debug.Log ("bluetime" + gameControll.blueTimeTotal);
            gameControll.blueTimeTotal -= StoreTruckClick.numForTime;
			decimal.Round ((decimal)gameControll.blueTimeTotal);
			if (gameControll.blueTimeTotal < 0.01f)
				gameControll.blueTimeTotal = 0;
			//Debug.Log ("bluetime" + gameControll.blueTimeTotal);
			panelController.blueText.text = gameControll.blueProfitTotal.ToString();
			panelController.blueTime.text = gameControll.blueTimeTotal.ToString();
			JSONClass details = new JSONClass ();
			string s = "remove blue truck " + StoreTruckClick.theTruckNum;
			details ["ClickCancelMark"] = s;
			TheLogger.instance.TakeAction (7, details);
		}

		for (int j = 0; j < arr.Length-1; j++) {
			int num1 = arr [j];
			int num2 = arr [j + 1];
			gameControll.capArray [num1, num2] += (int)arr1 [j];
			gameControll.capArray [num2, num1] += (int)arr1 [j];
			string capTruck1 = "capPath" + num1.ToString () + num2.ToString ();
			string capTruck2 = "capPath" + num2.ToString () + num1.ToString ();
			if (GameObject.Find (capTruck1) != null) {
				GameObject.Find (capTruck1).GetComponentInChildren<Text> ().text = gameControll.capArray [num1, num2].ToString ();
			}else if (GameObject.Find (capTruck2) != null) {
				GameObject.Find (capTruck2).GetComponentInChildren<Text> ().text = gameControll.capArray [num1, num2].ToString ();
			}
			string pathString = "pathAnim" + num1.ToString () + num2.ToString ();
			GameObject obj = GameObject.Find (pathString);
			//					obj.GetComponent<LineRenderer> ().material = Resources.Load<Material> ("Materials/greenAnim") as Material;
			//					obj.GetComponent<LineRenderer> ().SetWidth (0.25f, 0.25f);
			//string materialName=StoreTruckClick.materialQueue.Dequeue();
			obj.GetComponent<LineRenderer> ().startWidth=.15f;
            obj.GetComponent<LineRenderer>().endWidth = .15f;
            string truckText1 = "truckCap" + num1.ToString () + num2.ToString ();
			string truckText2 = "truckCap" + num2.ToString() + num1.ToString ();
			GameObject truckCap = new GameObject ();
			if (GameObject.Find (truckText1) != null) {
				truckCap = GameObject.Find (truckText1);
			} else if (GameObject.Find (truckText2) != null) {
				truckCap = GameObject.Find (truckText2);
			}
			Text t = truckCap.GetComponent<Text> ();
			t.enabled = false;
			removeIndicatorAndPath (num1, num2, StoreTruckClick.red, StoreTruckClick.green, StoreTruckClick.blue);
		}
		removeNodeColor (StoreTruckClick.red, StoreTruckClick.green, StoreTruckClick.blue);
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

//		int num1Length_1=Node.redLineArray.GetLength(0);
//		int num1Length_2=Node.redLineArray.GetLength(1);

		if (r) {
			Node.redPathNum [num1, num2]--;
			//Debug.Log ("redNum"+Node.redPathNum [num1, num2]);
			int redNum = Node.redPathNum [num1, num2];
			int greenNum = Node.greenPathNum [num1, num2];
			int blueNum = Node.bluePathNum [num1, num2];
//            int redNum1 = Node.redPathNum[num2, num1];
			string pathString = "pathAnim" + num1.ToString () + num2.ToString ();
			GameObject pathObj= GameObject.Find (pathString);
			string indicatorName = "redIndicator1" + num1.ToString() + num2.ToString();
			GameObject indicatorObj = GameObject.Find (indicatorName);

//			int numforColorR = 0;
//			int numforColorB = 0;
//			int numforColorG = 0;

			//the thing here is that the node color is not related with path num, I was wrong;
//			for (int i = 1; i < num1Length_1; i++) {
//				numforColorR += Node.redPathNum [i, num2];
//				Debug.Log ("i " + i + " num " +num2+" "+ Node.redPathNum [i, num2]);
//			}
//			for (int i = 1; i < num1Length_2; i++) {
//				numforColorR += Node.redPathNum [num2, i];
//				Debug.Log ("i " + i + " num " +num2+" "+ Node.redPathNum [num2, i]);
//			}

//			for (int i = 1; i < num1Length_1; i++) {
//				numforColorB += Node.bluePathNum [i, num2];
//			}
//			for (int i = 1; i < num1Length_2; i++) {
//				numforColorB += Node.bluePathNum [num2, i];
//			}
//
//			for (int i = 1; i < num1Length_1; i++) {
//				numforColorG += Node.greenPathNum [i, num2];
//			}
//			for (int i = 1; i < num1Length_2; i++) {
//				numforColorG += Node.greenPathNum [num2, i];
//			}

			//Debug.Log (numforColorR + "numforcolor"+ " " +num2);


			if (redNum == 0) {
				Destroy(indicatorObj);
				Node.redLineArray [num1, num2] = false;
				Node.redLineArray [num2, num1] = false;
				Node.redPathArray [num1, num2] = false;
//				string strNode = "node" + num2;
//				if (num2 != 1 && redNum1==0) {
//					GameObject node = GameObject.Find(strNode);
//					if ((node.GetComponent<Node> ().RGN || node.GetComponent<Node> ().RBN)&& !node.GetComponent<Node>().RGBN) {
//						//there is something wrong here.
//						Debug.Log("red");
//						gameControll.intersection--;
//                        if (gameControll.intersection < 0)
//                            gameControll.intersection = 0;
//                        GameObject.Find("intersection").GetComponent<Text>().text = gameControll.intersection.ToString();
//                    }
//
//					node.GetComponent<Node> ().RedN = false;
//					node.GetComponent<Node> ().RGN = false;
//					node.GetComponent<Node> ().RBN = false;
//					node.GetComponent<Node> ().RGBN = false;
//				}
				if (greenNum == 0 && blueNum == 0 ) {
					//Destroy (pathObj);
					pathObj.GetComponent<LineRenderer>().enabled=false;
					//Destroy (indicatorObj);
//					if (num2 != 1) {
//						node2G.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Node/node") as Sprite;
//					}
				} else if (greenNum > 0 && blueNum == 0 ) {
//					if (num2 != 1) {
//						node2G.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Node/G") as Sprite;
//					}
					pathObj.GetComponent<LineRenderer> ().material = Resources.Load<Material> ("Materials/greenAnim") as Material;
					//Destroy (indicatorObj);
					string greenName = "greenIndicator1" + num1.ToString () + num2.ToString ();
					Vector3 place = origin + new Vector3 (1.0f / 3.0f * (destination.x - origin.x), 1.0f / 3.0f * (destination.y - origin.y), 0f);
					GameObject.Find (greenName).transform.position = place;
				} else if (greenNum == 0 && blueNum > 0 ) {
//					if (num2 != 1) {
//						node2G.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Node/B") as Sprite;
//					}
					pathObj.GetComponent<LineRenderer> ().material = Resources.Load<Material> ("Materials/blueAnim") as Material;
					//Destroy (indicatorObj);
					string blueName = "blueIndicator1" + num1.ToString () + num2.ToString ();
					Vector3 place = origin + new Vector3 (1.0f / 3.0f * (destination.x - origin.x), 1.0f / 3.0f * (destination.y - origin.y), 0f);
					GameObject.Find (blueName).transform.position = place;
				} else if (greenNum > 0 && blueNum > 0 ) {
//					if (num2 != 1) {
//						node2G.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Node/GB") as Sprite;
//					}
					pathObj.GetComponent<LineRenderer> ().material = Resources.Load<Material> ("Materials/GradientBG") as Material;
					// (indicatorObj);
					string blueName = "blueIndicator1" + num1.ToString () + num2.ToString ();
					string greenName = "greenIndicator1" + num1.ToString () + num2.ToString ();
					Vector3 place1 = origin + new Vector3 (1.0f / 6.0f * (destination.x - origin.x), 1.0f / 6.0f * (destination.y - origin.y), 0f);
					Vector3 place2 = origin + new Vector3 (4.0f / 6.0f * (destination.x - origin.x), 4.0f / 6.0f * (destination.y - origin.y), 0f);
					GameObject.Find (greenName).transform.position = place1;
					GameObject.Find (blueName).transform.position = place2;
				}
			}


//			if (numforColorR == 0) {
//				string strNode = "node" + num2;
//				if (num2 != 1) {
//					GameObject node = GameObject.Find(strNode);
//					if ((node.GetComponent<Node> ().RGN || node.GetComponent<Node> ().RBN)&& !node.GetComponent<Node>().RGBN) {
//						//there is something wrong here.
//						Debug.Log("red");
//						gameControll.intersection--;
//						if (gameControll.intersection < 0)
//							gameControll.intersection = 0;
//						GameObject.Find("intersection").GetComponent<Text>().text = gameControll.intersection.ToString();
//					}
//
//					node.GetComponent<Node> ().RedN = false;
//					node.GetComponent<Node> ().RGN = false;
//					node.GetComponent<Node> ().RBN = false;
//					node.GetComponent<Node> ().RGBN = false;
//				}
//				if (numforColorG == 0 && numforColorB == 0 && num2 != 1 ) {
//					node2G.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Node/node") as Sprite;
//				} else if (greenNum > 0 && blueNum == 0 ) {
//					if (num2 != 1) {
//						node2G.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Node/G") as Sprite;
//					}
//				} else if (greenNum == 0 && blueNum > 0 ) {
//					if (num2 != 1) {
//						node2G.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Node/B") as Sprite;
//					}
//				} else if (greenNum > 0 && blueNum > 0 ) {
//					if (num2 != 1) {
//						node2G.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Node/GB") as Sprite;
//					}
//				}
//			}
		}

		if (g) {
			Node.greenPathNum [num1, num2]--;
			//Debug.Log ("greenNum"+Node.greenPathNum [num1, num2]);
			int redNum = Node.redPathNum [num1, num2];
			int greenNum = Node.greenPathNum [num1, num2];
			int blueNum = Node.bluePathNum [num1, num2];
//            int greenNum1 = Node.greenPathNum[num2, num1];
			string pathString = "pathAnim" + num1.ToString () + num2.ToString ();
			GameObject pathObj= GameObject.Find (pathString);
			string indicatorName = "greenIndicator1" + num1.ToString() + num2.ToString();
			GameObject indicatorObj = GameObject.Find (indicatorName);

			//the thing here is that the node color is not related with path num, I was wrong;
//			int numforColorR = 0;
//			int numforColorB = 0;
//			int numforColorG = 0;
//
//			for (int i = 1; i < num1Length_1; i++) {
//				numforColorR += Node.redPathNum [i, num2];
//			}
//			for (int i = 1; i < num1Length_2; i++) {
//				numforColorR += Node.redPathNum [num2, i];
//			}
//
//			for (int i = 1; i < num1Length_1; i++) {
//				numforColorB += Node.bluePathNum [i, num2];
//			}
//			for (int i = 1; i < num1Length_2; i++) {
//				numforColorB += Node.bluePathNum [num2, i];
//			}
//
//			for (int i = 1; i < num1Length_1; i++) {
//				numforColorG += Node.greenPathNum [i, num2];
//			}
//			for (int i = 1; i < num1Length_2; i++) {
//				numforColorG += Node.greenPathNum [num2, i];
//			}


			if (greenNum == 0) {
				Destroy(indicatorObj);
				Node.greenLineArray [num1, num2] = false;
				Node.greenLineArray [num2, num1] = false;
				Node.greenPathArray [num1, num2] = false;
//				string strNode = "node" + num2;
//				if (num2 != 1 && greenNum1==0) {
//					GameObject node = GameObject.Find(strNode);
//					if ((node.GetComponent<Node> ().RGN || node.GetComponent<Node> ().GBN)&& !node.GetComponent<Node>().RGBN) {
//						gameControll.intersection--;
//                        if (gameControll.intersection < 0)
//                            gameControll.intersection = 0;
//                        GameObject.Find("intersection").GetComponent<Text>().text = gameControll.intersection.ToString();
//                    }
//					node.GetComponent<Node> ().GreenN = false;
//					node.GetComponent<Node> ().RGN = false;
//					node.GetComponent<Node> ().GBN = false;
//					node.GetComponent<Node> ().RGBN = false;
//				}
				if (blueNum == 0 && redNum == 0) {
					pathObj.GetComponent<LineRenderer>().enabled=false;
					//Destroy (indicatorObj);
//					if (num2 != 1) {
//						node2G.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Node/node") as Sprite;
//					}
				} else if (redNum > 0 && blueNum == 0 ) {
//					if (num2 != 1) {
//						node2G.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Node/R") as Sprite;
//					}
					pathObj.GetComponent<LineRenderer> ().material = Resources.Load<Material> ("Materials/redAnim") as Material;
					//Destroy (indicatorObj);
					string redName = "redIndicator1" + num1.ToString () + num2.ToString ();
					Vector3 place = origin + new Vector3 (1.0f / 3.0f * (destination.x - origin.x), 1.0f / 3.0f * (destination.y - origin.y), 0f);
					GameObject.Find (redName).transform.position = place;
				} else if (redNum == 0 && blueNum > 0 ) {
//					if (num2 != 1) {
//						node2G.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Node/B") as Sprite;
//					}
					pathObj.GetComponent<LineRenderer> ().material = Resources.Load<Material> ("Materials/blueAnim") as Material;
					//Destroy (indicatorObj);
					string blueName = "blueIndicator1" + num1.ToString () + num2.ToString ();
					Vector3 place = origin + new Vector3 (1.0f / 3.0f * (destination.x - origin.x), 1.0f / 3.0f * (destination.y - origin.y), 0f);
					GameObject.Find (blueName).transform.position = place;
				} else if (redNum > 0 && blueNum > 0) {
//					if (num2 != 1) {
//						node2G.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Node/RB") as Sprite;
//					}
					pathObj.GetComponent<LineRenderer> ().material = Resources.Load<Material> ("Materials/GradientRB") as Material;
					//Destroy (indicatorObj);
					string blueName = "blueIndicator1" + num1.ToString () + num2.ToString ();
					string redName = "redIndicator1" + num1.ToString () + num2.ToString ();
					Vector3 place1 = origin + new Vector3 (1.0f / 6.0f * (destination.x - origin.x), 1.0f / 6.0f * (destination.y - origin.y), 0f);
					Vector3 place2 = origin + new Vector3 (4.0f / 6.0f * (destination.x - origin.x), 4.0f / 6.0f * (destination.y - origin.y), 0f);
					GameObject.Find (redName).transform.position = place1;
					GameObject.Find (blueName).transform.position = place2;
				}	
			}

//			if (numforColorG == 0) {
//				
//				string strNode = "node" + num2;
//				if (num2 != 1) {
//					GameObject node = GameObject.Find(strNode);
//					if ((node.GetComponent<Node> ().RGN || node.GetComponent<Node> ().GBN)&& !node.GetComponent<Node>().RGBN) {
//						gameControll.intersection--;
//						if (gameControll.intersection < 0)
//							gameControll.intersection = 0;
//						GameObject.Find("intersection").GetComponent<Text>().text = gameControll.intersection.ToString();
//					}
//					node.GetComponent<Node> ().GreenN = false;
//					node.GetComponent<Node> ().RGN = false;
//					node.GetComponent<Node> ().GBN = false;
//					node.GetComponent<Node> ().RGBN = false;
//				}
//				if (numforColorB == 0 && numforColorR == 0) {
//					//Destroy (indicatorObj);
//					if (num2 != 1) {
//						node2G.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Node/node") as Sprite;
//					}
//				} else if (numforColorR > 0 && numforColorB == 0 ) {
//					if (num2 != 1) {
//						node2G.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Node/R") as Sprite;
//					}
//				} else if (numforColorR == 0 && numforColorB > 0 ) {
//					if (num2 != 1) {
//						node2G.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Node/B") as Sprite;
//					}
//				} else if (numforColorR > 0 && numforColorB > 0) {
//					if (num2 != 1) {
//						node2G.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Node/RB") as Sprite;
//					}
//				}	
//			}
		}

		if (b) {
			Node.bluePathNum [num1, num2]--;
            //Debug.Log("this blue array value" +num1+num2+" "+ Node.bluePathNum[num1, num2]);
//			Debug.Log (Node.bluePathNum [1, 2] + " blue 1,2"+ num1 + num2);
//			Debug.Log (Node.bluePathNum [2, 5] + " blue 2,5"+num1 + num2);
//			Debug.Log (Node.bluePathNum [5, 2] + " blue 5,2"+num1 + num2);
//			Debug.Log (Node.bluePathNum [2, 1] + " blue 2,1"+num1 + num2);


            int redNum = Node.redPathNum [num1, num2];
			int greenNum = Node.greenPathNum [num1, num2];
			int blueNum = Node.bluePathNum [num1, num2];
//            int blueNum1 = Node.bluePathNum[num2, num1];
			string pathString = "pathAnim" + num1.ToString () + num2.ToString ();
			GameObject pathObj= GameObject.Find (pathString);
			string indicatorName = "blueIndicator1" + num1.ToString() + num2.ToString();
			GameObject indicatorObj = GameObject.Find (indicatorName);

//			int numforColorR = 0;
//			int numforColorB = 0;
//			int numforColorG = 0;
//
//			for (int i = 1; i < num1Length_1; i++) {
//				numforColorR += Node.redPathNum [i, num2];
//			}
//			for (int i = 1; i < num1Length_2; i++) {
//				numforColorR += Node.redPathNum [num2, i];
//			}
//
//			for (int i = 1; i < num1Length_1; i++) {
//				numforColorB += Node.bluePathNum [i, num2];
//			}
//			for (int i = 1; i < num1Length_2; i++) {
//				numforColorB += Node.bluePathNum [num2, i];
//			}
//
//			for (int i = 1; i < num1Length_1; i++) {
//				numforColorG += Node.greenPathNum [i, num2];
//			}
//			for (int i = 1; i < num1Length_2; i++) {
//				numforColorG += Node.greenPathNum [num2, i];
//			}
//
//			Debug.Log (numforColorB + "numforcolorb");

			if (blueNum == 0) {
				//Debug.Log (num1 + " " + num2+ " " + blueNum);
				Destroy(indicatorObj);
//				if (GameObject.Find (indicatorName) != null) {
//					Debug.Log (num1 + " " + num2);
//					Debug.Log ("!null");
//				}

				Node.blueLineArray [num1, num2] = false;
				Node.blueLineArray [num2, num1] = false;
				Node.bluePathArray [num1, num2] = false;
//				string strNode = "node" + num2;
//				if (num2 != 1 && blueNum1==0) {
//					GameObject node = GameObject.Find(strNode);
//					if ((node.GetComponent<Node> ().RBN || node.GetComponent<Node> ().GBN) && !node.GetComponent<Node>().RGBN) {
//						Debug.Log("blue");
//                        gameControll.intersection--;
//                        if (gameControll.intersection < 0)
//                            gameControll.intersection = 0;
//                        GameObject.Find("intersection").GetComponent<Text>().text = gameControll.intersection.ToString();
//					}
//					node.GetComponent<Node> ().BlueN = false;
//					node.GetComponent<Node> ().RBN = false;
//					node.GetComponent<Node> ().GBN = false;
//					node.GetComponent<Node> ().RGBN = false;
//				}
                if (greenNum == 0 && redNum == 0)
                {
                    pathObj.GetComponent<LineRenderer>().enabled = false;
                    //Destroy(indicatorObj);
//                    if (num2 != 1)
//                    {
//                        node2G.GetComponent<Image>().sprite = Resources.Load<Sprite>("Node/node") as Sprite;
//                    }
                }
                else if (redNum > 0 && greenNum == 0)
                {
//                    if (num2 != 1)
//                    {
//                        node2G.GetComponent<Image>().sprite = Resources.Load<Sprite>("Node/R") as Sprite;
//                    }
                    pathObj.GetComponent<LineRenderer>().material = Resources.Load<Material>("Materials/redAnim") as Material;
                    //Destroy(indicatorObj);
                    string redName = "redIndicator1" + num1.ToString() + num2.ToString();
                    Vector3 place = origin + new Vector3(1.0f / 3.0f * (destination.x - origin.x), 1.0f / 3.0f * (destination.y - origin.y), 0f);
                    GameObject.Find(redName).transform.position = place;
                }
                else if (redNum == 0 && greenNum > 0)
                {
//                    if (num2 != 1)
//                    {
//                        node2G.GetComponent<Image>().sprite = Resources.Load<Sprite>("Node/G") as Sprite;
//                    }
                    pathObj.GetComponent<LineRenderer>().material = Resources.Load<Material>("Materials/greenAnim") as Material;
                    //Destroy(indicatorObj);
                    string greenName = "greenIndicator1" + num1.ToString() + num2.ToString();
                    Vector3 place = origin + new Vector3(1.0f / 3.0f * (destination.x - origin.x), 1.0f / 3.0f * (destination.y - origin.y), 0f);
                    GameObject.Find(greenName).transform.position = place;
                }
                else if (redNum > 0 && greenNum > 0)
                {
//                    if (num2 != 1)
//                    {
//                        node2G.GetComponent<Image>().sprite = Resources.Load<Sprite>("Node/RG") as Sprite;
//                    }
                    pathObj.GetComponent<LineRenderer>().material = Resources.Load<Material>("Materials/GradientRG") as Material;
                    //Destroy(indicatorObj);
                    string greenName = "greenIndicator1" + num1.ToString() + num2.ToString();
                    string redName = "redIndicator1" + num1.ToString() + num2.ToString();
                    Vector3 place1 = origin + new Vector3(1.0f / 6.0f * (destination.x - origin.x), 1.0f / 6.0f * (destination.y - origin.y), 0f);
                    Vector3 place2 = origin + new Vector3(4.0f / 6.0f * (destination.x - origin.x), 4.0f / 6.0f * (destination.y - origin.y), 0f);
                    GameObject.Find(redName).transform.position = place1;
                    GameObject.Find(greenName).transform.position = place2;
                }
			}

//			if (numforColorB == 0) {
//				//Debug.Log (num1 + " " + num2+ " " + blueNum);
//				if (num2 != 1 ) {
//					string strNode = "node" + num2;
//					GameObject node = GameObject.Find(strNode);
//					if ((node.GetComponent<Node> ().RBN || node.GetComponent<Node> ().GBN) && !node.GetComponent<Node>().RGBN) {
//						Debug.Log("blue");
//						gameControll.intersection--;
//						if (gameControll.intersection < 0)
//							gameControll.intersection = 0;
//						GameObject.Find("intersection").GetComponent<Text>().text = gameControll.intersection.ToString();
//					}
//					node.GetComponent<Node> ().BlueN = false;
//					node.GetComponent<Node> ().RBN = false;
//					node.GetComponent<Node> ().GBN = false;
//					node.GetComponent<Node> ().RGBN = false;
//				}
//				if (numforColorG == 0 && numforColorR == 0)
//				{
//					if (num2 != 1)
//					{
//						node2G.GetComponent<Image>().sprite = Resources.Load<Sprite>("Node/node") as Sprite;
//					}
//				}
//				else if (numforColorR > 0 && numforColorG == 0)
//				{
//					if (num2 != 1)
//					{
//						node2G.GetComponent<Image>().sprite = Resources.Load<Sprite>("Node/R") as Sprite;
//					}
//				}
//				else if (numforColorR == 0 && numforColorG > 0)
//				{
//					if (num2 != 1)
//					{
//						node2G.GetComponent<Image>().sprite = Resources.Load<Sprite>("Node/G") as Sprite;
//					}
//				}
//				else if (numforColorR > 0 && numforColorG > 0)
//				{
//					if (num2 != 1)
//					{
//						node2G.GetComponent<Image>().sprite = Resources.Load<Sprite>("Node/RG") as Sprite;
//					}
//				}
//			}
		}



        //if (gameControll.intersection == 1)
        //{
        //    gameControll.intersection--;
        //    if (gameControll.intersection < 0)
        //        gameControll.intersection = 0;
        //    GameObject.Find("intersection").GetComponent<Text>().text = "0";
        //}
			
	}



	private void removeNodeColor(bool r,bool g,bool b){
		for (int i = 1; i < 6; i++) {
			int sumR = 0;
			int sumG = 0;
			int sumB = 0;
			for (int j = 1; j < 6; j++) {
				sumR += Node.redPathNum [i, j];
				sumR += Node.redPathNum [j, i];
				sumG += Node.greenPathNum [i, j];
				sumG += Node.greenPathNum [j, i];
				sumB += Node.bluePathNum [i, j];
				sumB += Node.bluePathNum [j, i];
			}
			if (r) {
				if (sumR == 0) {
					string strNode = "node" + i;
					GameObject node = GameObject.Find(strNode);
					if (i != 1) {
						if ((node.GetComponent<Node> ().RGN || node.GetComponent<Node> ().RBN)&& !node.GetComponent<Node>().RGBN) {
                            gameControll.intersection--;
							if (gameControll.intersection < 0)
                                gameControll.intersection = 0;
							GameObject.Find("intersection").GetComponent<Text>().text = gameControll.intersection.ToString();
						}

						node.GetComponent<Node> ().RedN = false;
						node.GetComponent<Node> ().RGN = false;
						node.GetComponent<Node> ().RBN = false;
						node.GetComponent<Node> ().RGBN = false;
					}
					if (sumG == 0 && sumB == 0 && i != 1 ) {
						node.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Node/node") as Sprite;
                        string dicName = "node" + i;
                        if (Node.nodeDic.ContainsKey(dicName))
                        {
                            Node.nodeDic.Remove(dicName);
                            Node.nodeDic.Add(dicName, "Node/node");
                        }
                        else
                        {
                            Node.nodeDic.Add(dicName, "Node/node");
                        }
                    } else if (sumG> 0 && sumB == 0 ) {
						if (i != 1) {
							node.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Node/G") as Sprite;
                            string dicName = "node" + i;
                            if (Node.nodeDic.ContainsKey(dicName))
                            {
                                Node.nodeDic.Remove(dicName);
                                Node.nodeDic.Add(dicName, "Node/G");
                            }
                            else
                            {
                                Node.nodeDic.Add(dicName, "Node/G");
                            }
                        }
					} else if (sumG == 0 && sumB > 0 ) {
						if (i != 1) {
							node.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Node/B") as Sprite;
                            string dicName = "node" + i;
                            if (Node.nodeDic.ContainsKey(dicName))
                            {
                                Node.nodeDic.Remove(dicName);
                                Node.nodeDic.Add(dicName, "Node/B");
                            }
                            else
                            {
                                Node.nodeDic.Add(dicName, "Node/B");
                            }
                        }
					} else if (sumG > 0 && sumB > 0 ) {
						if (i != 1) {
							node.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Node/GB") as Sprite;
                            string dicName = "node" + i;
                            if (Node.nodeDic.ContainsKey(dicName))
                            {
                                Node.nodeDic.Remove(dicName);
                                Node.nodeDic.Add(dicName, "Node/GB");
                            }
                            else
                            {
                                Node.nodeDic.Add(dicName, "Node/GB");
                            }
                        }
					}
				}
			}
			if (b) {
				if (sumB == 0) {
					string strNode = "node" + i;
					GameObject node = GameObject.Find(strNode);
					if (i != 1) {
						if ((node.GetComponent<Node> ().GBN || node.GetComponent<Node> ().RBN)&& !node.GetComponent<Node>().RGBN) {
                            gameControll.intersection--;
							if (gameControll.intersection < 0)
                                gameControll.intersection = 0;
							GameObject.Find("intersection").GetComponent<Text>().text = gameControll.intersection.ToString();
						}

						node.GetComponent<Node> ().BlueN = false;
						node.GetComponent<Node> ().RBN = false;
						node.GetComponent<Node> ().GBN = false;
						node.GetComponent<Node> ().RGBN = false;
					}
					if (sumG == 0 && sumR == 0 && i != 1 ) {
						node.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Node/node") as Sprite;
                        string dicName = "node" + i;
                        if (Node.nodeDic.ContainsKey(dicName))
                        {
                            Node.nodeDic.Remove(dicName);
                            Node.nodeDic.Add(dicName, "Node/node");
                        }
                        else
                        {
                            Node.nodeDic.Add(dicName, "Node/node");
                        }
                    } else if (sumG> 0 && sumR == 0 ) {
						if (i != 1) {
							node.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Node/G") as Sprite;
                            string dicName = "node" + i;
                            if (Node.nodeDic.ContainsKey(dicName))
                            {
                                Node.nodeDic.Remove(dicName);
                                Node.nodeDic.Add(dicName, "Node/G");
                            }
                            else
                            {
                                Node.nodeDic.Add(dicName, "Node/G");
                            }
                        }
					} else if (sumG == 0 && sumR > 0 ) {
						if (i != 1) {
							node.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Node/R") as Sprite;
                            string dicName = "node" + i;
                            if (Node.nodeDic.ContainsKey(dicName))
                            {
                                Node.nodeDic.Remove(dicName);
                                Node.nodeDic.Add(dicName, "Node/R");
                            }
                            else
                            {
                                Node.nodeDic.Add(dicName, "Node/R");
                            }
                        }
					} else if (sumG > 0 && sumR > 0 ) {
						if (i != 1) {
							node.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Node/RG") as Sprite;
                            string dicName = "node" + i;
                            if (Node.nodeDic.ContainsKey(dicName))
                            {
                                Node.nodeDic.Remove(dicName);
                                Node.nodeDic.Add(dicName, "Node/RG");
                            }
                            else
                            {
                                Node.nodeDic.Add(dicName, "Node/RG");
                            }
                        }
					}
				}
			}


			if (g) {
				if (sumG == 0) {
					string strNode = "node" + i;
					GameObject node = GameObject.Find(strNode);
					if (i != 1) {
						if ((node.GetComponent<Node> ().GBN || node.GetComponent<Node> ().RGN)&& !node.GetComponent<Node>().RGBN) {
							gameControll.intersection--;
							if (gameControll.intersection < 0)
								gameControll.intersection = 0;
							GameObject.Find("intersection").GetComponent<Text>().text = gameControll.intersection.ToString();
						}

						node.GetComponent<Node> ().GreenN = false;
						node.GetComponent<Node> ().RGN = false;
						node.GetComponent<Node> ().GBN = false;
						node.GetComponent<Node> ().RGBN = false;
					}
					if (sumB == 0 && sumR == 0 && i != 1 ) {
						node.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Node/node") as Sprite;
                        string dicName = "node" + i;
                        if (Node.nodeDic.ContainsKey(dicName))
                        {
                            Node.nodeDic.Remove(dicName);
                            Node.nodeDic.Add(dicName, "Node/node");
                        }
                        else
                        {
                            Node.nodeDic.Add(dicName, "Node/node");
                        }
                    } else if (sumB> 0 && sumR == 0 ) {
						if (i != 1) {
							node.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Node/B") as Sprite;
                            string dicName = "node" + i;
                            if (Node.nodeDic.ContainsKey(dicName))
                            {
                                Node.nodeDic.Remove(dicName);
                                Node.nodeDic.Add(dicName, "Node/B");
                            }
                            else
                            {
                                Node.nodeDic.Add(dicName, "Node/B");
                            }
                        }
					} else if (sumB == 0 && sumR > 0 ) {
						if (i != 1) {
							node.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Node/R") as Sprite;
                            string dicName = "node" + i;
                            if (Node.nodeDic.ContainsKey(dicName))
                            {
                                Node.nodeDic.Remove(dicName);
                                Node.nodeDic.Add(dicName, "Node/R");
                            }
                            else
                            {
                                Node.nodeDic.Add(dicName, "Node/R");
                            }
                        }
					} else if (sumB > 0 && sumR > 0 ) {
						if (i != 1) {
							node.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Node/RB") as Sprite;
                            string dicName = "node" + i;
                            if (Node.nodeDic.ContainsKey(dicName))
                            {
                                Node.nodeDic.Remove(dicName);
                                Node.nodeDic.Add(dicName, "Node/RB");
                            }
                            else
                            {
                                Node.nodeDic.Add(dicName, "Node/RB");
                            }
                        }
					}
				}
			}
		}
	}
}
	
