using UnityEngine;
using System.Collections;

public class depot : MonoBehaviour {
	private static bool haveClicked;

	private int count=0;

	[HideInInspector]public int start=0;
	public void OnClicked(){
		//bool red=GameObject.Find("GameController").GetComponent<gameControll>().redTruck;
		bool red=gameControll.redTruck;
		bool blue = gameControll.blueTruck;
		bool green = gameControll.greenTruck;
		//bool blue=GameObject.Find("GameController").GetComponent<gameControll>().blueTruck;
		//bool green=GameObject.Find("GameController").GetComponent<gameControll>().greenTruck;
		//GameObject obj=GameObject.FindGameObjectWithTag("Node");
		//int nodeNum = obj.GetComponentsInChildren<Node> ()[0];
		//Debug.Log (nodeNum);
		//Debug.Log(gameObject.tag);
		if ((red||blue||green) && haveClicked == true && count%2!=0) {
			Debug.Log ("finish this");
			haveClicked = false;
			count++;
			start = 0;
		} else if ((red||blue||green) && haveClicked == false) {
			haveClicked = true;
			count++;
			Debug.Log ("start this");
			start = 1;
		} else if (!(red||blue||green)) {
			Debug.Log ("please select a truck to start!");
		}
	}
}
