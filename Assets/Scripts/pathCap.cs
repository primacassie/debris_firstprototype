using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class pathCap : MonoBehaviour {
	public int[] node = new int[2];


//	[HideInInspector]public Texture2D cursorScroller = GameObject.Find ("GameController").GetComponent<gameControll> ().cursorTextureScroller;
//	[HideInInspector]public Texture2D cursorR = GameObject.Find ("GameController").GetComponent<gameControll> ().cursorTextureR;
//	[HideInInspector]public Texture2D cursorB = GameObject.Find ("GameController").GetComponent<gameControll> ().cursorTextureB;
//	[HideInInspector]public Texture2D cursorG = GameObject.Find ("GameController").GetComponent<gameControll> ().cursorTextureG;
//	[HideInInspector]public Texture2D cursorO = GameObject.Find ("GameController").GetComponent<gameControll> ().cursorTextureO;
	[HideInInspector]public CursorMode cursorMode = CursorMode.Auto;
	[HideInInspector]public Vector2 hotSpot = Vector2.zero;
	[HideInInspector]private bool mouseIn = false;
	[HideInInspector]public static GameObject rightObj;

	//store objects with capacity number
	private static GameObject[] caps;

	//make a function only call once in update
	private bool onlyFirst;
	//make a function only call when they enter the object after the first time.
//	private bool afterFirst;

	//remember the y position of mouse at the first time
	private int yPos=0;
	private Text changeCap;
	private int wholeCap;

	//remeber when mouse is holding while onmouseexit
	private bool onExitHold;

	//bool which decide which time to run
	//private bool timeFix;

	//bool that a truck should be assigned
	//private bool hasTruck;

	//remember the number of last frame
	private int lastTemp;

	//initalize slider of this pathCap here
	public static Slider sl;


	void Start(){
		caps=GameObject.FindGameObjectsWithTag("cap");

		//try to create path animation here
	}

	void FixedUpdate(){
		//Debug.Log (Time.fixedTime);
		Time.fixedDeltaTime=0.4f;
		//timeFix = true;
//		if (Time.fixedTime % 0.125 == 0.0) {
//			timeFix = true;
//		} else {
//			timeFix = false;
//		}
		findObject();
		if (Input.GetMouseButton (0) && !onlyFirst && (mouseIn||onExitHold) &&this.gameObject.Equals(rightObj)) {
			//Debug.Log ("111");
			if (gameControll.redTruck) {
				GameObject.Find ("GameController").GetComponent<gameControll> ().changeScrollerR ();
			}

			if (gameControll.blueTruck) {
				GameObject.Find ("GameController").GetComponent<gameControll> ().changeScrollerB ();
			}

			if (gameControll.greenTruck) {
				GameObject.Find ("GameController").GetComponent<gameControll> ().changeScrollerG ();
			}

			//GameObject.Find ("GameController").GetComponent<gameControll> ().changeScroller ();
			Vector2 mouse =Input.mousePosition;
			yPos = (int) mouse.y;
			changeCap = this.gameObject.GetComponentInChildren<Text> ();
			//wholeCap = int.Parse (changeCap.text);
			wholeCap=slider.capOrigin;
			lastTemp = int.Parse (changeCap.text);
			//Debug.Log ("wholeCap is "+wholeCap);
			onlyFirst = true;
		}
			
		
		if (Input.GetMouseButton (0) && (mouseIn||onExitHold) &&this.gameObject.Equals(rightObj)) {
			if (gameControll.redTruck) {
				GameObject.Find ("GameController").GetComponent<gameControll> ().changeScrollerR ();
			}

			if (gameControll.blueTruck) {
				GameObject.Find ("GameController").GetComponent<gameControll> ().changeScrollerB ();
			}

			if (gameControll.greenTruck) {
				GameObject.Find ("GameController").GetComponent<gameControll> ().changeScrollerG ();
			}
			int temp = int.Parse (changeCap.text);
			Vector2 mouseNow = Input.mousePosition;
			int mouseY = (int) mouseNow.y;
			//int change1 = (mouseY - yPos) / 10;   //change of the number which player want
			if ((mouseY > yPos) && (temp < wholeCap)) {
				temp += 1;
				changeCap.text = temp.ToString ();
			}
			//int change2 = (yPos - mouseY) / 10;
			if (temp > 0 && (yPos > mouseY)) {
				temp -= 1;
				changeCap.text = temp.ToString ();
			}
			//int truckDebris = wholeCap - temp;   //total changes compared with the origin.
			int numDebris=lastTemp-temp;
			//numbers compared with last frame
			Node.modifyInUpdate(numDebris,Node.passNode1,Node.passNode2);
			lastTemp = temp;
			//Debug.Log ("temp is "+temp);
		}

		//while mouse is not holding and already exit
		if (!Input.GetMouseButton (0) && (!mouseIn)) {
			if (gameControll.redTruck && !mouseIn) {
				Texture2D cursorR = GameObject.Find ("GameController").GetComponent<gameControll> ().cursorTextureR;
				Cursor.SetCursor (cursorR, hotSpot, cursorMode);
			} else if (gameControll.blueTruck && !mouseIn) {
				Texture2D cursorB = GameObject.Find ("GameController").GetComponent<gameControll> ().cursorTextureB;
				Cursor.SetCursor (cursorB, hotSpot, cursorMode);
			} else if (gameControll.greenTruck  && !mouseIn) {
				Texture2D cursorG = GameObject.Find ("GameController").GetComponent<gameControll> ().cursorTextureG;
				Cursor.SetCursor (cursorG, hotSpot, cursorMode);
			} else {
				Texture2D cursorO = GameObject.Find ("GameController").GetComponent<gameControll> ().cursorTextureO;
				Cursor.SetCursor (cursorO, hotSpot, cursorMode);
			}
			onExitHold = false;
		}
	}



	void OnMouseEnter(){
		if (gameControll.redTruck || gameControll.blueTruck || gameControll.greenTruck) {
			mouseIn = true;
		}
	}

	void OnMouseExit(){
		mouseIn = false;
		if (Input.GetMouseButton (0) && (gameControll.redTruck || gameControll.blueTruck || gameControll.greenTruck)) {
			onExitHold = true;
		}
	}

	//return the path.
	public static GameObject findObject(){
		foreach (GameObject obj in caps) {
			if ((obj.gameObject.GetComponent<pathCap> ().node [0] == Node.passNode1 && obj.gameObject.GetComponent<pathCap> ().node [1] == Node.passNode2)
				|| (obj.gameObject.GetComponent<pathCap> ().node [1] == Node.passNode1 && obj.gameObject.GetComponent<pathCap> ().node [0] == Node.passNode2)) {
				rightObj = obj;
				break;
			}
		}
		return rightObj;
	}

	public static void initializeSlider(){
		//inital slider value here
		//as I enabled the slider first so the function in slider invoke immediately, so I need to 
		//assign value to the variables in the slider first and then initialize it.
		slider.carOrigin = gameControll.carCap;
		slider.capOrigin = gameControll.capArray [Node.passNode1, Node.passNode2];
		sl=findObject().GetComponentInChildren<Slider>();
		sl.enabled = true;
		sl.GetComponent<RectTransform> ().localScale = new Vector2 (1, 1);
		sl.maxValue = int.Parse (rightObj.GetComponentInChildren<Text> ().text);
		if (sl.maxValue - gameControll.carCap > 0) {
			sl.minValue = sl.maxValue - gameControll.carCap;
		} else {
			sl.minValue = 0;	
		}
		//sl.value = int.Parse (rightObj.GetComponentInChildren<Text> ().text);
		sl.value=sl.maxValue;
		//Debug.Log (gameControll.carCap + "in the slider car cap");
	}

	public static void desableSlider(){
		sl.enabled = false;
		sl.GetComponent<RectTransform> ().localScale = new Vector2 (0, 0);
	}
}
