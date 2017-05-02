using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using SimpleJSON;

public class CheckMark : MonoBehaviour {

	public static bool nextStep;
	private int node1;
	private int node2;
	private float dist;
	//private float lineDrawSpeed;
	private float counter;
	private Vector3 origin;
	private Vector3 destination;
	private LineRenderer lr;
	private bool startUpdate;

	//a stack to store all unseen indicator
	private Stack<GameObject> indiStack=new Stack<GameObject>();
	public static bool refreshed;

	void Awake(){
  //      if (!gameControll.onlyUpdateAtFirstTimeForCheckMark && !RefreshButton.refresh)
  //      {
  //          addIndicatorAwake(1, 3, false, false, true, 0, 0, 0);
  //          addIndicatorAwake(3, 2, false, false, true, 0, 0, 0);
  //          addIndicatorAwake(2, 1, false, false, true, 0, 0, 0);
  //          addIndicatorAwake(1, 2, false, false, true, 0, 0, 0);
  //          addIndicatorAwake(2, 5, false, false, true, 0, 0, 0);
  //          addIndicatorAwake(5, 2, false, false, true, 0, 0, 0);
  //          addIndicatorAwake(2, 1, false, false, true, 0, 1, 0);
  //          //setIndicatorSeen (indiStack);
  //          addIndicatorAwake(1, 3, true, false, false, 0, 1, 0);
  //          addIndicatorAwake(3, 4, true, false, false, 0, 0, 0);
  //          addIndicatorAwake(4, 1, true, false, false, 0, 0, 0);
  //          addIndicatorAwake(1, 4, true, false, false, 0, 0, 0);
  //          addIndicatorAwake(4, 5, true, false, false, 0, 0, 0);
  //          addIndicatorAwake(5, 4, true, false, false, 0, 0, 0);
  //          addIndicatorAwake(4, 1, true, false, false, 1, 0, 0);
  //          gameControll.onlyUpdateAtFirstTimeForCheckMark = true;
		//}

		//if (RefreshButton.refresh) {
		//	refreshed = true;
		//}
	}
		

	void OnMouseDown(){
		JSONClass details = new JSONClass ();
		details ["ClickCheckMark"] = "finish this route";
		TheLogger.instance.TakeAction (4, details);
		pathCap.desableSlider();
		//GetComponent<AudioSource> ().Play ();
		this.gameObject.GetComponent<Image>().color = new Vector4(0, 0, 0, 0);
		StartCoroutine (waitToDestroy ());
	}


	IEnumerator  wait2seconds()
	{
		yield return new WaitForSeconds(2);
	}

	IEnumerator waitToDestroy()
	{
		for (int j = 0; j < Node.nodeArray.Length - 1; j++)
		{
			addIndicator(Node.nodeArray[j], Node.nodeArray[j + 1], gameControll.redTruck, gameControll.greenTruck, gameControll.blueTruck);
		}
		setIndicatorSeen (indiStack);
		nextStep = true;
		yield return new WaitForSeconds(2);
		Destroy(this.gameObject);
	}
		

	private string pathName(int num1,int num2,bool[,] rgb){
		string name="";
		if (rgb[num1, num2]) {
			name = "newPathAnim" + num1.ToString () + num2.ToString ();
		} else {
			name= "pathAnim"+num1.ToString()+num2.ToString();
		}
		return name;
	}
		

