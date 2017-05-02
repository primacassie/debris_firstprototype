using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SimpleJSON;
using System.Text.RegularExpressions;


public class submitButton : MonoBehaviour {

    public static int submitAndNewScene;  //this var is to tell StartNewTrail that this trail is submit or not.
    public static Dictionary<string,List<int>> animDic = new Dictionary<string,List<int>>();
    public static Dictionary<string, List<float>> animDicForCap = new Dictionary<string, List<float>>();
	private bool submitOnlyOnce;
	private DisplayManager display;
	public static List<List<List<int>>> ForTrailRed = new List<List<List<int>>> ();
	public static List<List<List<int>>> ForTrailGreen = new List<List<List<int>>> ();
	public static List<List<List<int>>> ForTrailBlue=new List<List<List<int>>>();
	public static List<float> forProf = new List<float> ();
	public static List<float> forTime = new List<float> ();
	public static List<int> forInter=new List<int>();
	public static List<List<List<float>>> ForTrailRedPath = new List<List<List<float>>> ();
	public static List<List<List<float>>> ForTrailGreenPath = new List<List<List<float>>> ();
	public static List<List<List<float>>> ForTrailBluePath = new List<List<List<float>>> ();
	//private Vector3 tar=new Vector3();
	//private bool submitOnce;
	//private Transform tar;
	//GameObject prof;
	//GameObject time;
	//GameObject intersection;
	//GameObject profText;
	//GameObject truckTime;
	//GameObject interText;
	float minP;
	float maxT;
	int inters;
	public static List<Dictionary<string,string>> ForPath = new List<Dictionary<string,string>> ();
    public static List<Dictionary<string, string>> ForCap = new List<Dictionary<string, string>>();
	public static Dictionary<string,string> storeLineRenderPath=new Dictionary<string, string>();
    // public static Dictionary<string, string> storeLineCapPath = new Dictionary<string, string>();
    public static int totalTruckNum;

    void Awake()
    {
        totalTruckNum = 0;
        submitAndNewScene = 0;
        animDic= new Dictionary<string, List<int>>();
        animDicForCap = new Dictionary<string, List<float>>();
        storeLineRenderPath=new Dictionary<string, string>();
        if (ForTrailRed.Count == 1)
        {
			DecideWhichMaterial (1);
            GameObject.Find("singleTrail1").GetComponent<Image>().enabled = true;
            GameObject.Find("singleTrail1").GetComponent<BoxCollider2D>().enabled = true;
            Behaviour halo = (Behaviour)GameObject.Find("singleTrail1").GetComponent("Halo");
            halo.enabled = true;
        } else if (ForTrailRed.Count > 1)
        {
//            GameObject.Find("overlappingImage").GetComponent<Image>().enabled = true;
//            GameObject.Find("overlappingImage").GetComponent<BoxCollider2D>().enabled = true;
			GameObject.Find("singleTrail1").GetComponent<Image>().enabled = true;
			GameObject.Find("singleTrail1").GetComponent<BoxCollider2D>().enabled = true;
            DecideWhichMaterial(1);
			for (int i = 2; i <= ForTrailRed.Count; i++) {
				GameObject newObj = Instantiate (Resources.Load ("Prefab/Trail")) as GameObject;
                string newName = "singleTrail" + i;
                newObj.name = newName;
                newObj.tag = "Trails";
                float height = GameObject.Find("singleTrail1").GetComponent<RectTransform>().rect.height;
                Vector2 pos = GameObject.Find("singleTrail1").GetComponent<RectTransform>().anchoredPosition;
                Vector2 newPos = new Vector2(pos.x, pos.y + height + 10 * (i - 1));
                newObj.transform.SetParent(GameObject.Find("singleTrail1").transform.parent.transform, false);
                newObj.GetComponent<RectTransform>().anchoredPosition = newPos;
                newObj.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
                newObj.GetComponent<Image>().enabled = true;
                newObj.GetComponent<BoxCollider2D>().enabled = true;
                DecideWhichMaterial(i);
            }
        }
    }


    void OnMouseDown()
    {
		submit();
    }


