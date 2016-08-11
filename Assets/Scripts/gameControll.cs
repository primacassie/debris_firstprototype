using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;

public class gameControll : MonoBehaviour
{

    // Use this for initialization
    [HideInInspector]
    public static ArrayList pathStore = new ArrayList();

    // initilize trucks
    [HideInInspector]
    public static bool redTruck;
    [HideInInspector]
    public static bool blueTruck;
    [HideInInspector]
    public static bool greenTruck;

    //store how many truck have been here
    public static int redTruckNum;
    public static int blueTruckNum;
    public static int greenTruckNum;

    //cursor texture
//    public Texture2D cursorTextureR;
//    public Texture2D cursorTextureG;
//    public Texture2D cursorTextureB;
//    public Texture2D cursorTextureO;
//    public Texture2D cursorTextureScrollerR;
//    public Texture2D cursorTextureScrollerG;
//    public Texture2D cursorTextureScrollerB;
//    [HideInInspector]
//    public CursorMode cursorMode = CursorMode.Auto;
//    [HideInInspector]
//    public Vector2 hotSpot = Vector2.zero;

    //total debris of each kind of color
    [HideInInspector]
    public static float redProfitTotal;
    [HideInInspector]
    public static float blueProfitTotal;
    [HideInInspector]
    public static float greenProfitTotal;

    //total time of each kind of color
    [HideInInspector]
    public static float redTimeTotal;
    [HideInInspector]
    public static float blueTimeTotal;
    [HideInInspector]
    public static float greenTimeTotal;

    [HideInInspector]
    public static float redProfitOnce;
    [HideInInspector]
    public static float blueProfitOnce;
    [HideInInspector]
    public static float greenProfitOnce;

    [HideInInspector]
    public static float redTimeOnce;
    [HideInInspector]
    public static float blueTimeOnce;
    [HideInInspector]
    public static float greenTimeOnce;



    public Image depot;
    //public Node node;

    //twoNode is a queue to store two nodes of a path
    public static Queue<int> twoNode = new Queue<int>();

    //create an array to store the capacity of each path
	public static int[,] capArray;

    //create an array of time
	public static float[,] timeArray;

    //an interger store car capacity
    public static int carCap = 100;
    //Animator anim;

    //my gameobject here refer to the input tab
    //	public static GameObject myGameObject;

    //public static StreamWriter output = new StreamWriter(@"/Users/ericgo/Desktop/HOOutput.txt");

	//store all possible path of node;
	public static bool[,] nodePath;


	//private int[,] tempArray = new int[6, 6];

