using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Node : MonoBehaviour
{
    public int num;

    //static int variable control intersection;
    public static int intersection;
    private Text sIntersection;

    //private DisplayManager displayManager;

    public Node(int n)
    {
        this.num = n;
    }

    public int get()
    {
        return num;
    }

    private static int size2lastNode;

    //create two globle number to pass two nodes number to gameContrll.cs
    public static int passNode1;
    public static int passNode2;

	//three index to store truck cap for each path
	public static int originCap;
	public static int laterCap;
	private static int size2last2Node;

    //this array store the gameobject represent the capacity of each path.
    private static GameObject[] capPath;

    //basic value that the scroll bar need to add
    private static float scrollBasicProfit;
    private static float scrollBasicProfitOnce;
    private static float scrollBasicTime;
    private static float scrollBasicTimeOnce;
	private static float secondBlue;

    //decide which kind of node should use
    private bool redN;
    private bool blueN;
    private bool greenN;
    private bool rbN;
    private bool rgN;
    private bool gbN;
    private bool rgbN;

	//judge if the backtodepot function is running
	private static bool backToDepot;

    //create lists of array to assign road information
    public static List<List<int>> redAl;
    public static List<List<int>> blueAl;
    public static List<List<int>> greenAl;

	//creat lists of array to assign truck information
	public static List<List<int>> redTruckCap;
	public static List<List<int>> blueTruckCap;
	public static List<List<int>> greenTruckCap;

	//create lists of array to assign profit information
	public static List<float> redProfitAl=new List<float>();
	public static List<float> greenProfitAl=new List<float>();
	public static List<float> blueProfitAl=new List<float>();

	//create lists of array to assign time inforamtion
	public static List<float> redTimeAl=new List<float>();
	public static List<float> greenTimeAl=new List<float>();
	public static List<float> blueTimeAl=new List<float>();

    public static List<int> storePath;
	public static List<int> storeTruckCap;
	public static float storeProfit;
	public static float storeTime;

    //static arrays store rgb line renders
    public static bool[,] redLineArray = new bool[6, 6];
    public static bool[,] blueLineArray = new bool[6, 6];
    public static bool[,] greenLineArray = new bool[6, 6];

	//three points that the checkmark will always appear
	public static Vector2 v1;
	public static Vector2 v2;
	public static Vector2 v3;

	//static array to make animation in the CheckMark.cs
	public static int nodeCount;
	public static int[] nodeArray;

	//static arrays to store rgb path cycle. The difference with above is it's not bidirection
	public static bool[,] rgbPathArray = new bool[6, 6];
	public static bool[,] redPathArray = new bool[6, 6];
	public static bool[,] greenPathArray = new bool[6, 6];
	public static bool[,] bluePathArray = new bool[6, 6];

	//create int array to store how many path in it
	public static int[,] redPathNum = new int[6, 6];
	public static int[,] greenPathNum = new int[6, 6];
	public static int[,] bluePathNum = new int[6, 6];


    //store the color of every node
    private static Dictionary<string, string> nodeDic = new Dictionary<string, string>();

    void Awake()
    {
		if (SceneManager.GetActiveScene ().name == "level2") {
			redLineArray = new bool[21, 21];
			blueLineArray = new bool[21, 21];
			greenLineArray = new bool[21, 21];

			rgbPathArray = new bool[21, 21];
			redPathArray = new bool[21, 21];
			greenPathArray = new bool[21, 21];
			bluePathArray = new bool[21, 21];

			//create int array to store how many path in it
			redPathNum = new int[21, 21];
			greenPathNum = new int[21, 21];
			bluePathNum = new int[21, 21];
		}
        capPath = GameObject.FindGameObjectsWithTag("cap");
        foreach (GameObject obj in capPath)
        {
            //obj.GetComponent<Text> ().text = "50";
            obj.GetComponentInChildren<Text>().text = "50";
        }
		redAl   = new List<List<int>> ();
		blueAl  = new List<List<int>> ();
		greenAl = new List<List<int>> ();
		redTruckCap=new List<List<int>> ();
		blueTruckCap=new List<List<int>> ();
		greenTruckCap=new List<List<int>> ();
        sIntersection = GameObject.Find ("intersection").GetComponent<Text> ();
		v1=getVector(GameObject.Find("depot").transform.position,GameObject.Find("node2").transform.position,GameObject.Find("node3").transform.position);
		v2=getVector(GameObject.Find("depot").transform.position,GameObject.Find("node2").transform.position,GameObject.Find("node5").transform.position);
		v3=getVector(GameObject.Find("depot").transform.position,GameObject.Find("node3").transform.position,GameObject.Find("node4").transform.position);      
    }

	void Update(){
		if (CheckMark.nextStep == true) {
			laterCap = gameControll.capArray [passNode1, passNode2];
			int t = originCap-laterCap;
			storeTruckCap.Add (t);
			Debug.Log ("length of this" + storeTruckCap.Count);
			backToDepot = false;
			if (gameControll.blueTruck) {
				blueAl.Add (storePath);
				blueTruckCap.Add (storeTruckCap);
				gameControll.blueTruck = false;
				storeProfit = gameControll.blueProfitOnce;
				storeTime = gameControll.blueTimeOnce;
				blueProfitAl.Add (storeProfit);
				blueTimeAl.Add (storeTime);
				Debug.Log ("store Time" + storeTime);
				gameControll.blueProfitOnce = 0;
				gameControll.blueTimeOnce = 0;
			}
			if (gameControll.redTruck) {
				redAl.Add (storePath);
				redTruckCap.Add (storeTruckCap);
				gameControll.redTruck = false;
				storeProfit = gameControll.redProfitOnce;
				storeTime = gameControll.redTimeOnce;
				Debug.Log ("store Time" + storeTime);
				redProfitAl.Add (storeProfit);
				redTimeAl.Add (storeTime);
				gameControll.redProfitOnce = 0;
				gameControll.redTimeOnce = 0;
			}
			if (gameControll.greenTruck) {
				greenAl.Add (storePath);
				greenTruckCap.Add (storeTruckCap);
				gameControll.greenTruck = false;
				storeProfit = gameControll.greenProfitOnce;
				storeTime = gameControll.greenTimeOnce;
				Debug.Log ("store Time" + storeTime);
				greenProfitAl.Add (storeProfit);
				greenTimeAl.Add (storeTime);
				gameControll.greenTimeOnce = 0;
				gameControll.greenProfitOnce = 0;
			}
			Destroy (GameObject.FindGameObjectWithTag ("truckText"));
			Debug.Log (" You have finish a cycle, please start another one!");

			//gameControll.saveToFile ("a cycle is finished.");

			//GameObject.Find ("ModalControl").GetComponent<testWindow> ().takeAction("You have finish a cycle!");

			//add truck scripts here
			//	GameObject.Find("storeTruck").GetComponent<storeTruck>().addTruck(0);

			//reset most of the things to the beginning here
			gameControll.twoNode.Clear ();
//			GameObject.Find ("GameController").GetComponent<gameControll> ().resetCursor ();
			GameObject.Find ("GameController").GetComponent<gameControll> ().resetDepot ();
			int i = 0;
			panelController.blueTextOnce.text = i.ToString ();
			panelController.redTextOnce.text = i.ToString ();
			panelController.greenTextOnce.text = i.ToString ();
			panelController.blueTimeOnce.text = i.ToString ();
			panelController.redTimeOnce.text = i.ToString ();
			panelController.greenTimeOnce.text = i.ToString ();
			CheckMark.nextStep = false;
		}
	}

    void OnMouseDown()
    {
        //Debug.Log (num);
        //int temp;
        Queue<int> t = gameControll.twoNode;
        int size = t.Count;


        if (size == 1)
        {
            int firstOfSize1 = t.Peek();
            if (gameControll.validPath(firstOfSize1, num))
            {
				GetComponent<AudioSource> ().Play ();
                gameControll.twoNode.Enqueue(num);
                //Debug.Log("this is the node" + num);
                size2lastNode = num;
                //string processtoSave = "connect depot to node " + num.ToString ();
                //gameControll.saveToFile (processtoSave);
                //Debug.Log (size2lastNode);

                //here get the capacity of the path
                passNode1 = firstOfSize1;
                passNode2 = num;
				size2last2Node = 1;
				//Debug.Log ("in first size2last2node " + size2last2Node);
                //change the image of node here
                clickChangeColor();

				foreach (int i in validPathAnimation(passNode1)) {
					string temp = "node" + i;
					if (i == 1) {
						temp = "depot";
					}
					Behaviour halo = (Behaviour) GameObject.Find (temp).GetComponent ("Halo");
					halo.enabled = false;
				}

				foreach (int i in validPathAnimation(passNode2)) {
					string temp = "node" + i;
					if (i == 1) {
						temp = "depot";
					}
					Behaviour halo = (Behaviour) GameObject.Find (temp).GetComponent ("Halo");
					halo.enabled = true;
				}

                if (passNode1 == 1)
                {
                    GameObject.Find("GameController").GetComponent<gameControll>().resetDepot();
                }

				clickChangeAnimation (passNode1, passNode2);
				originCap = gameControll.capArray [passNode1, passNode2];
				//Debug.Log ("origin"+originCap);

                //add node number to store the path
                storePath.Add(passNode2);

                //this function used to create new game object to realize the line render.
//                if (!pathAlreadyExist(passNode1, passNode2, gameControll.redTruck, gameControll.greenTruck, gameControll.blueTruck))
//                {
//                    if (redLineArray[passNode1, passNode2] || greenLineArray[passNode1, passNode2] || blueLineArray[passNode1, passNode2])
//                    {
//                        createAndDestroyObjectForLineRender();
//						//createObjectForLineRender();
//                    }
//                    else
//                    {
//                        createObjectForLineRender();
//                    }
//                }
				string pathname = pathName (passNode1, passNode2, rgbPathArray);
				setBoolArray (passNode1, passNode2, rgbPathArray);
				GameObject path = new GameObject ();
				path.name = pathname;
				path.AddComponent<LineRenderer> ();
				path.tag = "linerender";
				path.AddComponent<LineAnimation>();
				string dupObj = "newPathAnim" + passNode1.ToString () + passNode2.ToString ();
				if (pathname == dupObj) {
					string existObj = "pathAnim" + passNode1.ToString () + passNode2.ToString ();
					if (GameObject.Find (existObj) != null) {
						GameObject.Find (existObj).GetComponent<LineRenderer> ().enabled = false;
						//setIndicatorUnseen (num1, num2);
					}
				}
				path.GetComponent<LineAnimation>().rectAnimation (passNode1, passNode2);


                //here is the second version of UI design
                setInitialValue(passNode1, passNode2);
				Debug.Log ("initial 2 " + gameControll.blueTimeOnce);

                //				Debug.Log ("passnode1 is " + passNode1);
                //				Debug.Log ("passnode2 is " + passNode2);

                //pathCap.initializeSlider ();

                //this is the first version of ui design
                //find inactive here is a grame object contrlled from mygameObject
                //				GameObject findInactive = gameControll.myGameObject;
                //				findInactive.SetActive(true);

                //				modifyCap (inputControl.capVal, firstOfSize1, num);
                //				Debug.Log("you collect "+ inputControl.capVal
                //					+" the capacity of this path "+firstOfSize1+ num+" remains: " +  gameControll.capArray[firstOfSize1,num]);
            }
            else
            {
                string toSave = "this node is not connected with the node " + firstOfSize1 + " please select a valid one! ";
                Debug.Log(toSave);

                //GameObject.Find ("ModalControl").GetComponent<testWindow> ().takeAction (toSave);
            }
        }
        else if (size == 2)
        {
			GetComponent<AudioSource> ().Play ();
            //temp = gameControll.twoNode.Dequeue ();
            //Debug.Log ("remove" + temp);
            //Debug.Log(size2lastNode);

			if (gameControll.validPath(size2lastNode, num) && !backToDepot)
            {


				laterCap = gameControll.capArray [size2last2Node, size2lastNode];
		//		Debug.Log ("later cap" + laterCap);
				int tempCap = originCap-laterCap;
				if (gameControll.redTruck) {
					storeTruckCap.Add (tempCap);
				} else if (gameControll.blueTruck) {
					storeTruckCap.Add (tempCap);
				} else if (gameControll.greenTruck) {
					storeTruckCap.Add (tempCap);
				}
//				Debug.Log ("later" + laterCap);
//				Debug.Log ("truckCap is" + tempCap);
                gameControll.twoNode.Dequeue();
                //Debug.Log ("remove " + temp);
                pathCap.desableSlider();
                gameControll.twoNode.Enqueue(num);
                passNode1 = size2lastNode;
                passNode2 = num;

				size2last2Node = passNode1;
				originCap = gameControll.capArray [passNode1, passNode2];
				Debug.Log ("origin cap" + originCap);
                //				GameObject inputTab=GameObject.Find("InputTab");
                //				inputTab.SetActive (true);

                //change node color here
                clickChangeColor();
				clickChangeAnimation (passNode1, passNode2);


                //this function used to create new game object to realize the line render.
//                if (!pathAlreadyExist(passNode1, passNode2,gameControll.redTruck,gameControll.greenTruck,gameControll.blueTruck))
//                {
//                    if(redLineArray[passNode1,passNode2] || greenLineArray[passNode1, passNode2] || blueLineArray[passNode1, passNode2])
//                    {
//                        createAndDestroyObjectForLineRender();
//						//createObjectForLineRender();
//                    }else
//                    {
//                        createObjectForLineRender();
//                    }
//                }

				foreach (int i in validPathAnimation(passNode1)) {
					string temp = "node" + i;
					if (i == 1) {
						temp = "depot";
					}
					Behaviour halo = (Behaviour) GameObject.Find (temp).GetComponent ("Halo");
					halo.enabled = false;
				}

				if (passNode2 != 1) {
					foreach (int i in validPathAnimation(passNode2)) {
						string temp = "node" + i;
						if (i == 1) {
							temp = "depot";
						}
						Behaviour halo = (Behaviour) GameObject.Find (temp).GetComponent ("Halo");
						halo.enabled = true;
					}
				}

				string pathname = pathName (passNode1, passNode2, rgbPathArray);
				setBoolArray (passNode1, passNode2, rgbPathArray);
				GameObject path = new GameObject ();
				path.name = pathname;
				path.AddComponent<LineRenderer> ();
				path.tag = "linerender";
				path.AddComponent<LineAnimation>();
				string dupObj = "newPathAnim" + passNode1.ToString () + passNode2.ToString ();
				if (pathname == dupObj) {
					string existObj = "pathAnim" + passNode1.ToString () + passNode2.ToString ();
					if (GameObject.Find (existObj) != null) {
						GameObject.Find (existObj).GetComponent<LineRenderer> ().enabled = false;
						//setIndicatorUnseen (num1, num2);
					}
				}
				path.GetComponent<LineAnimation>().rectAnimation (passNode1, passNode2);

                storePath.Add(passNode2);

                //GameObject.Find ("GameController").GetComponent<LineAnimation> ().rectAnimation (passNode1,passNode2);
                //second ui update here
                setInitialValue(passNode1, passNode2);
                //pathCap.initializeSlider ();
                //pathCap.

                //string toSave="connect node " + passNode1 + " and node " + passNode2;
                //gameControll.saveToFile (toSave);

                //modify node back to depot here
                nodeBackToDepot ();


                //				GameObject findInactive = gameControll.myGameObject;
                //				findInactive.SetActive(true);

                //				modifyCap (inputControl.capVal, size2lastNode, num);
                //				Debug.Log ("you collect "+ inputControl.capVal
                //					+" the capacity of this path "+size2lastNode+ num+" remains: " +  gameControll.capArray[size2lastNode,num]);
                size2lastNode = num;
                //Debug.Log (size2lastNode);

                //here I need to add a few lines to process if the player come back to the depot
            }
            else
            {
                string toSave = "this node is not connected with the node " + size2lastNode + " please select a valid one! ";
                Debug.Log(toSave);
                //GameObject.Find ("ModalControl").GetComponent<testWindow> ().takeAction (toSave);
            }
        }
    }

    //here is the function that called when node back to depot
    public static void nodeBackToDepot()
	{
		if (Node.passNode2 == 1) {
			originCap = gameControll.capArray [passNode1, passNode2];
            GameObject.Find("GameController").GetComponent<gameControll>().resetDepot();
            backToDepot = true;
			setConfirmPathButton (storePath);
		}
	}

    //function to modify capacity of truck and path;
    //this can be the function with the first version
    public static void modifyCap(int num, int node1, int node2)
    {
        gameControll.carCap -= num;
        gameControll.capArray[node1, node2] -= num;
        gameControll.capArray[node2, node1] -= num;
        //string toSave = " collect " + num + " units of debris.";
        //modify the text in path of the UI
        //		foreach (GameObject obj in capPath) {
        //			int num1 = obj.GetComponent<pathCap> ().node [0];
        //			int num2 = obj.GetComponent<pathCap> ().node [1];
        //			if ((num1 == node1 && num2 == node2) || (num1 == node2 && num2 == node1)) {
        //				obj.GetComponentInChildren<Text> ().text = gameControll.capArray [node1, node2].ToString ();
        //				break;
        //			}
        //		}

        if (gameControll.redTruck)
        {
            gameControll.redProfitTotal += num * 10 - gameControll.timeArray[node1, node2] / 2 * 7;
            gameControll.redProfitOnce += num * 10 - gameControll.timeArray[node1, node2] / 2 * 7;
            gameControll.redTimeTotal += gameControll.timeArray[node1, node2] + num * 10;
            gameControll.redTimeOnce += gameControll.timeArray[node1, node2] + num * 10;
            string findName = "redTruckText" + (gameControll.redTruckNum - 1).ToString();
            GameObject.Find(findName).GetComponent<Text>().text = gameControll.carCap.ToString();
            //Debug.Log (gameControll.redDebrisTotal);
            //Debug.Log (panelController.redText);
            panelController.redText.text = gameControll.redProfitTotal.ToString();
            panelController.redTime.text = gameControll.redTimeTotal.ToString();
            panelController.redTextOnce.text = gameControll.redProfitOnce.ToString();
            panelController.redTimeOnce.text = gameControll.redTimeOnce.ToString();
        }

        if (gameControll.blueTruck)
        {
            gameControll.blueProfitTotal += num * 10 - gameControll.timeArray[node1, node2] / 2 * 7;
            gameControll.blueProfitOnce += num * 10 - gameControll.timeArray[node1, node2] / 2 * 7;
            gameControll.blueTimeTotal += gameControll.timeArray[node1, node2] + num * 10;
            gameControll.blueTimeOnce += gameControll.timeArray[node1, node2] + num * 10;
            string findName = "blueTruckText" + (gameControll.blueTruckNum - 1).ToString();
            GameObject.Find(findName).GetComponent<Text>().text = gameControll.carCap.ToString();
            panelController.blueText.text = gameControll.blueProfitTotal.ToString();
            panelController.blueTime.text = gameControll.blueTimeTotal.ToString();
            panelController.blueTextOnce.text = gameControll.blueProfitOnce.ToString();
            panelController.blueTimeOnce.text = gameControll.blueTimeOnce.ToString();
        }

        if (gameControll.greenTruck)
        {
            gameControll.greenProfitTotal += num * 10 - gameControll.timeArray[node1, node2] / 2 * 7;
            gameControll.greenProfitOnce += num * 10 - gameControll.timeArray[node1, node2] / 2 * 7;
            gameControll.greenTimeTotal += gameControll.timeArray[node1, node2] + num * 10;
            gameControll.greenTimeOnce += gameControll.timeArray[node1, node2] + num * 10;
            string findName = "greenTruckText" + (gameControll.greenTruckNum - 1).ToString();
            GameObject.Find(findName).GetComponent<Text>().text = gameControll.carCap.ToString();
            panelController.greenText.text = gameControll.greenProfitTotal.ToString();
            panelController.greenTime.text = gameControll.greenTimeTotal.ToString();
            panelController.greenTextOnce.text = gameControll.greenProfitOnce.ToString();
            panelController.greenTimeOnce.text = gameControll.greenTimeOnce.ToString();
        }
    }

    //this setInital for the modify in the update time
    public static void setInitialValue(int node1, int node2)
    {
        if (gameControll.redTruck)
        {
            gameControll.redProfitTotal -= gameControll.timeArray[node1, node2] / 2 * 7;
            gameControll.redProfitOnce -= gameControll.timeArray[node1, node2] / 2 * 7;
            gameControll.redTimeTotal += gameControll.timeArray[node1, node2];
            gameControll.redTimeOnce += gameControll.timeArray[node1, node2];
            scrollBasicProfit = gameControll.redProfitTotal;
            scrollBasicProfitOnce = gameControll.redProfitOnce;
            scrollBasicTime = gameControll.redTimeTotal;
            scrollBasicTimeOnce = gameControll.redTimeOnce;
            string findName = "redTruckText" + (gameControll.redTruckNum - 1).ToString();
            int truckS = 100 - gameControll.carCap;
            string truckStore = truckS + "/100";
            GameObject.Find(findName).GetComponent<Text>().text = truckStore;
            //Debug.Log (gameControll.redDebrisTotal);
            //Debug.Log (panelController.redText);
            panelController.redText.text = gameControll.redProfitTotal.ToString();
            panelController.redTime.text = gameControll.redTimeTotal.ToString();
            panelController.redTextOnce.text = gameControll.redProfitOnce.ToString();
            panelController.redTimeOnce.text = gameControll.redTimeOnce.ToString();
        }

        if (gameControll.blueTruck)
        {
            gameControll.blueProfitTotal -= gameControll.timeArray[node1, node2] / 2 * 7;
            gameControll.blueProfitOnce -= gameControll.timeArray[node1, node2] / 2 * 7;
            gameControll.blueTimeTotal += gameControll.timeArray[node1, node2];
            gameControll.blueTimeOnce += gameControll.timeArray[node1, node2];
            scrollBasicProfit = gameControll.blueProfitTotal;
            scrollBasicProfitOnce = gameControll.blueProfitOnce;
            scrollBasicTime = gameControll.blueTimeTotal;
            scrollBasicTimeOnce = gameControll.blueTimeOnce;
			Debug.Log ("initial" + gameControll.blueTimeOnce);
            string findName = "blueTruckText" + (gameControll.blueTruckNum - 1).ToString();
            int truckS = 100 - gameControll.carCap;
            string truckStore = truckS + "/100";
            GameObject.Find(findName).GetComponent<Text>().text = truckStore;
            //Debug.Log (gameControll.blueDebrisTotal);
            //Debug.Log (panelController.blueText);
            panelController.blueText.text = gameControll.blueProfitTotal.ToString();
            panelController.blueTime.text = gameControll.blueTimeTotal.ToString();
            panelController.blueTextOnce.text = gameControll.blueProfitOnce.ToString();
			panelController.blueTimeOnce.text = gameControll.blueTimeOnce.ToString ();
        }

        if (gameControll.greenTruck)
        {
            gameControll.greenProfitTotal -= gameControll.timeArray[node1, node2] / 2 * 7;
            gameControll.greenProfitOnce -= gameControll.timeArray[node1, node2] / 2 * 7;
            gameControll.greenTimeTotal += gameControll.timeArray[node1, node2];
            gameControll.greenTimeOnce += gameControll.timeArray[node1, node2];
            scrollBasicProfit = gameControll.greenProfitTotal;
            scrollBasicProfitOnce = gameControll.greenProfitOnce;
            scrollBasicTime = gameControll.greenTimeTotal;
            scrollBasicTimeOnce = gameControll.greenTimeOnce;
            string findName = "greenTruckText" + (gameControll.greenTruckNum - 1).ToString();
            int truckS = 100 - gameControll.carCap;
            string truckStore = truckS + "/100";
            GameObject.Find(findName).GetComponent<Text>().text = truckStore;
            //Debug.Log (gameControll.greenDebrisTotal);
            //Debug.Log (panelController.greenText);
            panelController.greenText.text = gameControll.greenProfitTotal.ToString();
            panelController.greenTime.text = gameControll.greenTimeTotal.ToString();
            panelController.greenTextOnce.text = gameControll.greenProfitOnce.ToString();
            panelController.greenTimeOnce.text = gameControll.greenTimeOnce.ToString();
        }
        pathCap.initializeSlider();
    }

    public static void modifyInUpdate(int num, int node1, int node2)
    {
        gameControll.carCap -= num;
        gameControll.capArray[node1, node2] -= num;
        gameControll.capArray[node2, node1] -= num;
        if (gameControll.redTruck)
        {
            gameControll.redProfitTotal += num * 10;
            gameControll.redProfitOnce += num * 10;
            gameControll.redTimeTotal += num * 10;
            gameControll.redTimeOnce += num * 10;
            string findName = "redTruckText" + (gameControll.redTruckNum - 1).ToString();
            int truckS = 100 - gameControll.carCap;
            string truckStore = truckS.ToString() + "/100";
            GameObject.Find(findName).GetComponent<Text>().text = truckStore;
            //Debug.Log (gameControll.redDebrisTotal);
            //Debug.Log (panelController.redText);
            panelController.redText.text = gameControll.redProfitTotal.ToString();
            panelController.redTime.text = gameControll.redTimeTotal.ToString();
            panelController.redTextOnce.text = gameControll.redProfitOnce.ToString();
            panelController.redTimeOnce.text = gameControll.redTimeOnce.ToString();
        }

        if (gameControll.blueTruck)
        {
            gameControll.blueProfitTotal += num * 10;
            gameControll.blueProfitOnce += num * 10;
            gameControll.blueTimeTotal += num * 10;
            gameControll.blueTimeOnce += num * 10;
            string findName = "blueTruckText" + (gameControll.blueTruckNum - 1).ToString();
            int truckS = 100 - gameControll.carCap;
            string truckStore = truckS.ToString() + "/100";
            GameObject.Find(findName).GetComponent<Text>().text = truckStore;
            //Debug.Log (gameControll.blueDebrisTotal);
            //Debug.Log (panelController.blueText);
            panelController.blueText.text = gameControll.blueProfitTotal.ToString();
            panelController.blueTime.text = gameControll.blueTimeTotal.ToString();
            panelController.blueTextOnce.text = gameControll.blueProfitOnce.ToString();
            panelController.blueTimeOnce.text = gameControll.blueTimeOnce.ToString();
        }

        if (gameControll.greenTruck)
        {
            gameControll.greenProfitTotal += num * 10;
            gameControll.greenProfitOnce += num * 10;
            gameControll.greenTimeTotal += num * 10;
            gameControll.greenTimeOnce += num * 10;
            string findName = "greenTruckText" + (gameControll.greenTruckNum - 1).ToString();
            int truckS = 100 - gameControll.carCap;
            string truckStore = truckS.ToString() + "/100";
            GameObject.Find(findName).GetComponent<Text>().text = truckStore;
            //Debug.Log (gameControll.greenDebrisTotal);
            //Debug.Log (panelController.greenText);
            panelController.greenText.text = gameControll.greenProfitTotal.ToString();
            panelController.greenTime.text = gameControll.greenTimeTotal.ToString();
            panelController.greenTextOnce.text = gameControll.greenProfitOnce.ToString();
            panelController.greenTimeOnce.text = gameControll.greenTimeOnce.ToString();
        }
    }

    //count the profit of modify the number by slider
    public static void modifyBySlider(int origin, int now, int originCar)
    {
        //Debug.Log ("carCap " + originCar);
        gameControll.carCap = originCar - (origin - now);
        if (gameControll.redTruck)
        {
            gameControll.redProfitTotal = scrollBasicProfit + (origin - now) * 10;
            gameControll.redProfitOnce = scrollBasicProfitOnce + (origin - now) * 10;
            gameControll.redTimeTotal = scrollBasicTime + (origin - now) * 10;
            gameControll.redTimeOnce = scrollBasicTimeOnce + (origin - now) * 10;
            string findName = "redTruckText" + (gameControll.redTruckNum - 1).ToString();
            int truckS = 100 - gameControll.carCap;
            string truckStore = truckS.ToString() + "/100";
            GameObject.Find(findName).GetComponent<Text>().text = truckStore;
            //Debug.Log (gameControll.redDebrisTotal);
            //Debug.Log (panelController.redText);
            panelController.redText.text = gameControll.redProfitTotal.ToString();
            panelController.redTime.text = gameControll.redTimeTotal.ToString();
            panelController.redTextOnce.text = gameControll.redProfitOnce.ToString();
            panelController.redTimeOnce.text = gameControll.redTimeOnce.ToString();
        }

        if (gameControll.blueTruck)
        {
            gameControll.blueProfitTotal = scrollBasicProfit + (origin - now) * 10;
            gameControll.blueProfitOnce = scrollBasicProfitOnce + (origin - now) * 10;
            gameControll.blueTimeTotal = scrollBasicTime + (origin - now) * 10;
            gameControll.blueTimeOnce = scrollBasicTimeOnce + (origin - now) * 10;
            string findName = "blueTruckText" + (gameControll.blueTruckNum - 1).ToString();
            int truckS = 100 - gameControll.carCap;
            string truckStore = truckS.ToString() + "/100";
            GameObject.Find(findName).GetComponent<Text>().text = truckStore;
            //Debug.Log (gameControll.blueDebrisTotal);
            //Debug.Log (panelController.blueText);
            panelController.blueText.text = gameControll.blueProfitTotal.ToString();
            panelController.blueTime.text = gameControll.blueTimeTotal.ToString();
            panelController.blueTextOnce.text = gameControll.blueProfitOnce.ToString();
            panelController.blueTimeOnce.text = gameControll.blueTimeOnce.ToString();
        }

        if (gameControll.greenTruck)
        {
            gameControll.greenProfitTotal = scrollBasicProfit + (origin - now) * 10;
            gameControll.greenProfitOnce = scrollBasicProfitOnce + (origin - now) * 10;
            gameControll.greenTimeTotal = scrollBasicTime + (origin - now) * 10;
            gameControll.greenTimeOnce = scrollBasicTimeOnce + (origin - now) * 10;
            string findName = "greenTruckText" + (gameControll.greenTruckNum - 1).ToString();
            int truckS = 100 - gameControll.carCap;
            string truckStore = truckS.ToString() + "/100";
            GameObject.Find(findName).GetComponent<Text>().text = truckStore;
            //Debug.Log (gameControll.greenDebrisTotal);
            //Debug.Log (panelController.greenText);
            panelController.greenText.text = gameControll.greenProfitTotal.ToString();
            panelController.greenTime.text = gameControll.greenTimeTotal.ToString();
            panelController.greenTextOnce.text = gameControll.greenProfitOnce.ToString();
            panelController.greenTimeOnce.text = gameControll.greenTimeOnce.ToString();
        }
    }

    //the funtion is used to change node color.
    private void changeNodeColor(string toWhich)
    {
        string pathToNode = "Node/" + toWhich;
        this.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(pathToNode) as Sprite;
        string dicName = "node" + passNode2.ToString();
        //Debug.Log("this is dicName " + dicName);
        if (nodeDic.ContainsKey(dicName))
        {
            nodeDic.Remove(dicName);
            nodeDic.Add(dicName, pathToNode);
        }
        else
        {
            nodeDic.Add(dicName, pathToNode);
        }
    }

    private void clickChangeColor()
    {
        if (!(redN || greenN || blueN || rgN || rbN || gbN || rgbN))
        {
            if (gameControll.redTruck)
            {
                changeNodeColor("R");
                redN = true;
            }
            else if (gameControll.blueTruck)
            {
                changeNodeColor("B");
                blueN = true;
            }
            else if (gameControll.greenTruck)
            {
                changeNodeColor("G");
                greenN = true;
            }
        }
        else if (redN && !(greenN || blueN || rgN || rbN || gbN || rgbN))
        {
            if (gameControll.blueTruck)
            {
				intersection++;
				sIntersection.text = intersection.ToString ();
                changeNodeColor("RB");
                blueN = true;
                rbN = true;
            }
            else if (gameControll.greenTruck)
            {
				intersection++;
				sIntersection.text = intersection.ToString ();
                changeNodeColor("RG");
                greenN = true;
                rgN = true;
            }
        }
        else if (blueN && !(greenN || redN || rgN || rbN || gbN || rgbN))
        {
            if (gameControll.redTruck)
            {
				intersection++;
				sIntersection.text = intersection.ToString ();
                changeNodeColor("RB");
                redN = true;
                rbN = true;
            }
            else if (gameControll.greenTruck)
            {
				intersection++;
				sIntersection.text = intersection.ToString ();
                changeNodeColor("GB");
                greenN = true;
                gbN = true;
            }
        }
        else if (greenN && !(blueN || redN || rgN || rbN || gbN || rgbN))
        {
            if (gameControll.redTruck)
            {
				intersection++;
				sIntersection.text = intersection.ToString ();
                changeNodeColor("RG");
                redN = true;
                rgN = true;
            }
            else if (gameControll.blueTruck)
            {
				intersection++;
				sIntersection.text = intersection.ToString ();
                changeNodeColor("GB");
                blueN = true;
                gbN = true;
            }
        }
        else if (rbN && !(greenN || rgN || gbN || rgbN))
        {
            if (gameControll.greenTruck)
            {
                changeNodeColor("RGB");
                greenN = true;
                rgN = true;
                gbN = true;
                rgbN = true;
            }
        }
        else if (rgN && !(blueN || rbN || gbN || rgbN))
        {
            if (gameControll.blueTruck)
            {
                changeNodeColor("RGB");
                blueN = true;
                rbN = true;
                gbN = true;
                rgbN = true;
            }
        }
        else if (gbN && !(redN || rbN || rgN || rgbN))
        {
            if (gameControll.redTruck)
            {
                changeNodeColor("RGB");
                redN = true;
                rbN = true;
                rgN = true;
                rgbN = true;
            }
        }
    }

	private void clickChangeAnimation(int num1,int num2){
		string animName1 = "node" + passNode1.ToString ();
		string animName2 = "node" + passNode2.ToString ();
		if(passNode1==1){
			animName1 = "depot";
		}
        GameObject.Find(animName1).GetComponent<Animator>().enabled = false;
        if (nodeDic.ContainsKey(animName1))
        {
            //Debug.Log("in contains "+animName1);
            string toWhich = nodeDic[animName1];
            string pathToNode = toWhich;
            //GameObject.Find(animName1).GetComponent<Animator>().enabled = false;
            GameObject.Find(animName1).GetComponent<Image>().sprite = Resources.Load<Sprite>(pathToNode) as Sprite;
        }
        if (passNode2==1){
            GameObject.Find(animName1).GetComponent<Animator>().enabled = false;
            return;
        }
        if (gameControll.redTruck) {
            GameObject.Find(animName2).GetComponent<NodeAnimation>().redAnimation();
		}
		if (gameControll.greenTruck) {
            GameObject.Find(animName2).GetComponent<NodeAnimation>().greenAnimation();
		}
		if (gameControll.blueTruck) {
            GameObject.Find(animName2).GetComponent<NodeAnimation>().blueAnimation();
		}
	}
//    private void createObjectForLineRender()
//    {
//        GameObject pathAnimation = new GameObject();
//        pathAnimation.AddComponent<RectTransform>();
//		pathAnimation.GetComponent<RectTransform>().Translate(new Vector3(0,0,10f));
//        pathAnimation.name = "pathAnimation" + passNode1.ToString() + passNode2.ToString();
//        pathAnimation.AddComponent<LineRenderer>();
//        pathAnimation.AddComponent<LineAnimation>();
//        pathAnimation.GetComponent<LineAnimation>().rectAnimation(passNode1, passNode2);
//        setLineArray(passNode1, passNode2);
//    }
//
//
//    private void createAndDestroyObjectForLineRender()
//    {
//        GameObject newPathAnimation = new GameObject();
//        GameObject pathAnimation = new GameObject();
//        newPathAnimation.AddComponent<RectTransform>();
//        newPathAnimation.name = "newPathAnimation" + passNode1.ToString() + passNode2.ToString();
//        newPathAnimation.AddComponent<LineRenderer>();
//        newPathAnimation.AddComponent<LineAnimation>();
//		//newPathAnimation.GetComponent<LineRenderer> ().useWorldSpace = false;
//		newPathAnimation.GetComponent<RectTransform>().Translate(new Vector3(0,0,-5f));
//        newPathAnimation.GetComponent<LineAnimation>().rectAnimation(passNode1, passNode2);
//        string name1 = "pathAnimation" + passNode1.ToString() + passNode2.ToString();
//        string name2 = "pathAnimation" + passNode2.ToString() + passNode1.ToString();
//        if (GameObject.Find(name1) != null)
//        {
//            pathAnimation = GameObject.Find(name1);
//        }else
//        {
//            pathAnimation = GameObject.Find(name2);
//        }
//		pathAnimation.GetComponent<LineRenderer> ().enabled = false;
//        StartCoroutine(waitAnim(newPathAnimation,pathAnimation));
//    }

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

	private string pathName(int num1,int num2,bool[,] rgb){
		string name="";
		if (rgb[num1, num2]) {
			name = "newPathAnim" + num1.ToString () + num2.ToString ();
		} else {
			name= "pathAnim"+num1.ToString()+num2.ToString();
		}
		return name;
	}

    //use this function to make the function inside wait few seconds.
    IEnumerator waitAnim(GameObject obj1,GameObject obj2)
    {
        //print(Time.time);
        yield return new WaitForSeconds(2);
        Destroy(obj1);
        setLineArray(passNode1, passNode2);
        //print(Time.time);
        LineRenderer lr = obj2.GetComponent<LineRenderer>();
        if (blueLineArray[passNode1, passNode2] && redLineArray[passNode1, passNode2] && !greenLineArray[passNode1, passNode2])
        {
            lr.material = Resources.Load<Material>("Materials/GradientRB") as Material;
			lr.enabled = true;
        }

        if (!blueLineArray[passNode1, passNode2] && redLineArray[passNode1, passNode2] && greenLineArray[passNode1, passNode2])
        {
            lr.material = Resources.Load<Material>("Materials/GradientRG") as Material;
			lr.enabled = true;
        }

        if (blueLineArray[passNode1, passNode2] && !redLineArray[passNode1, passNode2] && greenLineArray[passNode1, passNode2])
        {
            lr.material = Resources.Load<Material>("Materials/GradientBG") as Material;
			lr.enabled = true;
        }

        if (blueLineArray[passNode1, passNode2] && redLineArray[passNode1, passNode2] && greenLineArray[passNode1, passNode2])
        {
            lr.material = Resources.Load<Material>("Materials/GradientRGB") as Material;
			lr.enabled = true;
        }
    }

    private bool pathAlreadyExist(int num1, int num2, bool red, bool green, bool blue)
    {
        if (red)
        {
            if (redLineArray[num1,num2])
            {
                return true;
            }
        }
        if (blue)
        {
            if (blueLineArray[num1, num2])
            {
                return true;
            }
        }
        if (green)
        {
            if (greenLineArray[num1, num2])
            {
                return true;
            }
        }
        return false;    
    }

	private Vector2 getVector(Vector2 v1,Vector2 v2, Vector2 v3){
		Vector2 res = new Vector2 ((v1.x + v2.x + v3.x) / 3, (v1.y + v2.y + v3.y) / 3);
		return res;
	}

	//this function compare the nearest position of checkmark
	private static Vector2 getSmallest(Vector2 f1,Vector2 f2,Vector2 f3,float x, float y){
		if (Mathf.Abs(f1.x-x)+ Mathf.Abs(f1.y-y)<= Mathf.Abs(f2.x-x)+ Mathf.Abs(f2.y-y) && Mathf.Abs(f1.x-x)+ Mathf.Abs(f1.y-y) <=Mathf.Abs(f3.x-x)+ Mathf.Abs(f3.y-y)) {
			return f1;
		} else if (Mathf.Abs(f2.x-x)+ Mathf.Abs(f2.y-y) <= Mathf.Abs(f3.x-x)+ Mathf.Abs(f3.y-y)) {
			return f2;
		} else {
			return f3;
		}
	}
		

    private static void setConfirmPathButton(List<int> al)
    {
		float x = 0.0f;
		float y = 0.0f;
		HashSet<int> set = new HashSet <int> ();
		foreach (int num in al) {
			if (set.Add (num)) {
				string numStr = "node" + num.ToString ();
				if (num == 1) {
					numStr = "depot";
				}
				x += GameObject.Find (numStr).transform.position.x;
				y += GameObject.Find (numStr).transform.position.y;
			}
		}
		nodeCount = al.Count;
		nodeArray = new int[nodeCount];
		//IEnumerator e = al.GetEnumerator ();
//		while (e.MoveNext ()) {
//			nodeArray [i] = e.Current;
//			i++;
//		}
		int i=0;
		foreach(int value in al){
			nodeArray [i] = value;
			i++;
		}
		int count=set.Count;
		x = x / count;
		y = y / count;
		float z = 100f;
		Vector2 v4 = getSmallest (v1, v2, v3, x, y);
		x = v4.x;
		y = v4.y;
		GameObject checkMark = new GameObject ();
        checkMark.name = "checkMark";
		Transform parentTransform = GameObject.Find ("gamePanel").GetComponent<Transform> ();
		checkMark.transform.SetParent (parentTransform);
		checkMark.transform.position = new Vector3 (x, y, z);
		checkMark.transform.localScale = new Vector3 (0.5f, 0.5f, 1f);
		BoxCollider2D collider = checkMark.AddComponent<BoxCollider2D> ();
		collider.enabled = true;
		collider.size = new Vector2 (100, 100);
		Image cm = checkMark.AddComponent<Image> ();
		cm.sprite = Resources.Load<Sprite> ("Image/checkmark") as Sprite;
		//cm.color = new Color (1, 0, 0);
		checkMark.AddComponent<CheckMark> ();
		checkMark.AddComponent<AudioSource> ();
		checkMark.GetComponent<AudioSource> ().clip = Resources.Load<AudioClip> ("Audio/success1");
		checkMark.GetComponent<AudioSource> ().volume = 0.8f;
		checkMark.GetComponent<AudioSource> ().playOnAwake = false;
    }

    public void setLineArray(int num1,int num2)
    {
        if (gameControll.redTruck)
        {
            redLineArray[num1, num2] = true;
            redLineArray[num2, num1] = true;
        }
        if (gameControll.blueTruck)
        {
            blueLineArray[num1, num2] = true;
            blueLineArray[num2, num1] = true;
        }
        if (gameControll.greenTruck)
        {
            greenLineArray[num2, num1] = true;
            greenLineArray[num1, num2] = true;
        }
    }


	private IEnumerable<int> validPathAnimation(int num1){
		for (int i = 0; i < gameControll.nodePath.GetLength (1); i++) {
			if (gameControll.nodePath [num1, i]) {
				yield return i;
			}
		}
	}

	public bool RedN{
		get{
			return this.redN;
		}

		set{
			this.redN = value;
		}
	}

	public bool GreenN{
		get{
			return this.greenN;
		}

		set{
			this.greenN = value;
		}
	}

	public bool BlueN{
		get{
			return this.blueN;
		}

		set{
			this.blueN = value;
		}
	}
	public bool RBN{
		get{
			return this.rbN;
		}

		set{
			this.rbN = value;
		}
	}

	public bool RGN{
		get{
			return this.rgN;
		}

		set{
			this.rgN = value;
		}
	}
		
	public bool GBN{
		get{
			return this.gbN;
		}

		set{
			this.gbN = value;
		}
	}

	public bool RGBN{
		get{
			return this.rgbN;
		}

		set{
			this.rgbN = value;
		}
	}
		
}