	public void addIndicator(int num1,int num2, bool red, bool green, bool blue)
	{
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
		origin = node1G.transform.position;
		destination = node2G.transform.position;
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

		if (red)
		{
			int rn = Node.redPathNum[num1, num2];
			if (rn > 1)
				return;
			int pn=1;
			Vector3 place = new Vector3();
			if (Node.greenPathNum[num1, num2]== 0 && Node.bluePathNum[num1, num2] == 0 )
			{
				place = origin + new Vector3((float)pn / 3.0f * (destination.x - origin.x), (float)pn / 3.0f * (destination.y - origin.y), 0f);
			}else if (Node.greenPathNum[num1, num2] != 0 && Node.bluePathNum[num1, num2] == 0)
			{
				place = origin + new Vector3((float)pn / 6.0f * (destination.x - origin.x), (float)pn / 6.0f * (destination.y - origin.y), 0f);
				if (pn == 1)
				{
					//int gNum = Node.greenPathNum[num1, num2];
					string indicator1 = "greenIndicator1" + num1.ToString() + num2.ToString();
					GameObject.Find(indicator1).GetComponent<RectTransform>().position = origin +
						new Vector3(1.0f / 6.0f * 4.0f * (destination.x - origin.x), 1.0f / 6.0f * 4.0f * (destination.y - origin.y), 0f);
//					if (gNum == 2)
//					{
//						string indicator2 = "greenIndicator2" + num1.ToString() + num2.ToString();
//						GameObject.Find(indicator2).GetComponent<RectTransform>().position = origin +
//							new Vector3(1.0f / 6.0f * 5.0f * (destination.x - origin.x), 1.0f / 6.0f * 5.0f * (destination.y - origin.y), 0f);
//					}
				}
			}else if(Node.greenPathNum[num1, num2] == 0 && Node.bluePathNum[num1, num2] != 0)
			{
				place = origin + new Vector3((float)pn / 6.0f * (destination.x - origin.x), (float)pn / 6.0f * (destination.y - origin.y), 0f);
				if (pn == 1)
				{
					//int bNum = Node.bluePathNum[num1, num2];
					string indicator1 = "blueIndicator1" + num1.ToString() + num2.ToString();
					GameObject.Find(indicator1).GetComponent<RectTransform>().position = origin +
						new Vector3(1.0f / 6.0f * 4.0f * (destination.x - origin.x), 1.0f / 6.0f * 4.0f * (destination.y - origin.y), 0f);
//					if (bNum == 2)
//					{
//						string indicator2 = "blueIndicator2" + num1.ToString() + num2.ToString();
//						GameObject.Find(indicator2).GetComponent<RectTransform>().position = origin +
//							new Vector3(1.0f / 6.0f * 5.0f * (destination.x - origin.x), 1.0f / 6.0f * 5.0f * (destination.y - origin.y), 0f);
//					}
				}
			}
			else if (Node.greenPathNum[num1, num2] != 0 && Node.bluePathNum[num1, num2] != 0)
			{
				//int bNum = Node.bluePathNum[num1, num2];
				//int gNum = Node.greenPathNum[num1, num2];
				place = origin + new Vector3((float)pn / 9.0f * (destination.x - origin.x), (float)pn / 9.0f * (destination.y - origin.y), 0f);
				if (pn == 1)
				{
					string indicator1 = "greenIndicator1" + num1.ToString() + num2.ToString();
					GameObject.Find(indicator1).GetComponent<RectTransform>().position = origin +
						new Vector3(4.0f / 9.0f  * (destination.x - origin.x), 4.0f / 9.0f * (destination.y - origin.y), 0f);
					indicator1 = "blueIndicator1" + num1.ToString() + num2.ToString();
					GameObject.Find(indicator1).GetComponent<RectTransform>().position = origin +
						new Vector3(7.0f / 9.0f  * (destination.x - origin.x), 7.0f / 9.0f * (destination.y - origin.y), 0f);
//					if (gNum == 2)
//					{
//						string indicator2 = "greenIndicator2" + num1.ToString() + num2.ToString();
//						GameObject.Find(indicator2).GetComponent<RectTransform>().position = origin +
//							new Vector3(5.0f / 9.0f * (destination.x - origin.x), 5.0f/ 9.0f * (destination.y - origin.y), 0f);
//					}
//
//					if (bNum == 2)
//					{
//						string indicator2 = "blueIndicator2" + num1.ToString() + num2.ToString();
//						GameObject.Find(indicator2).GetComponent<RectTransform>().position = origin +
//							new Vector3(8.0f / 9.0f  * (destination.x - origin.x), 8.0f / 9.0f * (destination.y - origin.y), 0f);
//					}
				}
			}
			//add indicator here
			GameObject indicator = new GameObject();
			indicator.AddComponent<RectTransform>();
			indicator.tag = "indi";
			indicator.transform.SetParent(GameObject.Find("gamePanel").transform, false);
			indicator.AddComponent<Image>();
			//indicator.AddComponent<BoxCollider2D> ();
			//indicator.GetComponent<BoxCollider2D> ().enabled = true;
			//indicator.GetComponent<BoxCollider2D> ().size = new Vector2 (100, 100);
			//indicator.AddComponent<indicatorFunction> ();

			if (pn == 1)
			{
				indicator.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/indicatorR") as Sprite;
				string indicatorName = "redIndicator1" + num1.ToString() + num2.ToString();
				indicator.name = indicatorName;
			}
			if (pn == 2)
			{
				string indicatorName = "redIndicator2" + num1.ToString() + num2.ToString();
				indicator.name = indicatorName;
				indicator.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/indicatorR2") as Sprite;
			}
			indicator.GetComponent<RectTransform>().position = place;
			indicator.GetComponent<RectTransform>().localScale = new Vector3(0.15f, 0.15f, 0.15f);
			if (origin.x < destination.x)
			{
				indicator.transform.Rotate(new Vector3(0, 0, Mathf.Atan(slope) * Mathf.Rad2Deg-180f));
			}else
			{
				indicator.transform.Rotate(new Vector3(0, 0, Mathf.Atan(slope) * Mathf.Rad2Deg));
			}
		}

		if (green)
		{
			int gn = Node.greenPathNum[num1, num2];
			if (gn > 1)
				return;
			int pn=1;
			Vector3 place = new Vector3();
			if (Node.redPathNum[num1, num2] == 0 && Node.bluePathNum[num1, num2] == 0)
			{
				place = origin + new Vector3((float)pn / 3.0f * (destination.x - origin.x), (float)pn / 3.0f * (destination.y - origin.y), 0f);
			}
			else if (Node.redPathNum[num1, num2] != 0 && Node.bluePathNum[num1, num2] == 0)
			{
				place = origin + new Vector3(((float)pn + 3.0f) / 6.0f * (destination.x - origin.x), ((float)pn + 3.0f) / 6.0f * (destination.y - origin.y), 0f);
				if (pn == 1)
				{
					//int rNum = Node.redPathNum[num1, num2];
					string indicator1 = "redIndicator1" + num1.ToString() + num2.ToString();
					GameObject.Find(indicator1).GetComponent<RectTransform>().position = origin +
						new Vector3(1.0f / 6.0f * (destination.x - origin.x), 1.0f / 6.0f  * (destination.y - origin.y), 0f);
//					if (rNum == 2)
//					{
//						string indicator2 = "redIndicator2" + num1.ToString() + num2.ToString();
//						GameObject.Find(indicator2).GetComponent<RectTransform>().position = origin +
//							new Vector3(1.0f / 6.0f * 2.0f * (destination.x - origin.x), 1.0f / 6.0f * 2.0f * (destination.y - origin.y), 0f);
//					}
				}
			}
			else if (Node.redPathNum[num1, num2] == 0 && Node.bluePathNum[num1, num2] != 0)
			{
				place = origin + new Vector3((float)pn / 6.0f * (destination.x - origin.x), (float)pn / 6.0f * (destination.y - origin.y), 0f);
				if (pn == 1)
				{
					//int bNum = Node.bluePathNum[num1, num2];
					string indicator1 = "blueIndicator1" + num1.ToString() + num2.ToString();
					GameObject.Find(indicator1).GetComponent<RectTransform>().position = origin +
						new Vector3(1.0f / 6.0f * 4.0f * (destination.x - origin.x), 1.0f / 6.0f * 4.0f * (destination.y - origin.y), 0f);
//					if (bNum == 2)
//					{
//						string indicator2 = "blueIndicator2" + num1.ToString() + num2.ToString();
//						GameObject.Find(indicator2).GetComponent<RectTransform>().position = origin +
//							new Vector3(1.0f / 6.0f * 5.0f * (destination.x - origin.x), 1.0f / 6.0f * 5.0f * (destination.y - origin.y), 0f);
//					}
				}
			}
			else if (Node.redPathNum[num1, num2] != 0 && Node.bluePathNum[num1, num2] != 0)
			{
				//int bNum = Node.bluePathNum[num1, num2];
				//int rNum = Node.redPathNum[num1, num2];
				place = origin + new Vector3(((float)pn + 3.0f) / 9.0f * (destination.x - origin.x), ((float)pn + 3.0f) / 9.0f * (destination.y - origin.y), 0f);
				if (pn == 1)
				{
					string indicator1 = "redIndicator1" + num1.ToString() + num2.ToString();
					GameObject.Find(indicator1).GetComponent<RectTransform>().position = origin +
						new Vector3(1.0f / 9.0f * (destination.x - origin.x), 1.0f / 9.0f * (destination.y - origin.y), 0f);
					indicator1 = "blueIndicator1" + num1.ToString() + num2.ToString();
					GameObject.Find(indicator1).GetComponent<RectTransform>().position = origin +
						new Vector3(7.0f / 9.0f * (destination.x - origin.x), 7.0f / 9.0f * (destination.y - origin.y), 0f);
//					if (rNum == 2)
//					{
//						string indicator2 = "redIndicator2" + num1.ToString() + num2.ToString();
//						GameObject.Find(indicator2).GetComponent<RectTransform>().position = origin +
//							new Vector3(2.0f / 9.0f * (destination.x - origin.x), 2.0f / 9.0f * (destination.y - origin.y), 0f);
//					}

//					if (bNum == 2)
//					{
//						string indicator2 = "blueIndicator2" + num1.ToString() + num2.ToString();
//						GameObject.Find(indicator2).GetComponent<RectTransform>().position = origin +
//							new Vector3(8.0f / 9.0f * (destination.x - origin.x), 8.0f/9.0f * (destination.y - origin.y), 0f);
//					}
				}
			}
			//add indicator here
			GameObject indicator = new GameObject();
			indicator.AddComponent<RectTransform>();
			indicator.tag = "indi";
			indicator.transform.SetParent(GameObject.Find("gamePanel").transform, false);
			indicator.AddComponent<Image>();
//			indicator.AddComponent<BoxCollider2D> ();
//			indicator.GetComponent<BoxCollider2D> ().enabled = true;
//			indicator.GetComponent<BoxCollider2D> ().size = new Vector2 (100, 100);
//			indicator.AddComponent<indicatorFunction> ();
//
			if (pn == 1)
			{
				indicator.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/indicatorG") as Sprite;
				string indicatorName = "greenIndicator1" + num1.ToString() + num2.ToString();
				indicator.name = indicatorName;
			}
			if (pn == 2)
			{
				string indicatorName = "greenIndicator2" + num1.ToString() + num2.ToString();
				indicator.name = indicatorName;
				indicator.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/indicatorG2") as Sprite;
			}
			indicator.GetComponent<RectTransform>().position = place;
			indicator.GetComponent<RectTransform>().localScale = new Vector3(0.15f, 0.15f, 0.15f);
			if (origin.x < destination.x)
			{
				indicator.transform.Rotate(new Vector3(0, 0, Mathf.Atan(slope) * Mathf.Rad2Deg - 180f));
			}
			else
			{
				indicator.transform.Rotate(new Vector3(0, 0, Mathf.Atan(slope) * Mathf.Rad2Deg));
			}
		}

		if (blue)
		{
			int bn = Node.bluePathNum[num1, num2];
			if (bn > 1)
				return;
			int pn=1;
			Vector3 place = new Vector3();
			if (Node.redPathNum[num1, num2] == 0 && Node.greenPathNum[num1, num2] == 0)
			{
				place = origin + new Vector3((float)pn / 3.0f * (destination.x - origin.x), (float)pn / 3.0f * (destination.y - origin.y), 0f);
			}
			else if (Node.redPathNum[num1, num2] != 0 && Node.greenPathNum[num1, num2] == 0)
			{
				place = origin + new Vector3(((float)pn + 3.0f) / 6.0f * (destination.x - origin.x), ((float)pn + 3.0f) / 6.0f * (destination.y - origin.y), 0f);
				if (pn == 1)
				{
					//int rNum = Node.redPathNum[num1, num2];
					string indicator1 = "redIndicator1" + num1.ToString() + num2.ToString();
					GameObject.Find(indicator1).GetComponent<RectTransform>().position = origin +
						new Vector3(1.0f / 6.0f * (destination.x - origin.x), 1.0f / 6.0f * (destination.y - origin.y), 0f);
//					if (rNum == 2)
//					{
//						string indicator2 = "redIndicator2" + num1.ToString() + num2.ToString();
//						GameObject.Find(indicator2).GetComponent<RectTransform>().position = origin +
//							new Vector3(1.0f / 6.0f * 2.0f * (destination.x - origin.x), 1.0f / 6.0f * 2.0f * (destination.y - origin.y), 0f);
//					}
				}
			}
			else if (Node.redPathNum[num1, num2] == 0 && Node.greenPathNum[num1, num2] != 0)
			{
				place = origin + new Vector3(((float)pn + 3.0f) / 6.0f * (destination.x - origin.x), ((float)pn + 3.0f) / 6.0f * (destination.y - origin.y), 0f);
				if (pn == 1)
				{
					//int gNum = Node.greenPathNum[num1, num2];
					string indicator1 = "greenIndicator1" + num1.ToString() + num2.ToString();
					GameObject.Find(indicator1).GetComponent<RectTransform>().position = origin +
						new Vector3(1.0f / 6.0f * (destination.x - origin.x), 1.0f / 6.0f * (destination.y - origin.y), 0f);
//					if (gNum == 2)
//					{
//						string indicator2 = "greenIndicator2" + num1.ToString() + num2.ToString();
//						GameObject.Find(indicator2).GetComponent<RectTransform>().position = origin +
//							new Vector3(2.0f / 6.0f * (destination.x - origin.x), 2.0f / 6.0f * (destination.y - origin.y), 0f);
//					}
				}
			}
			else if (Node.redPathNum[num1, num2] != 0 && Node.greenPathNum[num1, num2] != 0)
			{
				//int gNum = Node.greenPathNum[num1, num2];
				//int rNum = Node.redPathNum[num1, num2];
				place = origin + new Vector3(((float)pn + 6.0f) / 9.0f * (destination.x - origin.x), ((float)pn + 6.0f) / 9.0f * (destination.y - origin.y), 0f);
				if (pn == 1)
				{
					string indicator1 = "redIndicator1" + num1.ToString() + num2.ToString();
					GameObject.Find(indicator1).GetComponent<RectTransform>().position = origin +
						new Vector3(1.0f / 9.0f * (destination.x - origin.x), 1.0f / 9.0f * (destination.y - origin.y), 0f);
					indicator1 = "greenIndicator1" + num1.ToString() + num2.ToString();
					GameObject.Find(indicator1).GetComponent<RectTransform>().position = origin +
						new Vector3(4.0f / 9.0f * (destination.x - origin.x), 4.0f / 9.0f * (destination.y - origin.y), 0f);
//					if (rNum == 2)
//					{
//						string indicator2 = "redIndicator2" + num1.ToString() + num2.ToString();
//						GameObject.Find(indicator2).GetComponent<RectTransform>().position = origin +
//							new Vector3(2.0f / 9.0f * (destination.x - origin.x), 2.0f / 9.0f * (destination.y - origin.y), 0f);
//					}
//
//					if (gNum == 2)
//					{
//						string indicator2 = "greenIndicator2" + num1.ToString() + num2.ToString();
//						GameObject.Find(indicator2).GetComponent<RectTransform>().position = origin +
//							new Vector3(5.0f / 9.0f * (destination.x - origin.x), 5.0f / 9.0f * (destination.y - origin.y), 0f);
//					}
				}
			}

			//add indicator here
			GameObject indicator = new GameObject();
			indicator.AddComponent<RectTransform>();
			indicator.tag = "indi";
			indicator.transform.SetParent(GameObject.Find("gamePanel").transform, false);
			indicator.AddComponent<Image>();
//			indicator.AddComponent<BoxCollider2D> ();
//			indicator.GetComponent<BoxCollider2D> ().enabled = true;
//			indicator.GetComponent<BoxCollider2D> ().size = new Vector2 (100, 100);
//			indicator.AddComponent<indicatorFunction> ();
			if (pn == 1)
			{
				indicator.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/indicatorB") as Sprite;
				string indicatorName = "blueIndicator1" + num1.ToString() + num2.ToString();
				indicator.name = indicatorName;
			}
			if (pn == 2)
			{
				string indicatorName = "blueIndicator2" + num1.ToString() + num2.ToString();
				indicator.name = indicatorName;
				indicator.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/indicatorB2") as Sprite;
			}
			indicator.GetComponent<RectTransform>().position = place;
			indicator.GetComponent<RectTransform>().localScale = new Vector3(0.15f, 0.15f, 0.15f);
			if (origin.x < destination.x)
			{
				indicator.transform.Rotate(new Vector3(0, 0, Mathf.Atan(slope) * Mathf.Rad2Deg - 180f));
			}
			else
			{
				indicator.transform.Rotate(new Vector3(0, 0, Mathf.Atan(slope) * Mathf.Rad2Deg));
			}
		}

	}

