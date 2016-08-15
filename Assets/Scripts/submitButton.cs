using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class submitButton : MonoBehaviour {


    public static Dictionary<string,List<int>> animDic = new Dictionary<string,List<int>>();
	private bool submitOnlyOnce;
	private DisplayManager display;
	//private Vector3 tar=new Vector3();
	//private bool submitOnce;
	private Transform tar;

 //   public float speed = 4f;

 //   private int current = 0;
 //   private int[] arr;
 //   Transform target;
 //   Transform[] tArr;
 //   private bool canUpdate;

 //   // Use this for initialization
    void Start () {
		tar=GameObject.Find ("depot").transform;
		//tar=new Vector3(-6,0,0);
	}
	
	//// Update is called once per frame
	//void Update () {
 //       //if (currentWayPoint < this.wayPointList.Length)
 //       //{
 //       //    if (targetWayPoint == null)
 //       //        targetWayPoint = wayPointList[currentWayPoint];
 //       //    finalAnimation();
 //       //}
 //       if (canUpdate)
 //       {
 //           if (current < arr.Length)
 //           {
 //               if (target == null)
 //                   target = tArr[current];
 //               finalAnimation();
 //           }
 //       }
 //   }

    void OnMouseDown()
    {
        //int redLength = Node.redAl.Count;
        //int blueLength = Node.blueAl.Count;
        //int greenLength = Node.greenAl.Count;
        //int[] redTimeArr = new int[redLength];
        //int[] blueTimeArr = new int[blueLength];
        //int[] greenTimeArr = new int[blueLength];
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
			//Debug.Log("congratulations! you finish this game!");
			//GameObject.Find("ModalControl").GetComponent<testWindow>().takeAction("Congratulations! You finish this round!");
			GetComponent<AudioSource> ().Play ();
			finalAnimation ();
			submitOnlyOnce = true;
			Button[] button = GameObject.Find ("Buttons").GetComponentsInChildren<Button> ();
			foreach (Button b in button) {
				b.interactable = false;
			}
			GameObject.Find ("storeTruck").SetActive (false);
//			foreach (GameObject obj in GameObject.FindGameObjectsWithTag ("Node")) {
//				Color c = obj.GetComponent<Image> ().color;
//				c.a = 0.6f;
//				obj.GetComponent<Image> ().color=c;
//			}
//			foreach (GameObject obj in GameObject.FindGameObjectsWithTag ("path")) {
//				Color c = obj.GetComponent<Image> ().color;
//				c.a = 0.6f;
//				obj.GetComponent<Image> ().color=c;
//			}
			StartCoroutine (wait1 ());
		} else if (sum == 0 && submitOnlyOnce) {
			display=DisplayManager.Instance();
			display.DisplayMessage ("You've already sumbitted!");
		}
        else
        {
            //string temp = "there are still " + cap + " debris in the path " + node1 + node2 + ", please clean it!";
			display=DisplayManager.Instance();
			display.DisplayMessage ("Pls Clean All Debris Before Sumbit!");
            //Debug.Log("there are still " + cap + " debris in the path " + node1 + node2 + ", please clean it!");
            //GameObject.Find("ModalControl").GetComponent<testWindow>().takeAction(temp);
        }
    }

    IEnumerator wait1()
    {
        yield return new WaitForSeconds(12);
        BlackHoleAnim();
    }

	IEnumerator wait(){
		yield return new WaitForSeconds (10);
		SceneManager.LoadScene ("end");
	}

	void BlackHoleAnim(){
		Camera.main.GetComponent<stickMap> ().enabled = true;
        //foreach(GameObject obj in GameObject.FindGameObjectsWithTag("linerender"))
        //{
        //    Destroy(obj);
        //}
        //foreach (GameObject obj in GameObject.FindGameObjectsWithTag("path"))
        //{
        //    Destroy(obj);
        //}
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("cap")){
			Destroy (obj);
		}
		foreach (GameObject obj in GameObject.FindGameObjectsWithTag("finalTruck")) {
			Destroy (obj);
		}
        Destroy(GameObject.Find("miniMap"));
	}

	private void NodeMove(){
        bool start = false;
		foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Node")) {
			obj.transform.position = Vector3.MoveTowards (obj.transform.position, tar.position, 3 * Time.deltaTime);
			if (obj.transform.position == tar.position) {
				Color c = obj.GetComponent<Image> ().color;
				c.a = 0;
				obj.GetComponent<Image> ().color = c;
			}
		}
        GameObject.Find("leftPanel").transform.position = Vector3.MoveTowards(GameObject.Find("leftPanel").transform.position, tar.position, 3 * Time.deltaTime);
        if (GameObject.Find("leftPanel").transform.position == tar.position)
        {
            start = true;
            Color c = GameObject.Find("leftPanel").GetComponent<Image>().color;
            c.a = 0;
            GameObject.Find("leftPanel").GetComponent<Image>().color = c;
        }
        if (start)
        {
            float minP = Mathf.Min(int.Parse(GameObject.Find("redTruckProfit").GetComponent<Text>().text), int.Parse(GameObject.Find("blueTruckProfit").GetComponent<Text>().text), int.Parse(GameObject.Find("greenTruckProfit").GetComponent<Text>().text));
            float maxT = Mathf.Max(int.Parse(GameObject.Find("redTruckTime").GetComponent<Text>().text), int.Parse(GameObject.Find("blueTruckTime").GetComponent<Text>().text), int.Parse(GameObject.Find("greenTruckTime").GetComponent<Text>().text));
            int inters = Node.intersection;
            GameObject prof=GameObject.Find("redProfit");
            GameObject time = GameObject.Find("redTime");
            
        }
        
    }

	void Update(){
        //int i = 0;
        //int j = 0;
        //foreach(GameObject obj in GameObject.FindGameObjectsWithTag("finalTruck"))
        //{
        //    i++;
        //    if (obj.transform.position == GameObject.Find("depot").transform.position)
        //    {
        //        j++;
        //    }
        //    if (i == j)
        //    {
        //        BlackHoleAnim();
        //    }
        //}
		if (Camera.main.GetComponent<stickMap> ().enabled == true) {
			NodeMove ();
		}
		
	}

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
			string htmlValue = "#db4f69";
			Color newCol;
			if (ColorUtility.TryParseHtmlString (htmlValue, out newCol)) {
				addIm.GetComponent<Image> ().color = newCol;
			}
			//go.AddComponent<Transform>();
			string truckName = "red" + r.ToString();
			go.name = truckName;
			animDic.Add(truckName, l);
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
			Image addIm = go.AddComponent<Image> ();
			string htmlValue = "#3b73e1";
			Color newCol;
			if (ColorUtility.TryParseHtmlString (htmlValue, out newCol)) {
				addIm.GetComponent<Image> ().color = newCol;
			}
			string truckName = "blue" + b.ToString();
			go.name = truckName;
			animDic.Add(truckName, l);
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
			//go.AddComponent<Transform>();
			string truckName = "green" + g.ToString();
			go.name = truckName;
			animDic.Add(truckName, l);
			g++;
			//			StartCoroutine (waitCreate());
		}
	}
}