    public void submit()
    {
//        int node1 = 0;
//        int node2 = 0;
//        int cap = 0;
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
		if (sum == 0 && !submitOnlyOnce) {
            submitAndNewScene++;
            totalTruckNum = Node.blueAl.Count + Node.redAl.Count + Node.greenAl.Count;

            Debug.Log("totalTruckNum" + totalTruckNum);
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("cap"))
            {
                obj.GetComponentInChildren<Text>().text = "50";
            }
            //Debug.Log ("redAL in sumbit" + Node.redAl.Count);
            //Debug.Log("congratulations! you finish this game!");
            //GameObject.Find("ModalControl").GetComponent<testWindow>().takeAction("Congratulations! You finish this round!");
            GetComponent<AudioSource> ().Play ();
			finalAnimation ();
			submitOnlyOnce = true;
			Button[] button = GameObject.Find ("Buttons").GetComponentsInChildren<Button> ();
			foreach (Button b in button) {
				b.interactable = false;
			}
			ForTrailRed.Add (new List<List<int>>(Node.redAl));
			ForTrailGreen.Add (new List<List<int>> (Node.greenAl));
			ForTrailBlue.Add (new List<List<int>> (Node.blueAl));
			ForTrailRedPath.Add (new List<List<float>> (Node.redTruckCap));
			ForTrailBluePath.Add (new List<List<float>> (Node.blueTruckCap));
			ForTrailGreenPath.Add (new List<List<float>> (Node.greenTruckCap));
			GameObject.Find ("storeTruck").SetActive (false);
            if (GameObject.Find("green") != null)
            {
                minP = Mathf.Min(float.Parse(GameObject.Find("redTruckProfit").GetComponent<Text>().text), float.Parse(GameObject.Find("blueTruckProfit").GetComponent<Text>().text), float.Parse(GameObject.Find("greenTruckProfit").GetComponent<Text>().text));
                maxT = Mathf.Max(float.Parse(GameObject.Find("redTruckTime").GetComponent<Text>().text), float.Parse(GameObject.Find("blueTruckTime").GetComponent<Text>().text), float.Parse(GameObject.Find("greenTruckTime").GetComponent<Text>().text));
            }else
            {
                minP = Mathf.Min(float.Parse(GameObject.Find("redTruckProfit").GetComponent<Text>().text), float.Parse(GameObject.Find("blueTruckProfit").GetComponent<Text>().text));
                maxT = Mathf.Max(float.Parse(GameObject.Find("redTruckTime").GetComponent<Text>().text), float.Parse(GameObject.Find("blueTruckTime").GetComponent<Text>().text));
            }
			inters = gameControll.intersection;
			forProf.Add (minP);
			forTime.Add (maxT);
			forInter.Add (inters);
			foreach(GameObject obj in GameObject.FindGameObjectsWithTag("linerender")){
				string name = obj.name;
                //sometimes players may click sumbit quickly so that there will be a duplicate path, so here I try to avoid "newPathAnim"
                if (name[0] == 'n')
                    continue;
				string resultString = Regex.Match(name, @"\d+").Value;
				int num1 = 0;
				int num2 = 0;
				if (resultString.Length == 2) {
					num1 = resultString [0]-'0';
					num2 = resultString [1]-'0';
				} else if (resultString.Length == 3) {
					num1 = resultString [0]-'0';
					num2 = int.Parse (resultString.Substring (1, 2));
				} else if (resultString.Length == 4) {
					num1 = int.Parse (resultString.Substring (0, 2));
					num2 = int.Parse (resultString.Substring (2, 2));
				}
				string path = "";
				if (Node.redPathArray [num1, num2]) {
					path += "r";
				}
				if (Node.greenPathArray [num1, num2]) {
					path += "g";
				}
				if (Node.bluePathArray [num1, num2]) {
					path += "b";
				}
				storeLineRenderPath.Add (resultString, path);
                //storeLineCapPath.Add(resultString,)
			}
			ForPath.Add (new Dictionary<string, string>(storeLineRenderPath));
            //ForCap.Add(new Dictionary<string, string>(storeLineCapPath));
			if (ForTrailRed.Count == 1) {
				GameObject.Find ("singleTrail1").GetComponent<Image> ().enabled = true;
				GameObject.Find ("singleTrail1").GetComponent<BoxCollider2D> ().enabled = true;
				if (Node.redAl.Count > 0 && Node.greenAl.Count > 0 && Node.blueAl.Count > 0) {
					GameObject.Find ("singleTrail1").GetComponent<Image> ().material = Resources.Load<Material> ("Materials/GradientRGB") as Material;
				} else if (Node.redAl.Count == 0 && Node.greenAl.Count > 0 && Node.blueAl.Count > 0) {
					GameObject.Find ("singleTrail1").GetComponent<Image> ().material = Resources.Load<Material> ("Materials/GradientBG") as Material;
				} else if (Node.redAl.Count > 0 && Node.greenAl.Count > 0 && Node.blueAl.Count == 0) {
					GameObject.Find ("singleTrail1").GetComponent<Image> ().material = Resources.Load<Material> ("Materials/GradientRG") as Material;
				} else if (Node.redAl.Count > 0 && Node.greenAl.Count == 0 && Node.blueAl.Count > 0) {
					GameObject.Find ("singleTrail1").GetComponent<Image> ().material = Resources.Load<Material> ("Materials/GradientRB") as Material;
				} else if (Node.redAl.Count > 0 && Node.greenAl.Count == 0 && Node.blueAl.Count == 0) {
					GameObject.Find ("singleTrail1").GetComponent<Image> ().material = Resources.Load<Material> ("Materials/redAnim") as Material;
				} else if (Node.redAl.Count == 0 && Node.greenAl.Count > 0 && Node.blueAl.Count == 0) {
					GameObject.Find ("singleTrail1").GetComponent<Image> ().material = Resources.Load<Material> ("Materials/greenAnim") as Material;
				}else if(Node.redAl.Count == 0 && Node.greenAl.Count == 0 && Node.blueAl.Count > 0){
					GameObject.Find ("singleTrail1").GetComponent<Image> ().material = Resources.Load<Material> ("Materials/blueAnim") as Material;
				}
				//Debug.Log ("this works");
			} else if (ForTrailRed.Count > 1) {
				//GameObject.Find ("singleTrail1").GetComponent<Image> ().enabled = true;
				//GameObject.Find ("singleTrail1").GetComponent<BoxCollider2D> ().enabled = true;
				//if (Node.redAl.Count > 0 && Node.greenAl.Count > 0 && Node.blueAl.Count > 0) {
				//	GameObject.Find ("singleTrail1").GetComponent<Image> ().material = Resources.Load<Material> ("Materials/GradientRGB") as Material;
				//} else if (Node.redAl.Count == 0 && Node.greenAl.Count > 0 && Node.blueAl.Count > 0) {
				//	GameObject.Find ("singleTrail1").GetComponent<Image> ().material = Resources.Load<Material> ("Materials/GradientBG") as Material;
				//} else if (Node.redAl.Count > 0 && Node.greenAl.Count > 0 && Node.blueAl.Count == 0) {
				//	GameObject.Find ("singleTrail1").GetComponent<Image> ().material = Resources.Load<Material> ("Materials/GradientRG") as Material;
				//} else if (Node.redAl.Count > 0 && Node.greenAl.Count == 0 && Node.blueAl.Count > 0) {
				//	GameObject.Find ("singleTrail1").GetComponent<Image> ().material = Resources.Load<Material> ("Materials/GradientRB") as Material;
				//} else if (Node.redAl.Count > 0 && Node.greenAl.Count == 0 && Node.blueAl.Count == 0) {
				//	GameObject.Find ("singleTrail1").GetComponent<Image> ().material = Resources.Load<Material> ("Materials/redAnim") as Material;
				//} else if (Node.redAl.Count == 0 && Node.greenAl.Count > 0 && Node.blueAl.Count == 0) {
				//	GameObject.Find ("singleTrail1").GetComponent<Image> ().material = Resources.Load<Material> ("Materials/blueAnim") as Material;
				//}else if(Node.redAl.Count == 0 && Node.greenAl.Count == 0 && Node.blueAl.Count > 0){
				//	GameObject.Find ("singleTrail1").GetComponent<Image> ().material = Resources.Load<Material> ("Materials/greenAnim") as Material;
				//}
				GameObject newObj = Instantiate (Resources.Load ("Prefab/Trail"))as GameObject;
				int num = ForTrailRed.Count;
				string newName = "singleTrail" + num;
				newObj.name = newName;
				newObj.tag = "Trails";
                float height = GameObject.Find("singleTrail1").GetComponent<RectTransform>().rect.height;
                Vector2 pos= GameObject.Find("singleTrail1").GetComponent<RectTransform>().anchoredPosition;
                Vector2 newPos = new Vector2(pos.x, pos.y - height - 10*(num-1));
				newObj.transform.SetParent (GameObject.Find ("singleTrail1").transform.parent.transform,false);
                newObj.GetComponent<RectTransform>().anchoredPosition = newPos;
                newObj.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
                newObj.GetComponent<Image>().enabled = true;
                newObj.GetComponent<BoxCollider2D>().enabled = true;
                DecideWhichMaterial (num);
//				GameObject.Find ("singleTrail").GetComponent<Image> ().enabled = false;
//				GameObject.Find ("singleTrail").GetComponent<BoxCollider2D> ().enabled = false;
//				GameObject.Find ("overlappingImage").GetComponent<Image> ().enabled = true;
//				GameObject.Find ("overlappingImage").GetComponent<BoxCollider2D> ().enabled = true;
				//Debug.Log ("this works 2");
			}
			JSONClass details = new JSONClass ();
			string s = "Min Profit: " + minP + "," + "Max Time: " + maxT + "," + "Intersection: " + inters;
			details ["ClickSubmit"] = s;
			TheLogger.instance.TakeAction (8, details);
			//StartCoroutine (wait1 ());
		} else if (sum == 0 && submitOnlyOnce) {
			display=DisplayManager.Instance();
			display.DisplayMessage ("You've already sumbitted!");
		}
        else
        {
            //string temp = "there are still " + cap + " debris in the path " + node1 + node2 + ", please clean it!";
			JSONClass details = new JSONClass ();
			details ["Incorrect Operation"] = "Submit before cleaning all debris";
			TheLogger.instance.TakeAction (10, details);
			display=DisplayManager.Instance();
			display.DisplayMessage ("Pls Clean All Debris Before Sumbit!");
            //Debug.Log("there are still " + cap + " debris in the path " + node1 + node2 + ", please clean it!");
            //GameObject.Find("ModalControl").GetComponent<testWindow>().takeAction(temp);
        }
    }

	void DecideWhichMaterial(int num){
		string name = "singleTrail" + num;
		GameObject obj = GameObject.Find (name);
        if (ForTrailRed[num - 1].Count != 0 && ForTrailBlue[num - 1].Count != 0 && ForTrailGreen[num - 1].Count != 0)
        {
            obj.GetComponent<Image>().material = Resources.Load<Material>("Materials/GradientRGB") as Material;
        }
        else if (ForTrailRed[num - 1].Count == 0 && ForTrailBlue[num - 1].Count != 0 && ForTrailGreen[num - 1].Count == 0)
        {
            obj.GetComponent<Image>().material = Resources.Load<Material>("Materials/blueAnim") as Material;
        }
        else if (ForTrailRed[num - 1].Count == 0 && ForTrailBlue[num - 1].Count != 0 && ForTrailGreen[num - 1].Count != 0)
        {
            obj.GetComponent<Image>().material = Resources.Load<Material>("Materials/GradientBG") as Material;
        }
        else if (ForTrailRed[num - 1].Count != 0 && ForTrailBlue[num - 1].Count == 0 && ForTrailGreen[num - 1].Count != 0)
        {
            obj.GetComponent<Image>().material = Resources.Load<Material>("Materials/GradientRG") as Material;
        }
        else if (ForTrailRed[num - 1].Count != 0 && ForTrailBlue[num - 1].Count != 0 && ForTrailGreen[num - 1].Count == 0)
        {
            obj.GetComponent<Image>().material = Resources.Load<Material>("Materials/GradientRB") as Material;
        }
        else if (ForTrailRed[num - 1].Count == 0 && ForTrailBlue[num - 1].Count == 0 && ForTrailGreen[num - 1].Count != 0)
        {
            obj.GetComponent<Image>().material = Resources.Load<Material>("Materials/greenAnim") as Material;
        }
        else if (ForTrailRed[num - 1].Count != 0 && ForTrailBlue[num - 1].Count == 0 && ForTrailGreen[num - 1].Count == 0)
        {
            obj.GetComponent<Image>().material = Resources.Load<Material>("Materials/redAnim") as Material;
        }
    }

    //IEnumerator wait1()
    //{
    //    yield return new WaitForSeconds(10);
    //    //BlackHoleAnim();
    //}

	//IEnumerator wait(){
	//	yield return new WaitForSeconds (1);
	//	if (prof.activeSelf == false) {
	//		prof.SetActive (true);
	//		time.SetActive (true);
	//		intersection.SetActive (true);
	//		profText.SetActive (true);
	//		truckTime.SetActive (true);
	//		interText.SetActive (true);
	//	}
	//	Transform tarProf = GameObject.Find ("ForFinalProfit").transform;
	//	Transform tarTime = GameObject.Find ("ForFinalTime").transform;
	//	Transform tarIntersection = GameObject.Find ("ForFinalIntersection").transform;
	//	Transform tarProfText=GameObject.Find ("ForFinalProfitText").transform;
	//	Transform tarTimeText = GameObject.Find ("ForFinalTimeText").transform;
	//	Transform tarIntersectionText = GameObject.Find ("ForFinalIntersectionText").transform;
	//	prof.transform.position = Vector3.MoveTowards (prof.transform.position, tarProf.position, 3 * Time.deltaTime);
	//	time.transform.position = Vector3.MoveTowards (time.transform.position, tarTime.position, 3 * Time.deltaTime);
	//	intersection.transform.position = Vector3.MoveTowards (intersection.transform.position, tarIntersection.position, 3 * Time.deltaTime);
	//	profText.transform.position = Vector3.MoveTowards (profText.transform.position, tarProfText.position, 3 * Time.deltaTime);
	//	truckTime.transform.position = Vector3.MoveTowards (truckTime.transform.position, tarTimeText.position, 3 * Time.deltaTime);
	//	interText.transform.position = Vector3.MoveTowards (interText.transform.position, tarIntersectionText.position, 3 * Time.deltaTime);
	//	if (truckTime.transform.position == tarTimeText.transform.position) {
	//		//Application.targetFrameRate = 1;
	//		while (float.Parse (profText.GetComponent<Text> ().text) < minP) {
	//			float t = float.Parse (profText.GetComponent<Text> ().text);
	//			t += 1;
	//			if (Mathf.Abs (minP - t) <= 10) {
	//				t = minP;
	//			}
	//			profText.GetComponent<Text> ().text = t.ToString ();
	//		}
	//		while (float.Parse (truckTime.GetComponent<Text> ().text) < maxT) {
	//			float t = float.Parse (truckTime.GetComponent<Text> ().text);
	//			t += 1;
	//			if (Mathf.Abs (maxT - t) <= 10) {
	//				t = maxT;
	//			}
	//			truckTime.GetComponent<Text> ().text = t.ToString ();
	//		}
	//		while (float.Parse (interText.GetComponent<Text> ().text) < inters) {
	//			float t = float.Parse (interText.GetComponent<Text> ().text);
	//			t += 1;
	//			interText.GetComponent<Text> ().text = t.ToString ();
	//		}

	//	}
	//}

	//void BlackHoleAnim(){
	//	Camera.main.GetComponent<stickMap> ().enabled = true;
 //       foreach(GameObject obj in GameObject.FindGameObjectsWithTag("linerender"))
 //       {
 //           Destroy(obj);
 //       }
	//	foreach (GameObject obj in GameObject.FindGameObjectsWithTag("indi")) {
	//		Destroy (obj);
	//	}
	//	Destroy (GameObject.Find ("Toggle"));
	//	prof.transform.SetParent (GameObject.Find ("gamePanel").transform);
	//	prof.transform.position = GameObject.Find ("depot").transform.position;
	//	time.transform.SetParent (GameObject.Find ("gamePanel").transform);
	//	time.transform.position = GameObject.Find ("depot").transform.position;
	//	intersection.transform.SetParent (GameObject.Find ("gamePanel").transform);
	//	intersection.transform.position = GameObject.Find ("depot").transform.position;
	//	if(prof.GetComponent<Image>()==null)
	//		prof.AddComponent<Image> ();
	//	prof.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Image/profit") as Sprite;
	//	prof.GetComponent<RectTransform> ().localScale = new Vector2 (1.3f, 1.3f);
	//	prof.GetComponent<RectTransform> ().sizeDelta = new Vector2 (80f, 80f);
	//	if(time.GetComponent<Image>()==null)
	//		time.AddComponent<Image> ();
	//	time.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Image/time") as Sprite;
	//	time.GetComponent<RectTransform> ().localScale = new Vector2 (1.3f, 1.3f);
	//	time.GetComponent<RectTransform> ().sizeDelta = new Vector2 (80f, 80f);
	//	if(intersection.GetComponent<Image>()==null)
	//		intersection.AddComponent<Image> ();
	//	intersection.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Image/overlaping") as Sprite;
	//	intersection.GetComponent<RectTransform> ().localScale = new Vector2 (1.3f, 1.3f);
	//	intersection.GetComponent<RectTransform> ().sizeDelta = new Vector2 (80f, 80f);
	//	profText.transform.SetParent (GameObject.Find ("gamePanel").transform);
	//	profText.transform.position = GameObject.Find ("depot").transform.position;
	//	if(profText.GetComponent<Text>()==null)
	//		profText.AddComponent<Text> ();
	//	profText.GetComponent<Text> ().text = "0";
	//	profText.GetComponent<RectTransform> ().localScale = new Vector2 (1.3f, 1.3f);
	//	profText.GetComponent<Text> ().fontSize = 40;
	//	//addText.GetComponent<Text> ().font = Resources.GetBuiltinResource (typeof(Font), "Arial.ttf") as Font;
	//	profText.GetComponent<Text> ().font =Resources.Load<Font>("Font/AGENCYR") as Font;
	//	profText.GetComponent<Text> ().fontStyle = FontStyle.Normal;
	//	profText.GetComponent<Text> ().alignment = TextAnchor.MiddleCenter;
	//	//addText.GetComponent<Text> ().color = new Color (1, 0, 0, 1);
	//	profText.GetComponent<Text> ().fontStyle = FontStyle.Bold;
	//	truckTime.transform.SetParent (GameObject.Find ("gamePanel").transform);
	//	truckTime.transform.position = GameObject.Find ("depot").transform.position;
	//	if(truckTime.GetComponent<Text>()==null)
	//		truckTime.AddComponent<Text> ();
	//	truckTime.GetComponent<Text> ().text = "0";
	//	truckTime.GetComponent<RectTransform> ().localScale = new Vector2 (1.3f, 1.3f);
	//	truckTime.GetComponent<Text> ().fontSize = 40;
	//	//addText.GetComponent<Text> ().font = Resources.GetBuiltinResource (typeof(Font), "Arial.ttf") as Font;
	//	truckTime.GetComponent<Text> ().font =Resources.Load<Font>("Font/AGENCYR") as Font;
	//	truckTime.GetComponent<Text> ().fontStyle = FontStyle.Normal;
	//	truckTime.GetComponent<Text> ().alignment = TextAnchor.MiddleCenter;
	//	//addText.GetComponent<Text> ().color = new Color (1, 0, 0, 1);
	//	truckTime.GetComponent<Text> ().fontStyle = FontStyle.Bold;
	//	interText.transform.SetParent (GameObject.Find ("gamePanel").transform);
	//	interText.transform.position = GameObject.Find ("depot").transform.position;
	//	if(interText.GetComponent<Text>()==null)
	//		interText.AddComponent<Text> ();
	//	interText.GetComponent<Text> ().text = "0";
	//	interText.GetComponent<RectTransform> ().localScale = new Vector2 (1.3f, 1.3f);
	//	interText.GetComponent<Text> ().fontSize = 40;
	//	//addText.GetComponent<Text> ().font = Resources.GetBuiltinResource (typeof(Font), "Arial.ttf") as Font;
	//	interText.GetComponent<Text> ().font =Resources.Load<Font>("Font/AGENCYR") as Font;
	//	interText.GetComponent<Text> ().fontStyle = FontStyle.Normal;
	//	interText.GetComponent<Text> ().alignment = TextAnchor.MiddleCenter;
	//	//addText.GetComponent<Text> ().color = new Color (1, 0, 0, 1);
	//	interText.GetComponent<Text> ().fontStyle = FontStyle.Bold;
	//	prof.SetActive (false);
	//	time.SetActive (false);
	//	intersection.SetActive (false);
	//	profText.SetActive (false);
	//	truckTime.SetActive (false);
	//	interText.SetActive (false);
 //       foreach (GameObject obj in GameObject.FindGameObjectsWithTag("path"))
 //       {
 //           Destroy(obj);
 //       }
 //       foreach (GameObject obj in GameObject.FindGameObjectsWithTag("cap")){
	//		Destroy (obj);
	//	}
	//	foreach (GameObject obj in GameObject.FindGameObjectsWithTag("finalTruck")) {
	//		Destroy (obj);
	//	}
 //       Destroy(GameObject.Find("miniMap"));
	//}

