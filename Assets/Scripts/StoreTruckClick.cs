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
	public static List<int> arrayForTruckCap;
	public static float numForProfit;
	public static float numForTime;
	private int numClick=0;
    //public static bool onlyOneClick;
	//public static Queue<GameObject> objQueue=new Queue<GameObject>();
	void OnMouseDown(){
        numClick++;
		//materialQueue.Clear ();
		if (!(gameControll.redTruck || gameControll.greenTruck || gameControll.blueTruck)) {
            //indicatorFunction.setCancelPathButton ();
            List<int> nodePathL = new List<int>();
            List<int> nodeTrcukCap = new List<int>();
            if (numClick % 2 == 1 && !(red || green || blue)) {
                //onlyOneClick = true;
				string truckName = this.gameObject.name;
				string resultString = Regex.Match(truckName, @"\d+").Value;
				int truckNum = int.Parse (resultString);
				theTruckNum = truckNum;
				//objQueue.Enqueue (this.gameObject);
				//List<int> nodePathL = new List<int> ();
				//List<int> nodeTrcukCap = new List<int> ();
				float profit = 0f;
				float time = 0f;
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
					int k = 0;
					//Debug.Log (truckNum + "length of this");
					foreach (List<int> l in Node.redTruckCap) {
						if (k == truckNum) {
							nodeTrcukCap = l;
							foreach(int te in l){
								Debug.Log (te);
							}
							break;
						}
						k++;
					}
					int n1 = 0;
					foreach (float l in Node.redProfitAl) {
						if (n1 == truckNum) {
							profit = l;
							break;
						}
						n1 ++;
					}
					int n2 = 0;
					foreach (float l in Node.redTimeAl) {
						if (n2 == theTruckNum) {
							time = l;
							break;
						}
						n2++;
					}
					//Debug.Log ("node length" + nodeTrcukCap.Count);
					int[] nodeArr1 = nodePathL.ToArray ();
					int[] nodeTruckArr = nodeTrcukCap.ToArray ();
					for (int j = 0; j < nodeArr1.Length-1; j++) {
						int num1 = nodeArr1 [j];
						int num2 = nodeArr1 [j + 1];
						string pathString = "pathAnim" + num1.ToString () + num2.ToString ();
						string truckText1 = "truckCap" + num1.ToString () + num2.ToString ();
						string truckText2 = "truckCap" + num2.ToString() + num1.ToString ();
						GameObject truckCap = new GameObject ();
						if (GameObject.Find (truckText1) != null) {
							truckCap = GameObject.Find (truckText1);
						} else if (GameObject.Find (truckText2) != null) {
							truckCap = GameObject.Find (truckText2);
						}
						Text t = truckCap.GetComponent<Text> ();
						t.enabled = true;
						t.text = nodeTruckArr [j].ToString ();
						t.GetComponent<Text> ().fontSize = 22;
						//addText.GetComponent<Text> ().font = Resources.GetBuiltinResource (typeof(Font), "Arial.ttf") as Font;
						t.GetComponent<Text> ().font =Resources.Load<Font>("Font/AGENCYR") as Font;
						t.GetComponent<Text> ().fontStyle = FontStyle.Normal;
						t.GetComponent<Text> ().alignment = TextAnchor.MiddleCenter;
						//addText.GetComponent<Text> ().color = new Color (1, 0, 0, 1);
						t.GetComponent<Text> ().fontStyle = FontStyle.Bold;
						string htmlValue = "#db4f69";
						Color newCol;
						if (ColorUtility.TryParseHtmlString (htmlValue, out newCol)) {
							t.color = newCol;
						}
						GameObject obj = GameObject.Find (pathString);
						//					obj.GetComponent<LineRenderer> ().material = Resources.Load<Material> ("Materials/redAnim") as Material;
						//					obj.GetComponent<LineRenderer> ().SetWidth (0.25f, 0.25f);
						//materialQueue.Enqueue(obj.GetComponent<LineRenderer>().material.name);
						obj.GetComponent<LineRenderer> ().SetWidth (0.25f, 0.25f);
						obj.GetComponent<LineRenderer> ().material = Resources.Load<Material> ("Materials/redAnim") as Material;
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

					int k = 0;
					//Debug.Log (truckNum + "length of this");
					foreach (List<int> l in Node.blueTruckCap) {
						if (k == truckNum) {
							nodeTrcukCap = l;
							break;
						}
						k++;
					}
					int n1 = 0;
					foreach (float l in Node.blueProfitAl) {
						if (n1 == truckNum) {
							profit = l;
							break;
						}
						n1 ++;
					}
					int n2 = 0;
					foreach (float l in Node.blueTimeAl) {
						if (n2 == theTruckNum) {
							time = l;
							break;
						}
						n2++;
					}
					int[] nodeArr1 = nodePathL.ToArray ();
					int[] nodeTruckArr = nodeTrcukCap.ToArray ();
					for (int j = 0; j < nodeArr1.Length-1; j++) {
						int num1 = nodeArr1 [j];
						int num2 = nodeArr1 [j + 1];
						string pathString = "pathAnim" + num1.ToString () + num2.ToString ();
						string truckText1 = "truckCap" + num1.ToString () + num2.ToString ();
						string truckText2 = "truckCap" + num2.ToString() + num1.ToString ();
						GameObject truckCap = new GameObject ();
						if (GameObject.Find (truckText1) != null) {
							truckCap = GameObject.Find (truckText1);
						} else if (GameObject.Find (truckText2) != null) {
							truckCap = GameObject.Find (truckText2);
						}
						Text t = truckCap.GetComponent<Text> ();
						t.enabled = true;
						t.text = nodeTruckArr [j].ToString ();
						t.GetComponent<Text> ().fontSize = 22;
						//addText.GetComponent<Text> ().font = Resources.GetBuiltinResource (typeof(Font), "Arial.ttf") as Font;
						t.GetComponent<Text> ().font =Resources.Load<Font>("Font/AGENCYR") as Font;
						t.GetComponent<Text> ().fontStyle = FontStyle.Normal;
						t.GetComponent<Text> ().alignment = TextAnchor.MiddleCenter;
						//addText.GetComponent<Text> ().color = new Color (1, 0, 0, 1);
						t.GetComponent<Text> ().fontStyle = FontStyle.Bold;
						string htmlValue = "#3b73e1";
						Color newCol;
						if (ColorUtility.TryParseHtmlString (htmlValue, out newCol)) {
							t.color = newCol;
						}
						GameObject obj = GameObject.Find (pathString);
						//					obj.GetComponent<LineRenderer> ().material = Resources.Load<Material> ("Materials/blueAnim") as Material;
						//					obj.GetComponent<LineRenderer> ().SetWidth (0.25f, 0.25f);
						//materialQueue.Enqueue(obj.GetComponent<LineRenderer>().material.name);
						obj.GetComponent<LineRenderer> ().SetWidth (0.25f, 0.25f);
						obj.GetComponent<LineRenderer> ().material = Resources.Load<Material> ("Materials/blueAnim") as Material;
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

					int k = 0;
					//Debug.Log (truckNum + "length of this");
					foreach (List<int> l in Node.greenTruckCap) {
						if (k == truckNum) {
							nodeTrcukCap = l;
							break;
						}
						k++;
					}
					int n1 = 0;
					foreach (float l in Node.greenProfitAl) {
						if (n1 == truckNum) {
							profit = l;
							break;
						}
						n1 ++;
					}
					int n2 = 0;
					foreach (float l in Node.greenTimeAl) {
						if (n2 == theTruckNum) {
							time = l;
							break;
						}
						n2++;
					}
					int[] nodeArr1 = nodePathL.ToArray ();
					int[] nodeTruckArr = nodeTrcukCap.ToArray ();
					for (int j = 0; j < nodeArr1.Length-1; j++) {
						int num1 = nodeArr1 [j];
						int num2 = nodeArr1 [j + 1];
						string pathString = "pathAnim" + num1.ToString () + num2.ToString ();
						GameObject obj = GameObject.Find (pathString);
						//					obj.GetComponent<LineRenderer> ().material = Resources.Load<Material> ("Materials/greenAnim") as Material;
						//					obj.GetComponent<LineRenderer> ().SetWidth (0.25f, 0.25f);
						//materialQueue.Enqueue(obj.GetComponent<LineRenderer>().material.name);
						string truckText1 = "truckCap" + num1.ToString () + num2.ToString ();
						string truckText2 = "truckCap" + num2.ToString() + num1.ToString ();
						GameObject truckCap = new GameObject ();
						if (GameObject.Find (truckText1) != null) {
							truckCap = GameObject.Find (truckText1);
						} else if (GameObject.Find (truckText2) != null) {
							truckCap = GameObject.Find (truckText2);
						}
						Text t = truckCap.GetComponent<Text> ();
						t.enabled = true;
						t.text = nodeTruckArr [j].ToString ();
						t.GetComponent<Text> ().fontSize = 22;
						//addText.GetComponent<Text> ().font = Resources.GetBuiltinResource (typeof(Font), "Arial.ttf") as Font;
						t.GetComponent<Text> ().font =Resources.Load<Font>("Font/AGENCYR") as Font;
						t.GetComponent<Text> ().fontStyle = FontStyle.Normal;
						t.GetComponent<Text> ().alignment = TextAnchor.MiddleCenter;
						//addText.GetComponent<Text> ().color = new Color (1, 0, 0, 1);
						t.GetComponent<Text> ().fontStyle = FontStyle.Bold;
						string htmlValue = "#33e786";
						Color newCol;
						if (ColorUtility.TryParseHtmlString (htmlValue, out newCol)) {
							t.color = newCol;
						}
						obj.GetComponent<LineRenderer> ().SetWidth (0.25f, 0.25f);
						obj.GetComponent<LineRenderer> ().material = Resources.Load<Material> ("Materials/greenAnim") as Material;
					}
				}

				arrayForChosenPath= nodePathL;
				arrayForTruckCap = nodeTrcukCap;
				numForProfit = profit;
				numForTime = time;
				if (!hasCancelMark) {
					setCancelPathButton (arrayForChosenPath.ToArray());
					hasCancelMark = true;
				}
				Debug.Log ("time in storeTruckCLick" + time);

				//			for (int i = 0; i < nodeArr1.Length-1; i++) {
				//				int num1 = nodeArr1 [i];
				//				int num2 = nodeArr1 [i + 1];
				//				string pathString = "pathAnim" + num1.ToString () + num2.ToString ();
				//				GameObject.Find (pathString).GetComponent<LineRenderer> ().SetWidth (0.25f, 0.25f);
				//			}


			}

			if (numClick % 2 == 0) {
                string truckName = this.gameObject.name;
                string resultString = Regex.Match(truckName, @"\d+").Value;
                int truckNum = int.Parse(resultString);
                if ((truckName[0]=='r' && red) || (truckName[0] == 'g' && green)|| (truckName[0] == 'b' && blue))
                {
                    if (truckName[0] == 'r')
                    {
                        int i = 0;
                        foreach (List<int> l in Node.redAl)
                        {
                            if (i == truckNum)
                            {
                                nodePathL = l;
                                break;
                            }
                            i++;
                        }
                    }
                    if (truckName[0] == 'g')
                    {
                        int i = 0;
                        foreach (List<int> l in Node.greenAl)
                        {
                            if (i == truckNum)
                            {
                                nodePathL = l;
                                break;
                            }
                            i++;
                        }
                    }
                    if (truckName[0] == 'b')
                    {
                        int i = 0;
                        foreach (List<int> l in Node.blueAl)
                        {
                            if (i == truckNum)
                            {
                                nodePathL = l;
                                break;
                            }
                            i++;
                        }
                    }
                    int[] arr = nodePathL.ToArray();
                    for (int j = 0; j < arr.Length - 1; j++)
                    {
                        int num1 = arr[j];
                        int num2 = arr[j + 1];
                        string pathString = "pathAnim" + num1.ToString() + num2.ToString();
                        GameObject obj = GameObject.Find(pathString);
                        //					obj.GetComponent<LineRenderer> ().material = Resources.Load<Material> ("Materials/greenAnim") as Material;
                        //					obj.GetComponent<LineRenderer> ().SetWidth (0.25f, 0.25f);
                        //string materialName=StoreTruckClick.materialQueue.Dequeue();
                        obj.GetComponent<LineRenderer>().SetWidth(0.15f, 0.15f);
                        string truckText1 = "truckCap" + num1.ToString() + num2.ToString();
                        string truckText2 = "truckCap" + num2.ToString() + num1.ToString();
                        GameObject truckCap = new GameObject();
                        if (GameObject.Find(truckText1) != null)
                        {
                            truckCap = GameObject.Find(truckText1);
                        }
                        else if (GameObject.Find(truckText2) != null)
                        {
                            truckCap = GameObject.Find(truckText2);
                        }
                        Text t = truckCap.GetComponent<Text>();
                        t.enabled = false;
                    }
                    if (StoreTruckClick.red)
                    {
                        //string goName = "redTruckImage" +(gameControll.redTruckNum-1).ToString ();
                        //string goText = "redTruckText" + gameControll.redTruckNum.ToString ();
                        StoreTruckClick.red = false;
                    }
                    if (StoreTruckClick.green)
                    {
                        StoreTruckClick.green = false;
                    }
                    if (StoreTruckClick.blue)
                    {
                        StoreTruckClick.blue = false;
                    }
                    Destroy(GameObject.Find("cancelMark"));
                    hasCancelMark = false;
                }
			}
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
			cancelMark.AddComponent<AudioSource> ();
			cancelMark.GetComponent<AudioSource> ().playOnAwake = false;
			cancelMark.GetComponent<AudioSource> ().clip = Resources.Load<AudioClip> ("Audio/trash");
			cancelMark.GetComponent<AudioSource> ().volume = 0.6f;
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
