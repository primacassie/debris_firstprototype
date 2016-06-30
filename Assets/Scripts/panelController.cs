using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class panelController : MonoBehaviour {

	//private GameObject scoreBoard;
	// Use this for initialization
	private int i=0;

	//Script to access number of the capcity and time it collect;
	public static Text redText,blueText,greenText;
	public static Text redTime, blueTime, greenTime;
	void Start () {
		GameObject redTruckDebris = GameObject.Find ("redTruckDebris");
		GameObject blueTruckDebris = GameObject.Find ("blueTruckDebris");
		GameObject greenTruckDebris = GameObject.Find ("greenTruckDebris");
		GameObject redTruckTime = GameObject.Find ("redTruckTime");
		GameObject blueTruckTime = GameObject.Find ("blueTruckTime");
		GameObject greenTruckTime = GameObject.Find ("greenTruckTime");
		redText = redTruckDebris.GetComponent<Text> ();
		blueText = blueTruckDebris.GetComponent<Text> ();
		greenText = greenTruckDebris.GetComponent<Text> ();
		redTime = redTruckTime.GetComponent<Text> ();
		blueTime = blueTruckTime.GetComponent<Text> ();
		greenTime = greenTruckTime.GetComponent<Text> ();
		redText.text = i.ToString();
		blueText.text = i.ToString();
		greenText.text = i.ToString();
		redTime.text = i.ToString();
		blueTime.text = i.ToString();
		greenTime.text = i.ToString();
	}
		
}
