using UnityEngine;
using System.Collections;

public class depot : MonoBehaviour {
	private bool firstClicked;
	private int count=0;
	[HideInInspector]public int start=0;
	public void OnClicked(){
		bool red=GameObject.Find("GameController").GetComponent<gameControll>().redTruck;
		//bool blue=GameObject.Find("GameController").GetComponent<gameControll>().blueTruck;
		//bool green=GameObject.Find("GameController").GetComponent<gameControll>().greenTruck;
		//GameObject obj=GameObject.FindGameObjectWithTag("Node");
		//int nodeNum = obj.GetComponentsInChildren<Node> ()[0];
		//Debug.Log (nodeNum);
		//Debug.Log(gameObject.tag);
		if (red == true && firstClicked == true && count%2!=0) {
			Debug.Log ("finish this");
			firstClicked = false;
			count++;
			start = 0;
		} else if (red == true && firstClicked == false) {
			firstClicked = true;
			count++;
			Debug.Log ("start this");
			start = 1;
		} else if (red==false) {
			Debug.Log ("nothing");
		}
	}
}
