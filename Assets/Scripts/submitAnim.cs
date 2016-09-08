using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class submitAnim : MonoBehaviour {

    public static int numForFinshTruck;
    private bool updateOnlyOnce1;
    private bool updateOnlyOnce;
	private Transform[] wayPointList;
    private int[] arr;
    private int[] arrC;
    private int cap;
    private int count;
    private int counter;
    private GameObject startNew;
    private GameObject finalSub;

    private int currentWayPoint = 0;
	Transform targetWayPoint;
	//Vector2 targetVector;

    public float speed = 5f;
    // Use this for initialization

    void Awake()
    {
        numForFinshTruck = 0;
        startNew = GameObject.Find("StartNew");
        finalSub = GameObject.Find("FinalSubmit");
    }

    void Start () {
        counter = 0;
        count = currentWayPoint - 1;
        string objName = this.gameObject.name;
        //Debug.Log(this.gameObject.name);
        if (submitButton.animDic.ContainsKey(objName))
        {
            List<int> l=submitButton.animDic[objName];
            List<int> c = submitButton.animDicForCap[objName];
            arr = l.ToArray();
            arrC = c.ToArray();
            //Debug.Log(this.gameObject.name+" "+ arr.Length);
            //Debug.Log(this.gameObject.name + " " + arrC.Length);
			wayPointList = new Transform[arr.Length];
			//wayPointVector = new Vector2[arr.Length];
            for(int i = 0; i < arr.Length; i++)
            {
                string nodeName = "node" + arr[i].ToString();
                //Debug.Log(nodeName);
                if (arr[i] == 1)
                {
                    nodeName = "depot";
                }
				wayPointList [i] = GameObject.Find (nodeName).transform;
				//wayPointVector [i] = new Vector3(GameObject.Find (nodeName).transform.position.x,GameObject.Find (nodeName).transform.position.y,0);
				//wayPointVector[i]=GameObject.Find(nodeName).transform.position;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (currentWayPoint < this.wayPointList.Length)
        {
            if (targetWayPoint == null)
                targetWayPoint = wayPointList[currentWayPoint];
            walk();

        }
        if (currentWayPoint == this.wayPointList.Length-1 && !updateOnlyOnce && transform.position==targetWayPoint.position)
        {
            numForFinshTruck++;
            Debug.Log(this.gameObject.name + " " + numForFinshTruck);
            updateOnlyOnce = true;
        }
        if (numForFinshTruck == submitButton.totalTruckNum && !updateOnlyOnce1)
        {
            finalSub.GetComponent<BoxCollider2D>().enabled = true;
            startNew.GetComponent<BoxCollider2D>().enabled = true;
            finalSub.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            startNew.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            updateOnlyOnce1 = true;
            if (StartNewTrail.numOfTrail == 5)
            {
                startNew.GetComponent<BoxCollider2D>().enabled = false;
                startNew.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
            }
        }
    }

    void walk()
    {
//		if (currentWayPoint < wayPointList.Length-1) {
//			float slope = (targetWayPoint.position.y - transform.position.y) / (targetWayPoint.position.x - transform.position.x);
//			Vector3 dir= new Vector3 (0, 0, Mathf.Atan (slope) * Mathf.Rad2Deg);
//			if (transform.position.x < targetWayPoint.position.x)
//			{
//				dir = new Vector3 (0, 0, (Mathf.Atan (slope) * Mathf.Rad2Deg - 180f+90)%180);
//			}else
//			{
//				dir = new Vector3 (0, 0, (Mathf.Atan (slope) * Mathf.Rad2Deg+90)%180);
//			}
//			//Debug.Log (dir.z);
////			if (Mathf.Abs(Mathf.Abs(transform.eulerAngles.z)-Mathf.Abs(dir.z)) >1) {
////				//Debug.Log ("dir.z "+dir.z);
////				//Debug.Log ("transform.z "+transform.eulerAngles.z);
////				transform.Rotate(new Vector3(0,0,1));
////			}
////			if (Mathf.Abs(Mathf.Abs(transform.eulerAngles.z)-Mathf.Abs(dir.z)) <= 1) {
////				//Debug.Log ("reach inside");
////				Debug.Log(this.gameObject.name+" " + dir.z);
////				transform.position = Vector3.MoveTowards (transform.position, targetWayPoint.position, speed * Time.deltaTime);
////			} 
//			if (Mathf.Abs((transform.eulerAngles.z-180)-dir.z) >1) {
//				//Debug.Log ("dir.z "+dir.z);
//				//Debug.Log ("transform.z "+transform.eulerAngles.z);
//				//Debug.Log(this.gameObject.name+" " + transform.eulerAngles.z);
//				transform.Rotate(new Vector3(0,0,1));
//			}
//			if (Mathf.Abs((transform.eulerAngles.z-180)-dir.z) <= 1) {
//				//Debug.Log ("reach inside");
//				transform.position = Vector3.MoveTowards (transform.position, targetWayPoint.position, speed * Time.deltaTime);
//			} 
//		}else {
//			transform.position = Vector3.MoveTowards (transform.position, targetWayPoint.position, speed * Time.deltaTime);
//		}
		float slope = (targetWayPoint.position.y - transform.position.y) / (targetWayPoint.position.x - transform.position.x);
		Vector3 dir= new Vector3 (0, 0, Mathf.Atan (slope) * Mathf.Rad2Deg);
		if (transform.position.x < targetWayPoint.position.x)
		{
			dir = new Vector3 (0, 0, (Mathf.Atan (slope) * Mathf.Rad2Deg+90)%180);
		}else
		{
			dir = new Vector3 (0, 0, (Mathf.Atan (slope) * Mathf.Rad2Deg+90+180)%180);
		}
		//Debug.Log (dir.z);

		if (Mathf.Abs((transform.eulerAngles.z-180)-dir.z) >5) {
			//Debug.Log ("dir.z "+dir.z);
			//Debug.Log ("transform.z "+transform.eulerAngles.z);
			//Debug.Log(this.gameObject.name+" " + transform.eulerAngles.z);
			transform.Rotate(new Vector3(0,0,5));
		}
        //Vector3 originPos = new Vector3();
        //Vector3 targetPos = new Vector3();

        if (count >= 0)
        {
            //originPos = wayPointList[count].position;
            //targetPos = wayPointList[count + 1].position;
            cap = arrC[count];
        }

        if (Mathf.Abs((transform.eulerAngles.z-180)-dir.z) <= 5) {
			//Debug.Log ("reach inside");
			transform.position = Vector3.MoveTowards (transform.position, targetWayPoint.position, speed * Time.deltaTime);
            Text textNumForCap = gameControll.gm[arr[count], arr[count + 1]].GetComponentInChildren<Text>();
            int numForCap = int.Parse(textNumForCap.text);
			int numFOrTruckCap =int.Parse(this.gameObject.GetComponentInChildren<Text> ().text);
            //Debug.Log("numforCap in anim"+numForCap);

            //cannot use while in update
            if (numForCap > 0 && counter < cap)
            {
                //if (Mathf.Abs(transform.position.x - originPos.x) % unitDist == 0)
                //{
                //    numForCap--;
                //    Debug.Log(numForCap);
                //    textNumForCap.text = numForCap.ToString();
                //}
                numForCap--;
                counter++;
				numFOrTruckCap++;
				if (numFOrTruckCap > 100)
					numFOrTruckCap = 100;

                //Debug.Log(numForCap);
                if (numForCap < 0)
                    numForCap = 0;
                textNumForCap.text = numForCap.ToString();
				this.gameObject.GetComponentInChildren<Text> ().text = numFOrTruckCap.ToString ();
            }

            //StartCoroutine(wait(numForCap, textNumForCap));
        }

        if (transform.position == targetWayPoint.position )
        {
            //Debug.Log ("did it");
            if (counter < cap)
            {
                Text textNumForCap = gameControll.gm[arr[count], arr[count + 1]].GetComponentInChildren<Text>();
                int numForCap = int.Parse(textNumForCap.text);
				int numForTruckCap = int.Parse (this.gameObject.GetComponentInChildren<Text> ().text);
                numForCap -= (cap - counter);
				numForTruckCap += (cap - counter);
                if (numForCap < 0)
                    numForCap = 0;
				if (numForTruckCap > 100)
					numForTruckCap = 100;
                textNumForCap.text = numForCap.ToString();
                this.gameObject.GetComponentInChildren<Text>().text = cap.ToString();
            }

			if (currentWayPoint < wayPointList.Length-1) {
				currentWayPoint++;
                count++;
                counter = 0;
				targetWayPoint = wayPointList[currentWayPoint];
				//transform.Rotate (new Vector3 (0, 0, 1));
			}
        }
    }

    //IEnumerator wait(int numForCap, Text textNumForCap )
    //{
    //    yield return new WaitForSeconds(0.5f);
    //    if (numForCap > 0 && counter<cap)
    //    {
    //        numForCap--;
    //        counter++;
    //        //Debug.Log(numForCap);
    //        textNumForCap.text = numForCap.ToString();
    //    }
    //}
}
