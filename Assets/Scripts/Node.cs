using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
    public int num;

    //static int variable control intersection;
    public static int intersection;
    //private Text sIntersection;

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

    //this array store the gameobject represent the capacity of each path.
    private static GameObject[] capPath;

    //basic value that the scroll bar need to add
    private static float scrollBasicProfit;
    private static float scrollBasicProfitOnce;
    private static float scrollBasicTime;
    private static float scrollBasicTimeOnce;

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
    private static List<List<int>> redAl;
    private static List<List<int>> blueAl;
    private static List<List<int>> greenAl;

    private static List<int> storePath;


    void Awake()
    {
        capPath = GameObject.FindGameObjectsWithTag("cap");
        foreach (GameObject obj in capPath)
        {
            //obj.GetComponent<Text> ().text = "50";
            obj.GetComponentInChildren<Text>().text = "50";
        }
		redAl   = new List<List<int>> ();
		blueAl  = new List<List<int>> ();
		greenAl = new List<List<int>> ();
        //sIntersection = GameObject.Find ("intersection").GetComponent<Text> ();
    }

	void Update(){
		if (CheckMark.nextStep == true) {
			backToDepot = false;
			if (gameControll.blueTruck) {
				blueAl.Add (storePath);
				gameControll.blueTruck = false;
				gameControll.blueProfitOnce = 0;
				gameControll.blueTimeOnce = 0;
			}
			if (gameControll.redTruck) {
				redAl.Add (storePath);
				gameControll.redTruck = false;
				gameControll.redProfitOnce = 0;
				gameControll.redTimeOnce = 0;
			}
			if (gameControll.greenTruck) {
				greenAl.Add (storePath);
				gameControll.greenTruck = false;
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
			GameObject.Find ("GameController").GetComponent<gameControll> ().resetCursor ();
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

        if (size == 0)
        {
            if (num != 1 && (gameControll.redTruck || gameControll.blueTruck || gameControll.greenTruck))
            {
                Debug.Log("Please click depot firstly to start!");
                //call modal control here
                //GameObject.Find ("ModalControl").GetComponent<testWindow> ().takeAction ("Please select depot as the start!");

            }
            else if (num == 1 && !(gameControll.redTruck || gameControll.blueTruck || gameControll.greenTruck))
            {
                Debug.Log("please select a truck!");

                //call modal control here
                //GameObject.Find ("ModalControl").GetComponent<testWindow> ().takeAction ("Please select a truck!");

            }
            else if ((gameControll.redTruck || gameControll.blueTruck || gameControll.greenTruck) && num == 1)
            {
                storePath = new List<int>();
                storePath.Add(1);
                gameControll.twoNode.Enqueue(num);
                Debug.Log("let's start!");

                //gameControll.saveToFile ("start from depot!");

                //try to make a fade text here
                //displayManager = DisplayManager.Instance ();
                //displayManager.DisplayMessage ("Start!!!");
            }
        }
        else if (size == 1)
        {
            int firstOfSize1 = t.Peek();
            if (gameControll.validPath(firstOfSize1, num))
            {
                gameControll.twoNode.Enqueue(num);
                Debug.Log("this is the node" + num);
                size2lastNode = num;
                //string processtoSave = "connect depot to node " + num.ToString ();
                //gameControll.saveToFile (processtoSave);
                //Debug.Log (size2lastNode);

                //change the image of node here
                clickChangeColor();

                //here get the capacity of the path
                passNode1 = firstOfSize1;
                passNode2 = num;

                //add node number to store the path
                storePath.Add(passNode2);

                //this function used to create new game object to realize the line render.
                createObjectForLineRender();

                //here is the second version of UI design
                setInitialValue(passNode1, passNode2);
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
            //temp = gameControll.twoNode.Dequeue ();
            //Debug.Log ("remove" + temp);
            //Debug.Log(size2lastNode);

			if (gameControll.validPath(size2lastNode, num) && !backToDepot)
            {
                gameControll.twoNode.Dequeue();
                //Debug.Log ("remove " + temp);
                pathCap.desableSlider();
                gameControll.twoNode.Enqueue(num);
                passNode1 = size2lastNode;
                passNode2 = num;
                //				GameObject inputTab=GameObject.Find("InputTab");
                //				inputTab.SetActive (true);

                //change node color here
                clickChangeColor();


                //this function used to create new game object to realize the line render.
                if (!pathAlreadyExist(storePath, passNode1, passNode2))
                {
                    createObjectForLineRender();
                }

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
            scrollBasicTimeOnce = gameControll.redTimeOnce;
            string findName = "blueTruckText" + (gameControll.blueTruckNum - 1).ToString();
            int truckS = 100 - gameControll.carCap;
            string truckStore = truckS + "/100";
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
                changeNodeColor("RB");
                blueN = true;
                rbN = true;
            }
            else if (gameControll.greenTruck)
            {
                changeNodeColor("RG");
                greenN = true;
                rgN = true;
            }
        }
        else if (blueN && !(greenN || redN || rgN || rbN || gbN || rgbN))
        {
            if (gameControll.redTruck)
            {
                changeNodeColor("RB");
                redN = true;
                rbN = true;
            }
            else if (gameControll.greenTruck)
            {
                changeNodeColor("GB");
                greenN = true;
                gbN = true;
            }
        }
        else if (greenN && !(blueN || redN || rgN || rbN || gbN || rgbN))
        {
            if (gameControll.redTruck)
            {
                changeNodeColor("RG");
                redN = true;
                rgN = true;
            }
            else if (gameControll.blueTruck)
            {
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

    private void createObjectForLineRender()
    {
        GameObject pathAnimation = new GameObject();
        pathAnimation.AddComponent<RectTransform>();
        pathAnimation.name = "pathAnimation" + passNode1.ToString() + passNode2.ToString();
        pathAnimation.AddComponent<LineRenderer>();
        pathAnimation.AddComponent<LineAnimation>();
        pathAnimation.GetComponent<LineAnimation>().rectAnimation(passNode1, passNode2);
    }

    private bool pathAlreadyExist(List<int> al, int num1, int num2)
    {
        for (int i = 0; i < al.Count - 1; i++)
        {
            if ((al[i] == num1 && al[i + 1] == num2) || (al[i] == num2 && al[i + 1] == num1))
            {
                return true;
            }
        }
        return false;
    }

	private bool otherColorInthePath(){
		
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
		int count=set.Count;
		x = x / count;
		y = y / count;
		float z = 100f;
		GameObject checkMark = new GameObject ();
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
    }
}