//	private void NodeMove(){
//        bool start = false;
//		foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Node")) {
//			obj.transform.position = Vector3.MoveTowards (obj.transform.position, tar.position, 3 * Time.deltaTime);
//			if (obj.transform.position == tar.position) {
//				Color c = obj.GetComponent<Image> ().color;
//				c.a = 0;
//				obj.GetComponent<Image> ().color = c;
//			}
//		}
//        GameObject.Find("leftPanel").transform.position = Vector3.MoveTowards(GameObject.Find("leftPanel").transform.position, tar.position, 3 * Time.deltaTime);
//        if (GameObject.Find("leftPanel").transform.position == tar.position)
//        {
//            start = true;
//            Color c = GameObject.Find("leftPanel").GetComponent<Image>().color;
//            c.a = 0;
////            GameObject.Find("leftPanel").GetComponent<Image>().color = c;
//			foreach(GameObject obj in GameObject.FindGameObjectsWithTag("leftPanel")){
//				if (obj.GetComponent<Image> () != null) {
//					obj.GetComponent<Image> ().color = c;
//				} else if (obj.GetComponent<Text> () != null) {
//					obj.GetComponent<Text> ().color = c;
//				}
//			}
//        }
//        if (start)
//        {
////            GameObject prof=GameObject.Find("redProfit");
////            GameObject time = GameObject.Find("redtime");
////			GameObject intersection = GameObject.Find ("intersection");
////			GameObject profText = GameObject.Find ("redTruckProfit");
////			GameObject timeText = GameObject.Find ("redTruckTime");
////			Color c = prof.GetComponent<Image> ().color;
////			c.a = 1;
////			prof.GetComponent<Image> ().color = c;
////			time.GetComponent<Image> ().color = c;
////			intersection.GetComponent<Text> ().color = c;
////			profText.GetComponent<Text> ().color = c;
////			timeText.GetComponent<Text> ().color = c;
////			prof.transform.localScale = new Vector2 (3, 3);
////			time.transform.localScale = new Vector2 (3, 3);
////			intersection.transform.localScale = new Vector2 (3, 3);
////			profText.transform.localScale = new Vector2 (3, 3);
////			timeText.transform.localScale = new Vector2 (3, 3);
//			StartCoroutine (wait ());
//        }
        
