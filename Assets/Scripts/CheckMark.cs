using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CheckMark : MonoBehaviour {

	public static bool nextStep;
	private int node1;
	private int node2;
	private float dist;
	private float lineDrawSpeed;
	private float counter;
	private Vector3 origin;
	private Vector3 destination;
	private LineRenderer lr;
	private bool startUpdate;

	//static arrays to store rgb path cycle. The difference with above is it's not bidirection
	private static bool[,] rgbPathArray = new bool[6, 6];
	private static bool[,] redPathArray = new bool[6, 6];
	private static bool[,] greenPathArray = new bool[6, 6];
	private static bool[,] bluePathArray = new bool[6, 6];

	//use a queue to store two nodes
	private Queue<int> twoNode=new Queue<int>();

    //three variable to count how many cycles finish in the game
    private static int redCycleCount;
    private static int blueCycleCount;
    private static int greenCycleCount;

    //create int array to store how many path in it
    private static int[,] redPathNum = new int[6, 6];
    private static int[,] greenPathNum = new int[6, 6];
    private static int[,] bluePathNum = new int[6, 6];

    void Start()
	{
		lineDrawSpeed = 10;
	}

	private void rectAnimation(int num1, int num2)
	{
		node1 = num1;
		node2 = num2;
		string strNode1 = "node" + node1;
		if (node1 == 1)
		{
			strNode1 = "depot";
		}
		string strNode2 = "node" + node2;
		if (node2 == 1)
		{
			strNode2 = "depot";
		}
		GameObject node1G = GameObject.Find(strNode1);
		GameObject node2G = GameObject.Find(strNode2);
		origin = node1G.transform.position;
		destination = node2G.transform.position;
		float slope = (origin.y - destination.y) / (origin.x - destination.x);
		float d = 0.165f;
		if (origin.y > destination.y) {
			origin = new Vector3 (origin.x + d*Mathf.Sin(Mathf.Atan(slope)), origin.y- d*Mathf.Cos(Mathf.Atan(slope)), origin.z);
			destination = new Vector3 (destination.x + d*Mathf.Sin(Mathf.Atan(slope)), destination.y-d*Mathf.Cos(Mathf.Atan(slope)), destination.z);
		}
		if (origin.y < destination.y) {
			origin = new Vector3 (origin.x - d*Mathf.Sin(Mathf.Atan(slope)), origin.y+d*Mathf.Cos(Mathf.Atan(slope)), origin.z);
			destination = new Vector3 (destination.x - d*Mathf.Sin(Mathf.Atan(slope)), destination.y+d*Mathf.Cos(Mathf.Atan(slope)), destination.z);
		}
		if (origin.y == destination.y) {
			if (origin.x < destination.x) {
				origin = new Vector3 (origin.x , origin.y-d, origin.z);
				destination = new Vector3 (destination.x , destination.y-d, destination.z);
			} else if (origin.x > destination.x) {
				origin = new Vector3 (origin.x , origin.y+d, origin.z);
				destination = new Vector3 (destination.x , destination.y+d, destination.z);
			}
		}
		lr = GetComponent<LineRenderer>();
		lr.SetPosition(0, origin);
		lr.SetWidth(.15f, .15f);
		if (gameControll.redTruck)
		{
			lr.material = Resources.Load<Material>("Materials/redAnimHalf") as Material;
		}else if (gameControll.greenTruck)
		{
			lr.material = Resources.Load<Material>("Materials/greenAnimHalf") as Material;
		}
		else if (gameControll.blueTruck)
		{
			lr.material = Resources.Load<Material>("Materials/blueAnimHalf") as Material;
		}
		dist = Vector3.Distance(origin, destination);
		startUpdate = true;
		StartCoroutine (toGradientColor (num1, num2));
	}

	void OnMouseDown(){
		pathCap.desableSlider();
        this.gameObject.GetComponent<Image>().color = new Vector4(0, 0, 0, 0);
		StartCoroutine (waitTime ());
    }

	IEnumerator toGradientColor(int num1,int num2){
		yield return new WaitForSeconds (2);
		string desObj = "newPathAnim" + num1.ToString () + num2.ToString ();
		if (GameObject.Find (desObj) != null) {
			Destroy (GameObject.Find (desObj));
		}
		string existObj = "pathAnim" + num1.ToString () + num2.ToString ();
		if (GameObject.Find (existObj) != null) {
			lr = GameObject.Find (existObj).GetComponent<LineRenderer> ();
			lr.enabled = true;
			if (redPathArray[num1, num2] && !greenPathArray[num1, num2] && bluePathArray[num1, num2])
			{
				lr.material = Resources.Load<Material>("Materials/GradientRB") as Material;
			}

			if (redPathArray[num1, num2] && greenPathArray[num1, num2] && !bluePathArray[num1, num2])
			{
				lr.material = Resources.Load<Material>("Materials/GradientRG") as Material;
			}

			if (!redPathArray[num1, num2] && greenPathArray[num1, num2] && bluePathArray[num1, num2])
			{
				lr.material = Resources.Load<Material>("Materials/GradientBG") as Material;
			}

			if (redPathArray[num1, num2] && greenPathArray[num1, num2] && bluePathArray[num1, num2])
			{
				lr.material = Resources.Load<Material>("Materials/GradientRGB") as Material;
			}
		}
	}

	IEnumerator waitTime()
	{
        for (int i=0; i < Node.nodeArray.Length; i++) {
			if (twoNode.Count == 0) {
				twoNode.Enqueue (Node.nodeArray [i]);
			}else if (twoNode.Count == 1) {
				twoNode.Enqueue (Node.nodeArray [i]);
				int num1 = twoNode.Peek ();
				int num2 = Node.nodeArray [i];
				string pathname = pathName (num1, num2, rgbPathArray);
				setBoolArray (num1, num2, rgbPathArray);
				GameObject path = new GameObject ();
				path.name = pathname;
				path.AddComponent<LineRenderer> ();
                path.AddComponent<CheckMark>();
				string dupObj = "newPathAnim" + num1.ToString () + num2.ToString ();
				if (pathname == dupObj) {
					string existObj = "pathAnim" + num1.ToString () + num2.ToString ();
					if (GameObject.Find (existObj) != null) {
						GameObject.Find (existObj).GetComponent<LineRenderer> ().enabled = false;
					}
				}
                path.GetComponent<CheckMark>().rectAnimation (num1, num2);
            }
            else if (twoNode.Count == 2) {
				twoNode.Dequeue ();
				twoNode.Enqueue (Node.nodeArray [i]);
				int num1 = twoNode.Peek ();
				int num2 = Node.nodeArray [i];
				string pathname = pathName (num1, num2, rgbPathArray);
				setBoolArray (num1, num2, rgbPathArray);
				GameObject path = new GameObject ();
				path.name = pathname;
				path.AddComponent<LineRenderer> ();
                path.AddComponent<CheckMark>();
                yield return new WaitForSeconds(2);
				string dupObj = "newPathAnim" + num1.ToString () + num2.ToString ();
				if (pathname == dupObj) {
					string existObj = "pathAnim" + num1.ToString () + num2.ToString ();
					if (GameObject.Find (existObj) != null)
						GameObject.Find (existObj).GetComponent<LineRenderer> ().enabled = false;
				}
                path.GetComponent<CheckMark>().rectAnimation(num1, num2);
                if (num2 == 1) {
                    //if (gameControll.redTruck)
                    //{
                    //    redCycleCount++;
                    //}else if(gameControll.blueTruck){
                    //    blueCycleCount++;
                    //}else if (gameControll.greenTruck)
                    //{
                    //    greenCycleCount++;
                    //}

                    //so to design a data stucture here, I can create a few arraylists or linkedlist? to track storeTruck, Path,
                    //every time I delete one,I only move the UI by translate them, and then count the number of total of them to determine
                    //the order of them.
                    StartCoroutine(waitToDestroy1());
                    //for (int j = 0; j < Node.nodeArray.Length - 1; j++)
                    //{
                    //    addIndicator(Node.nodeArray[j], Node.nodeArray[j + 1], gameControll.redTruck, gameControll.greenTruck, gameControll.blueTruck);
                    //}
                    //StartCoroutine(waitToDestroy());
                }
			}
		}
	}


        IEnumerator  wait2seconds()
    {
        yield return new WaitForSeconds(2);
    }

    IEnumerator waitToDestroy1()
    {
        yield return new WaitForSeconds(2);
        for (int j = 0; j < Node.nodeArray.Length - 1; j++)
        {
            addIndicator(Node.nodeArray[j], Node.nodeArray[j + 1], gameControll.redTruck, gameControll.greenTruck, gameControll.blueTruck);
        }
        nextStep = true;
        StartCoroutine(waitToDestroy());
    }

    IEnumerator waitToDestroy()
    {
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }

	void Update()
	{
		if ((counter < dist) && startUpdate)
		{
			counter += .1f / lineDrawSpeed;
			float x = Mathf.Lerp(0, dist, counter);
			Vector3 pointAlongline = x * Vector3.Normalize(destination - origin) + origin;
			lr.SetPosition(1, pointAlongline);
		}
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

	private void setBoolArray(int num1,int num2,bool[,] rgb){
		rgb [num1, num2] = true;
		if (gameControll.redTruck)
        {
            redPathArray[num1, num2] = true;
            redPathNum[num1, num2]++;
        }
				
		if (gameControll.greenTruck)
        {
            greenPathArray[num1, num2] = true;
            greenPathNum[num1, num2]++;
        }
					
		if (gameControll.blueTruck)
        {
			bluePathArray [num1, num2] = true;
            bluePathNum[num1, num2]++;
        }
	}

    private void addIndicator(int num1,int num2, bool red, bool green, bool blue)
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
        if (origin.y > destination.y)
        {
            origin = new Vector3(origin.x + d * Mathf.Sin(Mathf.Atan(slope)), origin.y - d * Mathf.Cos(Mathf.Atan(slope)), origin.z);
            destination = new Vector3(destination.x + d * Mathf.Sin(Mathf.Atan(slope)), destination.y - d * Mathf.Cos(Mathf.Atan(slope)), destination.z);
        }
        if (origin.y < destination.y)
        {
            origin = new Vector3(origin.x - d * Mathf.Sin(Mathf.Atan(slope)), origin.y + d * Mathf.Cos(Mathf.Atan(slope)), origin.z);
            destination = new Vector3(destination.x - d * Mathf.Sin(Mathf.Atan(slope)), destination.y + d * Mathf.Cos(Mathf.Atan(slope)), destination.z);
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
            int pn = redPathNum[num1, num2];
            Vector3 place = new Vector3();
            if (greenPathNum[num1, num2]== 0 && bluePathNum[num1, num2] == 0)
            {
                place = origin + new Vector3((float)pn / 3.0f * (destination.x - origin.x), (float)pn / 3.0f * (destination.y - origin.y), 0f);
            }else if (greenPathNum[num1, num2] != 0 && bluePathNum[num1, num2] == 0)
            {
                place = origin + new Vector3((float)pn / 6.0f * (destination.x - origin.x), (float)pn / 6.0f * (destination.y - origin.y), 0f);
                if (pn == 1)
                {
                    int gNum = greenPathNum[num1, num2];
                    string indicator1 = "greenIndicator1" + num1.ToString() + num2.ToString();
                    GameObject.Find(indicator1).GetComponent<RectTransform>().position = origin +
                        new Vector3(1.0f / 6.0f * 4.0f * (destination.x - origin.x), 1.0f / 6.0f * 4.0f * (destination.y - origin.y), 0f);
                    if (gNum == 2)
                    {
                        string indicator2 = "greenIndicator2" + num1.ToString() + num2.ToString();
                        GameObject.Find(indicator2).GetComponent<RectTransform>().position = origin +
                            new Vector3(1.0f / 6.0f * 5.0f * (destination.x - origin.x), 1.0f / 6.0f * 5.0f * (destination.y - origin.y), 0f);
                    }
                }
            }else if(greenPathNum[num1, num2] == 0 && bluePathNum[num1, num2] != 0)
            {
                place = origin + new Vector3((float)pn / 6.0f * (destination.x - origin.x), (float)pn / 6.0f * (destination.y - origin.y), 0f);
                if (pn == 1)
                {
                    int bNum = bluePathNum[num1, num2];
                    string indicator1 = "blueIndicator1" + num1.ToString() + num2.ToString();
                    GameObject.Find(indicator1).GetComponent<RectTransform>().position = origin +
                        new Vector3(1.0f / 6.0f * 4.0f * (destination.x - origin.x), 1.0f / 6.0f * 4.0f * (destination.y - origin.y), 0f);
                    if (bNum == 2)
                    {
                        string indicator2 = "blueIndicator2" + num1.ToString() + num2.ToString();
                        GameObject.Find(indicator2).GetComponent<RectTransform>().position = origin +
                            new Vector3(1.0f / 6.0f * 5.0f * (destination.x - origin.x), 1.0f / 6.0f * 5.0f * (destination.y - origin.y), 0f);
                    }
                }
            }
            else if (greenPathNum[num1, num2] != 0 && bluePathNum[num1, num2] != 0)
            {
                int bNum = bluePathNum[num1, num2];
                int gNum = greenPathNum[num1, num2];
                place = origin + new Vector3((float)pn / 9.0f * (destination.x - origin.x), (float)pn / 9.0f * (destination.y - origin.y), 0f);
                if (pn == 1)
                {
                    string indicator1 = "greenIndicator1" + num1.ToString() + num2.ToString();
                    GameObject.Find(indicator1).GetComponent<RectTransform>().position = origin +
                        new Vector3(4.0f / 9.0f  * (destination.x - origin.x), 4.0f / 9.0f * (destination.y - origin.y), 0f);
                    indicator1 = "blueIndicator1" + num1.ToString() + num2.ToString();
                    GameObject.Find(indicator1).GetComponent<RectTransform>().position = origin +
                        new Vector3(7.0f / 9.0f  * (destination.x - origin.x), 7.0f / 9.0f * (destination.y - origin.y), 0f);
                    if (gNum == 2)
                    {
                        string indicator2 = "greenIndicator2" + num1.ToString() + num2.ToString();
                        GameObject.Find(indicator2).GetComponent<RectTransform>().position = origin +
                            new Vector3(5.0f / 9.0f * (destination.x - origin.x), 5.0f/ 9.0f * (destination.y - origin.y), 0f);
                    }

                    if (bNum == 2)
                    {
                        string indicator2 = "blueIndicator2" + num1.ToString() + num2.ToString();
                        GameObject.Find(indicator2).GetComponent<RectTransform>().position = origin +
                            new Vector3(8.0f / 9.0f  * (destination.x - origin.x), 8.0f / 9.0f * (destination.y - origin.y), 0f);
                    }
                }
            }
            GameObject indicator = new GameObject();
            indicator.AddComponent<RectTransform>();
            indicator.transform.SetParent(GameObject.Find("gamePanel").transform, false);
            indicator.AddComponent<Image>();
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
            int pn = greenPathNum[num1, num2];
            Vector3 place = new Vector3();
            if (redPathNum[num1, num2] == 0 && bluePathNum[num1, num2] == 0)
            {
                place = origin + new Vector3((float)pn / 3.0f * (destination.x - origin.x), (float)pn / 3.0f * (destination.y - origin.y), 0f);
            }
            else if (redPathNum[num1, num2] != 0 && bluePathNum[num1, num2] == 0)
            {
                place = origin + new Vector3(((float)pn + 3.0f) / 6.0f * (destination.x - origin.x), ((float)pn + 3.0f) / 6.0f * (destination.y - origin.y), 0f);
                if (pn == 1)
                {
                    int rNum = redPathNum[num1, num2];
                    string indicator1 = "redIndicator1" + num1.ToString() + num2.ToString();
                    GameObject.Find(indicator1).GetComponent<RectTransform>().position = origin +
                        new Vector3(1.0f / 6.0f * (destination.x - origin.x), 1.0f / 6.0f  * (destination.y - origin.y), 0f);
                    if (rNum == 2)
                    {
                        string indicator2 = "redIndicator2" + num1.ToString() + num2.ToString();
                        GameObject.Find(indicator2).GetComponent<RectTransform>().position = origin +
                            new Vector3(1.0f / 6.0f * 2.0f * (destination.x - origin.x), 1.0f / 6.0f * 2.0f * (destination.y - origin.y), 0f);
                    }
                }
            }
            else if (redPathNum[num1, num2] == 0 && bluePathNum[num1, num2] != 0)
            {
                place = origin + new Vector3((float)pn / 6.0f * (destination.x - origin.x), (float)pn / 6.0f * (destination.y - origin.y), 0f);
                if (pn == 1)
                {
                    int bNum = bluePathNum[num1, num2];
                    string indicator1 = "blueIndicator1" + num1.ToString() + num2.ToString();
                    GameObject.Find(indicator1).GetComponent<RectTransform>().position = origin +
                        new Vector3(1.0f / 6.0f * 4.0f * (destination.x - origin.x), 1.0f / 6.0f * 4.0f * (destination.y - origin.y), 0f);
                    if (bNum == 2)
                    {
                        string indicator2 = "blueIndicator2" + num1.ToString() + num2.ToString();
                        GameObject.Find(indicator2).GetComponent<RectTransform>().position = origin +
                            new Vector3(1.0f / 6.0f * 5.0f * (destination.x - origin.x), 1.0f / 6.0f * 5.0f * (destination.y - origin.y), 0f);
                    }
                }
            }
            else if (redPathNum[num1, num2] != 0 && bluePathNum[num1, num2] != 0)
            {
                int bNum = bluePathNum[num1, num2];
                int rNum = redPathNum[num1, num2];
                place = origin + new Vector3(((float)pn + 3.0f) / 9.0f * (destination.x - origin.x), ((float)pn + 3.0f) / 9.0f * (destination.y - origin.y), 0f);
                if (pn == 1)
                {
                    string indicator1 = "redIndicator1" + num1.ToString() + num2.ToString();
                    GameObject.Find(indicator1).GetComponent<RectTransform>().position = origin +
                        new Vector3(1.0f / 9.0f * (destination.x - origin.x), 1.0f / 9.0f * (destination.y - origin.y), 0f);
                    indicator1 = "blueIndicator1" + num1.ToString() + num2.ToString();
                    GameObject.Find(indicator1).GetComponent<RectTransform>().position = origin +
                        new Vector3(7.0f / 9.0f * (destination.x - origin.x), 7.0f / 9.0f * (destination.y - origin.y), 0f);
                    if (rNum == 2)
                    {
                        string indicator2 = "redIndicator2" + num1.ToString() + num2.ToString();
                        GameObject.Find(indicator2).GetComponent<RectTransform>().position = origin +
                            new Vector3(2.0f / 9.0f * (destination.x - origin.x), 2.0f / 9.0f * (destination.y - origin.y), 0f);
                    }

                    if (bNum == 2)
                    {
                        string indicator2 = "blueIndicator2" + num1.ToString() + num2.ToString();
                        GameObject.Find(indicator2).GetComponent<RectTransform>().position = origin +
                            new Vector3(8.0f / 9.0f * (destination.x - origin.x), 8.0f/9.0f * (destination.y - origin.y), 0f);
                    }
                }
            }
            GameObject indicator = new GameObject();
            indicator.AddComponent<RectTransform>();
            indicator.transform.SetParent(GameObject.Find("gamePanel").transform, false);
            indicator.AddComponent<Image>();
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
            int pn = bluePathNum[num1, num2];
            Vector3 place = new Vector3();
            if (redPathNum[num1, num2] == 0 && greenPathNum[num1, num2] == 0)
            {
                place = origin + new Vector3((float)pn / 3.0f * (destination.x - origin.x), (float)pn / 3.0f * (destination.y - origin.y), 0f);
            }
            else if (redPathNum[num1, num2] != 0 && greenPathNum[num1, num2] == 0)
            {
                place = origin + new Vector3(((float)pn + 3.0f) / 6.0f * (destination.x - origin.x), ((float)pn + 3.0f) / 6.0f * (destination.y - origin.y), 0f);
                if (pn == 1)
                {
                    int rNum = redPathNum[num1, num2];
                    string indicator1 = "redIndicator1" + num1.ToString() + num2.ToString();
                    GameObject.Find(indicator1).GetComponent<RectTransform>().position = origin +
                        new Vector3(1.0f / 6.0f * (destination.x - origin.x), 1.0f / 6.0f * (destination.y - origin.y), 0f);
                    if (rNum == 2)
                    {
                        string indicator2 = "redIndicator2" + num1.ToString() + num2.ToString();
                        GameObject.Find(indicator2).GetComponent<RectTransform>().position = origin +
                            new Vector3(1.0f / 6.0f * 2.0f * (destination.x - origin.x), 1.0f / 6.0f * 2.0f * (destination.y - origin.y), 0f);
                    }
                }
            }
            else if (redPathNum[num1, num2] == 0 && greenPathNum[num1, num2] != 0)
            {
                place = origin + new Vector3(((float)pn + 3.0f) / 6.0f * (destination.x - origin.x), ((float)pn + 3.0f) / 6.0f * (destination.y - origin.y), 0f);
                if (pn == 1)
                {
                    int gNum = greenPathNum[num1, num2];
                    string indicator1 = "greenIndicator1" + num1.ToString() + num2.ToString();
                    GameObject.Find(indicator1).GetComponent<RectTransform>().position = origin +
                        new Vector3(1.0f / 6.0f * (destination.x - origin.x), 1.0f / 6.0f * (destination.y - origin.y), 0f);
                    if (gNum == 2)
                    {
                        string indicator2 = "greenIndicator2" + num1.ToString() + num2.ToString();
                        GameObject.Find(indicator2).GetComponent<RectTransform>().position = origin +
                            new Vector3(2.0f / 6.0f * (destination.x - origin.x), 2.0f / 6.0f * (destination.y - origin.y), 0f);
                    }
                }
            }
            else if (redPathNum[num1, num2] != 0 && greenPathNum[num1, num2] != 0)
            {
                int gNum = greenPathNum[num1, num2];
                int rNum = redPathNum[num1, num2];
                place = origin + new Vector3(((float)pn + 6.0f) / 9.0f * (destination.x - origin.x), ((float)pn + 6.0f) / 9.0f * (destination.y - origin.y), 0f);
                if (pn == 1)
                {
                    string indicator1 = "redIndicator1" + num1.ToString() + num2.ToString();
                    GameObject.Find(indicator1).GetComponent<RectTransform>().position = origin +
                        new Vector3(1.0f / 9.0f * (destination.x - origin.x), 1.0f / 9.0f * (destination.y - origin.y), 0f);
                    indicator1 = "greenIndicator1" + num1.ToString() + num2.ToString();
                    GameObject.Find(indicator1).GetComponent<RectTransform>().position = origin +
                        new Vector3(4.0f / 9.0f * (destination.x - origin.x), 4.0f / 9.0f * (destination.y - origin.y), 0f);
                    if (rNum == 2)
                    {
                        string indicator2 = "redIndicator2" + num1.ToString() + num2.ToString();
                        GameObject.Find(indicator2).GetComponent<RectTransform>().position = origin +
                            new Vector3(2.0f / 9.0f * (destination.x - origin.x), 2.0f / 9.0f * (destination.y - origin.y), 0f);
                    }

                    if (gNum == 2)
                    {
                        string indicator2 = "greenIndicator2" + num1.ToString() + num2.ToString();
                        GameObject.Find(indicator2).GetComponent<RectTransform>().position = origin +
                            new Vector3(5.0f / 9.0f * (destination.x - origin.x), 5.0f / 9.0f * (destination.y - origin.y), 0f);
                    }
                }
            }
            GameObject indicator = new GameObject();
            indicator.AddComponent<RectTransform>();
            indicator.transform.SetParent(GameObject.Find("gamePanel").transform, false);
            indicator.AddComponent<Image>();
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