	private void setIndicatorUnseen(int num1,int num2){
		string indicatorR1 = "redIndicator1" + num1.ToString () + num2.ToString ();
		string indicatorR2 = "redIndicator2" + num1.ToString () + num2.ToString ();
		string indicatorG1 = "greenIndicator1" + num1.ToString () + num2.ToString ();
		string indicatorG2 = "greenIndicator2" + num1.ToString () + num2.ToString ();
		string indicatorB1 = "blueIndicator1" + num1.ToString () + num2.ToString ();
		string indicatorB2 = "blueIndicator2" + num1.ToString () + num2.ToString ();
		if (GameObject.Find (indicatorR1) != null) {
			GameObject obj = GameObject.Find (indicatorR1);
			obj.GetComponent<Image> ().color = new Vector4 (0, 0, 0, 0);
			indiStack.Push (obj);
		}
		if (GameObject.Find (indicatorR2) != null) {
			GameObject obj = GameObject.Find (indicatorR2);
			obj.GetComponent<Image> ().color=new Vector4 (0, 0, 0, 0);
			indiStack.Push (obj);
		}
		if (GameObject.Find (indicatorG1) != null) {
			GameObject obj = GameObject.Find (indicatorG1);
			obj.GetComponent<Image> ().color=new Vector4 (0, 0, 0, 0);
			indiStack.Push (obj);
		}
		if (GameObject.Find (indicatorG2) != null) {
			GameObject obj = GameObject.Find (indicatorG2);
			obj.GetComponent<Image> ().color=new Vector4 (0, 0, 0, 0);
			indiStack.Push (obj);
		}
		if (GameObject.Find (indicatorB1) != null) {
			GameObject obj = GameObject.Find (indicatorB1);
			obj.GetComponent<Image> ().color=new Vector4 (0, 0, 0, 0);
			indiStack.Push (obj);
		}
		if (GameObject.Find (indicatorB2) != null) {
			GameObject obj = GameObject.Find (indicatorB2);
			obj.GetComponent<Image> ().color=new Vector4 (0, 0, 0, 0);
			indiStack.Push (obj);
		}
	}