//    }

	//void Update(){
 //       //int i = 0;
 //       //int j = 0;
 //       //foreach(GameObject obj in GameObject.FindGameObjectsWithTag("finalTruck"))
 //       //{
 //       //    i++;
 //       //    if (obj.transform.position == GameObject.Find("depot").transform.position)
 //       //    {
 //       //        j++;
 //       //    }
 //       //    if (i == j)
 //       //    {
 //       //        BlackHoleAnim();
 //       //    }
 //       //}
	//	if (Camera.main.GetComponent<stickMap> ().enabled == true) {
	//		NodeMove ();
	//	}
		
	//}

//	IEnumerator waitCreate(){
//		yield return new WaitForSeconds (1);
//	}

	public void finalAnimation()
	{
		int r = 0;
		foreach(List<int> l in Node.redAl)
		{
			GameObject go = new GameObject();
			go.transform.SetParent(GameObject.Find("Canvas").transform);
			go.transform.position=GameObject.Find("depot").transform.position;
			go.transform.localScale = new Vector2 (0.3f, 0.6f);
			go.tag = "finalTruck";
			//go.AddComponent<Image>();
			//go.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/truck_R32") as Sprite;
			go.AddComponent<submitAnim>();
			Image addIm = go.AddComponent<Image> ();
            GameObject textForCap = new GameObject();
            //add text in the truck to display how many debris the truck has
            textForCap.transform.SetParent(go.transform, false);
			textForCap.transform.localScale = new Vector2 (1, 1);
            Text thisText=textForCap.AddComponent<Text>();
            thisText.text = "0";
            thisText.transform.localPosition = go.transform.position;
            thisText.fontSize = 30;
            thisText.resizeTextForBestFit = true;
            thisText.font = Resources.Load<Font>("Font/AGENCYR") as Font;
            thisText.fontStyle = FontStyle.Normal;
            thisText.alignment = TextAnchor.MiddleCenter;
            thisText.fontStyle = FontStyle.Normal;
            string name = "redTruckText";
            textForCap.name = name;
            string htmlValue = "#db4f69";
			Color newCol;
			if (ColorUtility.TryParseHtmlString (htmlValue, out newCol)) {
				addIm.GetComponent<Image> ().color = newCol;
			}
            string htmlValueWhite = "#BDD3F8FF";
            if (ColorUtility.TryParseHtmlString(htmlValueWhite, out newCol))
            {
                thisText.color = newCol;
            }
            //go.AddComponent<Transform>();
            string truckName = "red" + r.ToString();
			go.name = truckName;
			animDic.Add(truckName, l);
            //if(r<Node.redTruckCap.Count)
            animDicForCap.Add(truckName, Node.redTruckCap[r]);
			r++;
			//			StartCoroutine (waitCreate());
			//yield return new WaitForSeconds(1);
			//r++;
		}
		//Debug.Log("r number "+r);
		int b = 0;
		foreach (List<int> l in Node.blueAl)
		{
			GameObject go = new GameObject();
			go.tag = "finalTruck";
			go.transform.SetParent(GameObject.Find("Canvas").transform);
			go.transform.position=GameObject.Find("depot").transform.position;
			go.transform.localScale = new Vector2 (0.3f, 0.6f);
			//go.AddComponent<Transform>();
			//go.AddComponent<Image>();
			//go.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/truck_B32") as Sprite;
			go.AddComponent<submitAnim>();
            GameObject textForCap = new GameObject();
            //add text in the truck to display how many debris the truck has
            textForCap.transform.SetParent(go.transform, false);
			textForCap.transform.localScale = new Vector2 (1, 1);
            Text thisText = textForCap.AddComponent<Text>();
            string name = "blueTruckText";
            textForCap.name = name;
            thisText.text = "0";
            thisText.transform.localPosition = go.transform.position;
            thisText.fontSize = 30;
            thisText.resizeTextForBestFit = true;
            thisText.font = Resources.Load<Font>("Font/AGENCYR") as Font;
            thisText.fontStyle = FontStyle.Normal;
            thisText.alignment = TextAnchor.MiddleCenter;
            thisText.fontStyle = FontStyle.Normal;
            string htmlValueWhite = "#BDD3F8FF";
            Image addIm = go.AddComponent<Image> ();
			string htmlValue = "#3b73e1";
			Color newCol;
			if (ColorUtility.TryParseHtmlString (htmlValue, out newCol)) {
				addIm.GetComponent<Image> ().color = newCol;
			}
            if (ColorUtility.TryParseHtmlString(htmlValueWhite, out newCol))
            {
                thisText.color = newCol;
            }
            string truckName = "blue" + b.ToString();
			go.name = truckName;
			animDic.Add(truckName, l);
            //if(b<Node.blueTruckCap.Count)
            animDicForCap.Add(truckName, Node.blueTruckCap[b]);
			b++;
			//			StartCoroutine (waitCreate());
		}
		int g = 0;
		foreach (List<int> l in Node.greenAl)
		{
			GameObject go = new GameObject();
			go.tag = "finalTruck";
			go.transform.SetParent(GameObject.Find("Canvas").transform);
			go.transform.position=GameObject.Find("depot").transform.position;
			go.transform.localScale = new Vector2 (0.3f, 0.6f);
			//go.AddComponent<Image>();
			//go.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/truck_G32") as Sprite;
			go.AddComponent<submitAnim>();
			Image addIm = go.AddComponent<Image> ();
			string htmlValue = "#33e786";
			Color newCol;
            if (ColorUtility.TryParseHtmlString (htmlValue, out newCol)) {
				addIm.GetComponent<Image> ().color = newCol;
			}
            GameObject textForCap = new GameObject();
            //add text in the truck to display how many debris the truck has
            textForCap.transform.SetParent(go.transform, false);
            Text thisText = textForCap.AddComponent<Text>();
			textForCap.transform.localScale = new Vector2 (1, 1);
            thisText.text = "0";
            thisText.transform.localPosition = go.transform.position;
            thisText.fontSize = 30;
            thisText.resizeTextForBestFit = true;
            thisText.font = Resources.Load<Font>("Font/AGENCYR") as Font;
            thisText.fontStyle = FontStyle.Normal;
            thisText.alignment = TextAnchor.MiddleCenter;
            thisText.fontStyle = FontStyle.Normal;
            string htmlValueWhite = "#BDD3F8FF";
            string name = "greenTruckText";
            textForCap.name = name;
            if (ColorUtility.TryParseHtmlString(htmlValueWhite, out newCol))
            {
                thisText.color = newCol;
            }
            //go.AddComponent<Transform>();
            string truckName = "green" + g.ToString();
			go.name = truckName;
			animDic.Add(truckName, l);
            //if(g<Node.greenTruckCap.Count)
            animDicForCap.Add(truckName, Node.greenTruckCap[g]);
            g++;
			//			StartCoroutine (waitCreate());
		}
	}
}