    void Awake()
    {
        //		GameObject inputTab=GameObject.Find("InputTab");
        //		myGameObject = inputTab;
        //		inputTab.SetActive (false);

        //Cursor.SetCursor(cursorTextureO, hotSpot, cursorMode);
		if (SceneManager.GetActiveScene ().name == "start") {
			
			capArray = new int[6, 6];
			timeArray = new float[6, 6];
		}

		if (SceneManager.GetActiveScene ().name == "level2") {
			capArray = new int[21, 21];
			timeArray = new float[21, 21];
		}
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("cap"))
        {
            obj.GetComponentInChildren<Slider>().enabled = false;
            obj.GetComponentInChildren<Slider>().GetComponent<RectTransform>().localScale = new Vector2(0, 0);
        }
        //Change Foreground to the layer you want it to display on 
        //You could prob. make a public variable for this
        //particleSystem.renderer.sortingLayerName = "Foreground";

    }

    void Start()
    {
        initializeCapArray();  //the method is to initalize both capacity arrays one is boolean and the other is int array.
                               //loadInputField();  //load input field when a path is clicked.
		//tempArray=capArray;                      //anim=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

		if (Input.GetKeyDown (KeyCode.Escape)) {
			//GameObject.Find("ModalControl").GetComponent<testWindow>().takeAction("Do you want to quit the game?");
			Application.Quit();
//			if (Time.frameCount>100 &&Time.frameCount < 300) {
//				if (Input.GetKeyDown (KeyCode.Escape)) {
//					Debug.Log ("quit");
//					Application.Quit ();
//				}
//			}
		}
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (Camera.main.orthographicSize < 5)
            {
                Camera.main.orthographicSize++;
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (Camera.main.orthographicSize > 1)
            {
                Camera.main.orthographicSize--;
            }
        }
        if (Camera.main.orthographicSize == 5)
        {
            Camera.main.transform.position = new Vector3(0, 0, 40);
        }
        //		if(Input.GetMouseButtonDown(0){
        //			Camera.main.rect.x=
        //		}
    }

    //change cursor and depot color
    public void changeRed()
    {
		if (!(redTruck || blueTruck || greenTruck) && !StoreTruckClick.hasCancelMark)
        {
			GameObject.Find("red").GetComponent<AudioSource> ().Play ();
            //Cursor.SetCursor(cursorTextureR, hotSpot, cursorMode);
            //depot.sprite = Resources.Load<Sprite> ("Image/cursorR") as Sprite;
            redTruck = true;
            GameObject.Find("depot").GetComponent<depotAnimation>().redAnimation();
            //saveToFile ("Choose the red truck.");
            RedTruck aRedTruck = new RedTruck();
            carCap = aRedTruck.capacity;
            GameObject.Find("storeTruck").GetComponent<storeTruck>().addTruck(redTruckNum);
            redTruckNum++;
			Node.storePath = new List<int>();
			Node.storeTruckCap=new List<int>();
			Node.storePath.Add(1);
			twoNode.Enqueue (1);
			foreach (int[] arr in pathStore) {
				if (arr [0] == 1) {
					string temp = "node" + arr [1];
					Behaviour halo=(Behaviour)GameObject.Find (temp).GetComponent ("Halo");
					halo.enabled = true;
				}
			}
        }
		if (greenTruck && (twoNode.Count==1) && twoNode.Peek()==1) {
			//Cursor.SetCursor(cursorTextureR, hotSpot, cursorMode);
			GameObject.Find("red").GetComponent<AudioSource> ().Play ();
			greenTruckNum--;
			string goName = "greenTruckImage" +greenTruckNum.ToString ();
			string goText = "greenTruckText" + greenTruckNum.ToString ();
			redTruck = true;
			greenTruck = false;
			Destroy (GameObject.Find (goName));
			Destroy (GameObject.Find (goText));
			GameObject.Find("storeTruck").GetComponent<storeTruck>().addTruck(redTruckNum);
			redTruckNum++;
			GameObject.Find("depot").GetComponent<depotAnimation>().redAnimation();
		}

		if (blueTruck && (twoNode.Count==1) && twoNode.Peek()==1) {
			GameObject.Find("red").GetComponent<AudioSource> ().Play ();
			//Cursor.SetCursor(cursorTextureR, hotSpot, cursorMode);
			blueTruckNum--;
			string goName = "blueTruckImage" +blueTruckNum.ToString ();
			string goText = "blueTruckText" + blueTruckNum.ToString ();
			redTruck = true;
			blueTruck = false;
			Destroy (GameObject.Find (goName));
			Destroy (GameObject.Find (goText));
			GameObject.Find("storeTruck").GetComponent<storeTruck>().addTruck(redTruckNum);
			redTruckNum++;
			GameObject.Find("depot").GetComponent<depotAnimation>().redAnimation();
		}
    }

    public void changeBlue()
    {
		if (!(redTruck || blueTruck || greenTruck) && !StoreTruckClick.hasCancelMark)
        {
            //Cursor.SetCursor(cursorTextureB, hotSpot, cursorMode);
            //depot.sprite = Resources.Load<Sprite> ("Image/cursorB") as Sprite;
			GameObject.Find("blue").GetComponent<AudioSource> ().Play ();
            blueTruck = true;
            GameObject.Find("depot").GetComponent<depotAnimation>().blueAnimation();
            //saveToFile ("Choose the blue truck.");
            BlueTruck aBlueTruck = new BlueTruck();
            carCap = aBlueTruck.capacity;
            GameObject.Find("storeTruck").GetComponent<storeTruck>().addTruck(blueTruckNum);
            blueTruckNum++;
			Node.storePath = new List<int>();
			Node.storePath.Add(1);
			Node.storeTruckCap=new List<int>();
			twoNode.Enqueue (1);
			foreach (int[] arr in pathStore) {
				if (arr [0] == 1) {
					string temp = "node" + arr [1];
					Behaviour halo=(Behaviour)GameObject.Find (temp).GetComponent ("Halo");
					halo.enabled = true;
				}
			}
        }

		if (greenTruck && (twoNode.Count==1) && twoNode.Peek()==1) {
			//Cursor.SetCursor(cursorTextureB, hotSpot, cursorMode);
			GameObject.Find("blue").GetComponent<AudioSource> ().Play ();
			greenTruckNum--;
			string goName = "greenTruckImage" +greenTruckNum.ToString ();
			string goText = "greenTruckText" + greenTruckNum.ToString ();
			blueTruck = true;
			greenTruck = false;
			Destroy (GameObject.Find (goName));
			Destroy (GameObject.Find (goText));
			GameObject.Find("storeTruck").GetComponent<storeTruck>().addTruck(blueTruckNum);
			blueTruckNum++;
			GameObject.Find("depot").GetComponent<depotAnimation>().blueAnimation();
		}

		if (redTruck && (twoNode.Count==1) && twoNode.Peek()==1) {
			//Cursor.SetCursor(cursorTextureB, hotSpot, cursorMode);
			GameObject.Find("blue").GetComponent<AudioSource> ().Play ();
			redTruckNum--;
			string goName = "redTruckImage" +redTruckNum.ToString ();
			string goText = "redTruckText" + redTruckNum.ToString ();
			Destroy (GameObject.Find (goName));
			Destroy (GameObject.Find (goText));
			blueTruck = true;
			redTruck = false;
			GameObject.Find("storeTruck").GetComponent<storeTruck>().addTruck(blueTruckNum);
			blueTruckNum++;
			GameObject.Find("depot").GetComponent<depotAnimation>().blueAnimation();
		}
    }

    public void changeGreen()
    {
		if (!(redTruck || blueTruck || greenTruck)&& !StoreTruckClick.hasCancelMark)
        {
            //Cursor.SetCursor(cursorTextureG, hotSpot, cursorMode);
            //depot.sprite = Resources.Load<Sprite> ("Image/cursorG") as Sprite;
			GameObject.Find("green").GetComponent<AudioSource> ().Play ();
            greenTruck = true;
            GameObject.Find("depot").GetComponent<depotAnimation>().greenAnimation();
            //saveToFile ("Choose the green truck.");
            GreenTruck aGreenTruck = new GreenTruck();
            carCap = aGreenTruck.capacity;
            GameObject.Find("storeTruck").GetComponent<storeTruck>().addTruck(greenTruckNum);
            greenTruckNum++;
			Node.storePath = new List<int>();
			Node.storePath.Add(1);
			Node.storeTruckCap=new List<int>();
			twoNode.Enqueue (1);
			foreach (int[] arr in pathStore) {
				if (arr [0] == 1) {
					string temp = "node" + arr [1];
					Behaviour halo=(Behaviour)GameObject.Find (temp).GetComponent ("Halo");
					halo.enabled = true;
				}
			}
        }

		if (blueTruck && (twoNode.Count==1) && twoNode.Peek()==1) {
			//Cursor.SetCursor(cursorTextureG, hotSpot, cursorMode);
			GameObject.Find("green").GetComponent<AudioSource> ().Play ();
			blueTruckNum--;
			string goName = "blueTruckImage" +blueTruckNum.ToString ();
			string goText = "blueTruckText" + blueTruckNum.ToString ();
			Destroy (GameObject.Find (goName));
			Destroy (GameObject.Find (goText));
			greenTruck = true;
			blueTruck = false;
			GameObject.Find("storeTruck").GetComponent<storeTruck>().addTruck(greenTruckNum);
			greenTruckNum++;
			GameObject.Find("depot").GetComponent<depotAnimation>().greenAnimation();
		}
		if (redTruck && (twoNode.Count==1) && twoNode.Peek()==1) {
			//Cursor.SetCursor(cursorTextureG, hotSpot, cursorMode);
			GameObject.Find("green").GetComponent<AudioSource> ().Play ();
			redTruckNum--;
			string goName = "redTruckImage" +redTruckNum.ToString ();
			string goText = "redTruckText" + redTruckNum.ToString ();
			Destroy (GameObject.Find (goName));
			Destroy (GameObject.Find (goText));
			greenTruck = true;
			redTruck = false;
			GameObject.Find("storeTruck").GetComponent<storeTruck>().addTruck(greenTruckNum);
			greenTruckNum++;
			GameObject.Find("depot").GetComponent<depotAnimation>().greenAnimation();
		}
    }