	private void setIndicatorSeen(Stack<GameObject> st){
		while (st.Count != 0) {
			//Debug.Log (1111);
			GameObject obj = st.Pop ();
			obj.GetComponent<Image> ().color=new Vector4 (1, 1, 1, 1);
		}
	}

	public void addIndicatorAwake(int num1,int num2, bool red, bool green, bool blue,int redNum,int blueNum,int greenNum)
	{
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
		origin = node1G.transform.position;
		destination = node2G.transform.position;
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

		if (red)
		{
			string indiName = "redIndicator1" + num1.ToString () + num2.ToString ();
			if (GameObject.Find (indiName) != null) {
				return;
			}
			int rn = redNum;
			if (rn > 1)
				return;
			int pn=1;
			Vector3 place = new Vector3();
			if (greenNum== 0 && blueNum == 0 )
			{
				place = origin + new Vector3((float)pn / 3.0f * (destination.x - origin.x), (float)pn / 3.0f * (destination.y - origin.y), 0f);
			}else if (greenNum != 0 && blueNum == 0)
			{
				place = origin + new Vector3((float)pn / 6.0f * (destination.x - origin.x), (float)pn / 6.0f * (destination.y - origin.y), 0f);
				if (pn == 1)
				{
					//int gNum = greenNum;
					string indicator1 = "greenIndicator1" + num1.ToString() + num2.ToString();
					GameObject.Find(indicator1).GetComponent<RectTransform>().position = origin +
						new Vector3(1.0f / 6.0f * 4.0f * (destination.x - origin.x), 1.0f / 6.0f * 4.0f * (destination.y - origin.y), 0f);
					//					if (gNum == 2)
					//					{
					//						string indicator2 = "greenIndicator2" + num1.ToString() + num2.ToString();
					//						GameObject.Find(indicator2).GetComponent<RectTransform>().position = origin +
					//							new Vector3(1.0f / 6.0f * 5.0f * (destination.x - origin.x), 1.0f / 6.0f * 5.0f * (destination.y - origin.y), 0f);
					//					}
				}
			}else if(greenNum == 0 && blueNum != 0)
			{
				place = origin + new Vector3((float)pn / 6.0f * (destination.x - origin.x), (float)pn / 6.0f * (destination.y - origin.y), 0f);
				if (pn == 1)
				{
					//int bNum = blueNum;
					string indicator1 = "blueIndicator1" + num1.ToString() + num2.ToString();
					GameObject.Find(indicator1).GetComponent<RectTransform>().position = origin +
						new Vector3(1.0f / 6.0f * 4.0f * (destination.x - origin.x), 1.0f / 6.0f * 4.0f * (destination.y - origin.y), 0f);
					//					if (bNum == 2)
					//					{
					//						string indicator2 = "blueIndicator2" + num1.ToString() + num2.ToString();
					//						GameObject.Find(indicator2).GetComponent<RectTransform>().position = origin +
					//							new Vector3(1.0f / 6.0f * 5.0f * (destination.x - origin.x), 1.0f / 6.0f * 5.0f * (destination.y - origin.y), 0f);
					//					}
				}
			}
			else if (greenNum != 0 && blueNum != 0)
			{
				//int bNum = blueNum;
				//int gNum = greenNum;
				place = origin + new Vector3((float)pn / 9.0f * (destination.x - origin.x), (float)pn / 9.0f * (destination.y - origin.y), 0f);
				if (pn == 1)
				{
					string indicator1 = "greenIndicator1" + num1.ToString() + num2.ToString();
					GameObject.Find(indicator1).GetComponent<RectTransform>().position = origin +
						new Vector3(4.0f / 9.0f  * (destination.x - origin.x), 4.0f / 9.0f * (destination.y - origin.y), 0f);
					indicator1 = "blueIndicator1" + num1.ToString() + num2.ToString();
					GameObject.Find(indicator1).GetComponent<RectTransform>().position = origin +
						new Vector3(7.0f / 9.0f  * (destination.x - origin.x), 7.0f / 9.0f * (destination.y - origin.y), 0f);
					//					if (gNum == 2)
					//					{
					//						string indicator2 = "greenIndicator2" + num1.ToString() + num2.ToString();
					//						GameObject.Find(indicator2).GetComponent<RectTransform>().position = origin +
					//							new Vector3(5.0f / 9.0f * (destination.x - origin.x), 5.0f/ 9.0f * (destination.y - origin.y), 0f);
					//					}
					//
					//					if (bNum == 2)
					//					{
					//						string indicator2 = "blueIndicator2" + num1.ToString() + num2.ToString();
					//						GameObject.Find(indicator2).GetComponent<RectTransform>().position = origin +
					//							new Vector3(8.0f / 9.0f  * (destination.x - origin.x), 8.0f / 9.0f * (destination.y - origin.y), 0f);
					//					}
				}
			}
			//add indicator here
			GameObject indicator = new GameObject();
			indicator.AddComponent<RectTransform>();
			indicator.tag = "indi";
			indicator.transform.SetParent(GameObject.Find("gamePanel").transform, false);
			indicator.AddComponent<Image>();
			//indicator.AddComponent<BoxCollider2D> ();
			//indicator.GetComponent<BoxCollider2D> ().enabled = true;
			//indicator.GetComponent<BoxCollider2D> ().size = new Vector2 (100, 100);
			//indicator.AddComponent<indicatorFunction> ();

			if (pn == 1)
			{
				indicator.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/indicatorR") as Sprite;
				string indicatorName = "redIndicator1" + num1.ToString() + num2.ToString();
				indicator.name = indicatorName;
			}
			if (pn == 2)
			{
				string indicatorName = "redIndicator2" + num1.ToString() + num2.ToString();
				indicator.name = indicatorName;
				indicator.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/indicatorR2") as Sprite;
			}
			indicator.GetComponent<RectTransform>().position = place;
			indicator.GetComponent<RectTransform>().localScale = new Vector3(0.15f, 0.15f, 0.15f);
			if (origin.x < destination.x)
			{
				indicator.transform.Rotate(new Vector3(0, 0, Mathf.Atan(slope) * Mathf.Rad2Deg-180f));
			}else
			{
				indicator.transform.Rotate(new Vector3(0, 0, Mathf.Atan(slope) * Mathf.Rad2Deg));
			}
		}

		if (green)
		{
			string indiName = "greenIndicator1" + num1.ToString () + num2.ToString ();
			if (GameObject.Find (indiName) != null) {
				return;
			}
			int gn = greenNum;
			if (gn > 1)
				return;
			int pn=1;
			Vector3 place = new Vector3();
			if (redNum == 0 && blueNum == 0)
			{
				place = origin + new Vector3((float)pn / 3.0f * (destination.x - origin.x), (float)pn / 3.0f * (destination.y - origin.y), 0f);
			}
			else if (redNum != 0 && blueNum == 0)
			{
				place = origin + new Vector3(((float)pn + 3.0f) / 6.0f * (destination.x - origin.x), ((float)pn + 3.0f) / 6.0f * (destination.y - origin.y), 0f);
				if (pn == 1)
				{
					//int rNum = redNum;
					string indicator1 = "redIndicator1" + num1.ToString() + num2.ToString();
					GameObject.Find(indicator1).GetComponent<RectTransform>().position = origin +
						new Vector3(1.0f / 6.0f * (destination.x - origin.x), 1.0f / 6.0f  * (destination.y - origin.y), 0f);
					//					if (rNum == 2)
					//					{
					//						string indicator2 = "redIndicator2" + num1.ToString() + num2.ToString();
					//						GameObject.Find(indicator2).GetComponent<RectTransform>().position = origin +
					//							new Vector3(1.0f / 6.0f * 2.0f * (destination.x - origin.x), 1.0f / 6.0f * 2.0f * (destination.y - origin.y), 0f);
					//					}
				}
			}
			else if (redNum == 0 && blueNum != 0)
			{
				place = origin + new Vector3((float)pn / 6.0f * (destination.x - origin.x), (float)pn / 6.0f * (destination.y - origin.y), 0f);
				if (pn == 1)
				{
					//int bNum = blueNum;
					string indicator1 = "blueIndicator1" + num1.ToString() + num2.ToString();
					GameObject.Find(indicator1).GetComponent<RectTransform>().position = origin +
						new Vector3(1.0f / 6.0f * 4.0f * (destination.x - origin.x), 1.0f / 6.0f * 4.0f * (destination.y - origin.y), 0f);
					//					if (bNum == 2)
					//					{
					//						string indicator2 = "blueIndicator2" + num1.ToString() + num2.ToString();
					//						GameObject.Find(indicator2).GetComponent<RectTransform>().position = origin +
					//							new Vector3(1.0f / 6.0f * 5.0f * (destination.x - origin.x), 1.0f / 6.0f * 5.0f * (destination.y - origin.y), 0f);
					//					}
				}
			}
			else if (redNum != 0 && blueNum != 0)
			{
				//int bNum = blueNum;
				//int rNum = redNum;
				place = origin + new Vector3(((float)pn + 3.0f) / 9.0f * (destination.x - origin.x), ((float)pn + 3.0f) / 9.0f * (destination.y - origin.y), 0f);
				if (pn == 1)
				{
					string indicator1 = "redIndicator1" + num1.ToString() + num2.ToString();
					GameObject.Find(indicator1).GetComponent<RectTransform>().position = origin +
						new Vector3(1.0f / 9.0f * (destination.x - origin.x), 1.0f / 9.0f * (destination.y - origin.y), 0f);
					indicator1 = "blueIndicator1" + num1.ToString() + num2.ToString();
					GameObject.Find(indicator1).GetComponent<RectTransform>().position = origin +
						new Vector3(7.0f / 9.0f * (destination.x - origin.x), 7.0f / 9.0f * (destination.y - origin.y), 0f);
					//					if (rNum == 2)
					//					{
					//						string indicator2 = "redIndicator2" + num1.ToString() + num2.ToString();
					//						GameObject.Find(indicator2).GetComponent<RectTransform>().position = origin +
					//							new Vector3(2.0f / 9.0f * (destination.x - origin.x), 2.0f / 9.0f * (destination.y - origin.y), 0f);
					//					}

					//					if (bNum == 2)
					//					{
					//						string indicator2 = "blueIndicator2" + num1.ToString() + num2.ToString();
					//						GameObject.Find(indicator2).GetComponent<RectTransform>().position = origin +
					//							new Vector3(8.0f / 9.0f * (destination.x - origin.x), 8.0f/9.0f * (destination.y - origin.y), 0f);
					//					}
				}
			}
			//add indicator here
			GameObject indicator = new GameObject();
			indicator.AddComponent<RectTransform>();
			indicator.tag = "indi";
			indicator.transform.SetParent(GameObject.Find("gamePanel").transform, false);
			indicator.AddComponent<Image>();
			//			indicator.AddComponent<BoxCollider2D> ();
			//			indicator.GetComponent<BoxCollider2D> ().enabled = true;
			//			indicator.GetComponent<BoxCollider2D> ().size = new Vector2 (100, 100);
			//			indicator.AddComponent<indicatorFunction> ();
			//
			if (pn == 1)
			{
				indicator.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/indicatorG") as Sprite;
				string indicatorName = "greenIndicator1" + num1.ToString() + num2.ToString();
				indicator.name = indicatorName;
			}
			if (pn == 2)
			{
				string indicatorName = "greenIndicator2" + num1.ToString() + num2.ToString();
				indicator.name = indicatorName;
				indicator.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/indicatorG2") as Sprite;
			}
			indicator.GetComponent<RectTransform>().position = place;
			indicator.GetComponent<RectTransform>().localScale = new Vector3(0.15f, 0.15f, 0.15f);
			if (origin.x < destination.x)
			{
				indicator.transform.Rotate(new Vector3(0, 0, Mathf.Atan(slope) * Mathf.Rad2Deg - 180f));
			}
			else
			{
				indicator.transform.Rotate(new Vector3(0, 0, Mathf.Atan(slope) * Mathf.Rad2Deg));
			}
		}

		if (blue)
		{
			string indiName = "blueIndicator1" + num1.ToString () + num2.ToString ();
			if (GameObject.Find (indiName) != null) {
				return;
			}
			int bn = blueNum;
			if (bn > 1)
				return;
			int pn=1;
			Vector3 place = new Vector3();
			if (redNum == 0 && greenNum == 0)
			{
				place = origin + new Vector3((float)pn / 3.0f * (destination.x - origin.x), (float)pn / 3.0f * (destination.y - origin.y), 0f);
			}
			else if (redNum != 0 && greenNum == 0)
			{
				place = origin + new Vector3(((float)pn + 3.0f) / 6.0f * (destination.x - origin.x), ((float)pn + 3.0f) / 6.0f * (destination.y - origin.y), 0f);
				if (pn == 1)
				{
					//int rNum = redNum;
					string indicator1 = "redIndicator1" + num1.ToString() + num2.ToString();
					GameObject.Find(indicator1).GetComponent<RectTransform>().position = origin +
						new Vector3(1.0f / 6.0f * (destination.x - origin.x), 1.0f / 6.0f * (destination.y - origin.y), 0f);
					//					if (rNum == 2)
					//					{
					//						string indicator2 = "redIndicator2" + num1.ToString() + num2.ToString();
					//						GameObject.Find(indicator2).GetComponent<RectTransform>().position = origin +
					//							new Vector3(1.0f / 6.0f * 2.0f * (destination.x - origin.x), 1.0f / 6.0f * 2.0f * (destination.y - origin.y), 0f);
					//					}
				}
			}
			else if (redNum == 0 && greenNum != 0)
			{
				place = origin + new Vector3(((float)pn + 3.0f) / 6.0f * (destination.x - origin.x), ((float)pn + 3.0f) / 6.0f * (destination.y - origin.y), 0f);
				if (pn == 1)
				{
					//int gNum = greenNum;
					string indicator1 = "greenIndicator1" + num1.ToString() + num2.ToString();
					GameObject.Find(indicator1).GetComponent<RectTransform>().position = origin +
						new Vector3(1.0f / 6.0f * (destination.x - origin.x), 1.0f / 6.0f * (destination.y - origin.y), 0f);
					//					if (gNum == 2)
					//					{
					//						string indicator2 = "greenIndicator2" + num1.ToString() + num2.ToString();
					//						GameObject.Find(indicator2).GetComponent<RectTransform>().position = origin +
					//							new Vector3(2.0f / 6.0f * (destination.x - origin.x), 2.0f / 6.0f * (destination.y - origin.y), 0f);
					//					}
				}
			}
			else if (redNum != 0 && greenNum != 0)
			{
				//int gNum = greenNum;
				//int rNum = redNum;
				place = origin + new Vector3(((float)pn + 6.0f) / 9.0f * (destination.x - origin.x), ((float)pn + 6.0f) / 9.0f * (destination.y - origin.y), 0f);
				if (pn == 1)
				{
					string indicator1 = "redIndicator1" + num1.ToString() + num2.ToString();
					GameObject.Find(indicator1).GetComponent<RectTransform>().position = origin +
						new Vector3(1.0f / 9.0f * (destination.x - origin.x), 1.0f / 9.0f * (destination.y - origin.y), 0f);
					indicator1 = "greenIndicator1" + num1.ToString() + num2.ToString();
					GameObject.Find(indicator1).GetComponent<RectTransform>().position = origin +
						new Vector3(4.0f / 9.0f * (destination.x - origin.x), 4.0f / 9.0f * (destination.y - origin.y), 0f);
					//					if (rNum == 2)
					//					{
					//						string indicator2 = "redIndicator2" + num1.ToString() + num2.ToString();
					//						GameObject.Find(indicator2).GetComponent<RectTransform>().position = origin +
					//							new Vector3(2.0f / 9.0f * (destination.x - origin.x), 2.0f / 9.0f * (destination.y - origin.y), 0f);
					//					}
					//
					//					if (gNum == 2)
					//					{
					//						string indicator2 = "greenIndicator2" + num1.ToString() + num2.ToString();
					//						GameObject.Find(indicator2).GetComponent<RectTransform>().position = origin +
					//							new Vector3(5.0f / 9.0f * (destination.x - origin.x), 5.0f / 9.0f * (destination.y - origin.y), 0f);
					//					}
				}
			}

			//add indicator here
			GameObject indicator = new GameObject();
			indicator.AddComponent<RectTransform>();
			indicator.tag = "indi";
			indicator.transform.SetParent(GameObject.Find("gamePanel").transform, false);
			indicator.AddComponent<Image>();
			//			indicator.AddComponent<BoxCollider2D> ();
			//			indicator.GetComponent<BoxCollider2D> ().enabled = true;
			//			indicator.GetComponent<BoxCollider2D> ().size = new Vector2 (100, 100);
			//			indicator.AddComponent<indicatorFunction> ();
			if (pn == 1)
			{
				indicator.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/indicatorB") as Sprite;
				string indicatorName = "blueIndicator1" + num1.ToString() + num2.ToString();
				indicator.name = indicatorName;
			}
			if (pn == 2)
			{
				string indicatorName = "blueIndicator2" + num1.ToString() + num2.ToString();
				indicator.name = indicatorName;
				indicator.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/indicatorB2") as Sprite;
			}
			indicator.GetComponent<RectTransform>().position = place;
			indicator.GetComponent<RectTransform>().localScale = new Vector3(0.15f, 0.15f, 0.15f);
			if (origin.x < destination.x)
			{
				indicator.transform.Rotate(new Vector3(0, 0, Mathf.Atan(slope) * Mathf.Rad2Deg - 180f));
			}
			else
			{
				indicator.transform.Rotate(new Vector3(0, 0, Mathf.Atan(slope) * Mathf.Rad2Deg));
			}
		}

	}
}
