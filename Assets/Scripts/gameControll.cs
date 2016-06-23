using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class gameControll : MonoBehaviour {

	// Use this for initialization
	[HideInInspector]public static ArrayList pathStore = new ArrayList();
	void Start () {
		GameObject[] path = GameObject.FindGameObjectsWithTag ("path");
		foreach (GameObject go in path) {
			pathStore.Add (go.GetComponent<Graph> ().node);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool redTruck;
	public bool blueTruck;
	public bool greenTruck;
	public Texture2D cursorTextureR;
	public Texture2D cursorTextureG;
	public Texture2D cursorTextureB;
	public CursorMode cursorMode = CursorMode.Auto;
	public Vector2 hotSpot = Vector2.zero;
	public Image depot;
	public Node node;

	public static Queue<int> twoNode=new Queue<int>();

	public void changeRed(){
		Cursor.SetCursor(cursorTextureR, hotSpot, cursorMode);
		depot.sprite = Resources.Load<Sprite> ("Image/truck_R256") as Sprite;
		redTruck = true;
		blueTruck = false;
		greenTruck = false;
	}

	public void changeBlue(){
		Cursor.SetCursor(cursorTextureB, hotSpot, cursorMode);
		depot.sprite = Resources.Load<Sprite> ("Image/truck_B256") as Sprite;
		blueTruck = true;
		redTruck = false;
		greenTruck = false;
	}

	public void changeGreen(){
		Cursor.SetCursor(cursorTextureG, hotSpot, cursorMode);
		depot.sprite = Resources.Load<Sprite> ("Image/truck_G256") as Sprite;
		greenTruck = true;
		blueTruck = false;
		redTruck = false;
	}

	public static bool validPath(int num1, int num2){
		foreach (int[] ar in pathStore) {
			Debug.Log ("ar1 == " + ar [0] + " ar2== " + ar [1]);
			if ((num1 == ar [0] && num2 == ar [1]) || (num1 == ar [1] && num2 == ar [0])) {
				Debug.Log ("true");
				return true;
			}
		}
		Debug.Log ("false");
		return false;
	}
		
}
