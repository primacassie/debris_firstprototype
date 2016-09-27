using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SimpleJSON;

public class slider : MonoBehaviour {
	//GameObject thisObj;
	public static int capOrigin;
	public static int carOrigin;
	
	public void sliderCap(float newCap){
		JSONClass details = new JSONClass ();
		details ["ClickSlider"] = newCap.ToString();
		TheLogger.instance.TakeAction (3, details);
		gameControll.capArray [Node.passNode1, Node.passNode2] = (int) newCap;
		gameControll.capArray [Node.passNode2, Node.passNode1] = (int) newCap;
		//Debug.Log ("carOrigin is "+carOrigin);
		pathCap.rightObj.GetComponentInChildren<Text>().text=gameControll.capArray [Node.passNode1, Node.passNode2].ToString();
		Node.modifyBySlider (capOrigin, gameControll.capArray [Node.passNode1, Node.passNode2],carOrigin);
	}

//	void Update(){
//		if (gameControll.carCap == 0) {
//			if (capOrigin < gameControll.capArray [Node.passNode1, Node.passNode2])
//				pathCap.sl.enabled = true;
//			if (capOrigin > gameControll.capArray [Node.passNode1, Node.passNode2])
//				pathCap.sl.enabled = false;
//		}
//	}
}