//    public void resetCursor()
//    {
//        Cursor.SetCursor(cursorTextureO, Vector2.zero, cursorMode);
//    }
//
//    public void changeScrollerR()
//    {
//        Cursor.SetCursor(cursorTextureScrollerR, hotSpot, cursorMode);
//    }
//
//    public void changeScrollerG()
//    {
//        Cursor.SetCursor(cursorTextureScrollerG, hotSpot, cursorMode);
//    }
//
//    public void changeScrollerB()
//    {
//        Cursor.SetCursor(cursorTextureScrollerB, hotSpot, cursorMode);
//    }

    public void resetDepot()
    {
        depot.sprite = Resources.Load<Sprite>("Image/depot") as Sprite;
    }

    //detect if it is a valid path
    public static bool validPath(int num1, int num2)
    {
        if (greenTruck || blueTruck || redTruck)
        {
            foreach (int[] ar in pathStore)
            {
                //Debug.Log ("ar1 == " + ar [0] + " ar2== " + ar [1]);
                if ((num1 == ar[0] && num2 == ar[1]) || (num1 == ar[1] && num2 == ar[0]))
                {
                    //Debug.Log ("true");
                    return true;
                }
            }
            //Debug.Log ("false");
            return false;
        }
        else if (!(greenTruck || blueTruck || redTruck))
        {
            Debug.Log("please select a truck!");
            //GameObject.Find("ModalControl").GetComponent<testWindow>().takeAction("Please select a truck!");
            return false;
        }
        return false;
    }
		

    public static void inputIsRight(int cap)
    {
        if (cap < 0)
        {
            Debug.Log("you can't assign a negative value to the car!");
            //GameObject.Find("ModalControl").GetComponent<testWindow>().takeAction("you can't assign a negative value to the car!");
        }
        else if (carCap - cap < 0)
        {
            Debug.Log("please re-enter a number because your car is overweightted");
            //GameObject.Find("ModalControl").GetComponent<testWindow>().takeAction("please re-enter a number because your car is overweightted");
        }
        else if (capArray[Node.passNode1, Node.passNode2] - cap < 0)
        {
            Debug.Log("please re-enter a number because you don't have such debris!");
            //add a pop up dialog here
            //GameObject.Find("ModalControl").GetComponent<testWindow>().takeAction("please re-enter a number because you don't have such debris!");
        }
        else
        {
            //			inputControl.valCorrect=true;
            //string toSave = "Collect " + cap + " units of debris.";
            //gameControll.saveToFile (toSave);
        }
    }

    private void loadInputField()
    {
        var obj = new GameObject();
        obj.transform.SetParent(GameObject.FindObjectOfType<Canvas>().gameObject.transform, false);
        obj.name = "InputPanel";
        obj.transform.position.Set(0, 0, 0);
        obj.transform.localScale.Set(1f, 1f, 0f);
        Image img = obj.AddComponent<Image>();
        img.sprite = Resources.Load<Sprite>("Image/purpleRect");
        obj.GetComponent<RectTransform>().sizeDelta = new Vector2(640f, 480f);

        //only one component can be added to a rectangle, so you need to create multiple objects attach to the rect to 
        //get what you want to create.
        GameObject textBox = new GameObject();
        textBox.transform.SetParent(GameObject.Find("InputPanel").transform, false);
        textBox.name = "TextPanel";
        textBox.transform.position.Set(0f, 0f, 0f);
        Text panelText = textBox.AddComponent<Text>();
        panelText.text = "please input the capacity of your truck";
        panelText.fontSize = 40;
        panelText.color = new Color(1, 1, 1);
        panelText.alignment = TextAnchor.MiddleCenter;
        panelText.font = Resources.Load<Font>("Font/AGENCYR") as Font;
        textBox.GetComponent<RectTransform>().sizeDelta = new Vector2(480f, 200f);  //set size by sizeDelta
        textBox.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 100f);  //set position by anchoredPosition

        //add input field here
        //		GameObject inputF = new GameObject ();
        //		inputF.transform.SetParent (GameObject.Find ("InputPanel").transform, false);
        //		inputF.transform.position.Set (0f, 0f, 0f);
        //		inputF.name = "inputTab";
        //		inputF.AddComponent<RectTransform> ();
        //		Image inputImage=inputF.AddComponent<Image> ();
        //		InputField inp=inputF.AddComponent<InputField> ();
        //		//inputF.GetComponent<Transform> ().localScale = new Vector2 (200f, 200f);
        //		inputF.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (-160f, -120f);
        //		inputF.GetComponent<RectTransform> ().sizeDelta = new Vector2 (320f, 120f);
        //		inp.text = "Please input the capacity here";
        //		inputImage.color = Color.green;
        //		inputF.GetComponent<InputField>().colors=ColorBlock.defaultColorBlock;
        //		//inputF.GetComponent<InputField>().image= Resources.Load<Image> ("Image/White_Rect") as Image;
        //		inputF.GetComponent<InputField>().targetGraphic=Resources.Load<Image> ("Image/White_Rect") as Image;

        //InputField inputTab = GameObject.Find ("Canvas").GetComponent<InputField> ();

        //inputTab.text = "Please input the capacity here...";
    }


    public void initializeCapArray()
    {
        GameObject[] path = GameObject.FindGameObjectsWithTag("path");
        //boolean array to store if they are connected;
        bool[,] connArray = new bool[6, 6];
		if (SceneManager.GetActiveScene ().name == "level2") {
			connArray = new bool[21, 21];
		}

        foreach (GameObject go in path)
        {
            pathStore.Add(go.GetComponent<Graph>().node);
            int arr1 = go.GetComponent<Graph>().node[0];
            int arr2 = go.GetComponent<Graph>().node[1];
            connArray[arr1, arr2] = true;
            connArray[arr2, arr1] = true;
            int cap = go.GetComponent<Graph>().capacity;
            capArray[arr1, arr2] = cap;
            capArray[arr2, arr1] = cap;
            float time = go.GetComponent<Graph>().time;
            timeArray[arr1, arr2] = time;
            timeArray[arr2, arr1] = time;
        }
		nodePath = connArray;
    }
		
    public static void saveToFile(string save)
    {
        using (StreamWriter writeText = File.AppendText("/Users/ericgo/Desktop/HOOutput.txt"))
        {
            writeText.WriteLine(save);
            writeText.Close();
        }
    }

	public void toggleForFilter(bool value){
		GameObject[] objArr=GameObject.FindGameObjectsWithTag ("linerender");
		foreach (GameObject obj in objArr) {
			obj.GetComponent<LineRenderer> ().enabled = value;
		}
		GameObject[] objArr1 = GameObject.FindGameObjectsWithTag ("indi");
		foreach (GameObject obj in objArr1) {
			obj.GetComponent<Image> ().enabled = value;
		}
	}
}
