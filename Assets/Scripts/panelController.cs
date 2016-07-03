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
	public static Text redTextOnce, blueTextOnce, greenTextOnce;
	public static Text redTimeOnce, blueTimeOnce, greenTimeOnce;
	void Start () {
		GameObject redTruckProfit = GameObject.Find ("redTruckProfit");
		GameObject blueTruckProfit = GameObject.Find ("blueTruckProfit");
		GameObject greenTruckProfit = GameObject.Find ("greenTruckProfit");
		GameObject redTruckProfitOnce = GameObject.Find ("redTruckProfitOnce");
		GameObject blueTruckProfitOnce = GameObject.Find ("blueTruckProfitOnce");
		GameObject greenTruckProfitOnce = GameObject.Find ("greenTruckProfitOnce");
		GameObject redTruckTime = GameObject.Find ("redTruckTime");
		GameObject blueTruckTime = GameObject.Find ("blueTruckTime");
		GameObject greenTruckTime = GameObject.Find ("greenTruckTime");
		GameObject redTruckTimeOnce = GameObject.Find ("redTruckTimeOnce");
		GameObject blueTruckTimeOnce = GameObject.Find ("blueTruckTimeOnce");
		GameObject greenTruckTimeOnce = GameObject.Find ("greenTruckTimeOnce");
		redText = redTruckProfit.GetComponent<Text> ();
		blueText = blueTruckProfit.GetComponent<Text> ();
		greenText = greenTruckProfit.GetComponent<Text> ();
		redTime = redTruckTime.GetComponent<Text> ();
	    blueTime = blueTruckTime.GetComponent<Text> ();
		greenTime = greenTruckTime.GetComponent<Text> ();
		redTextOnce = redTruckProfitOnce.GetComponent<Text> ();
		redTimeOnce = redTruckTimeOnce.GetComponent<Text> ();
		blueTextOnce = blueTruckProfitOnce.GetComponent<Text> ();
		blueTimeOnce = blueTruckTimeOnce.GetComponent<Text> ();
		greenTextOnce = greenTruckTimeOnce.GetComponent<Text> ();
		greenTimeOnce = greenTruckTimeOnce.GetComponent<Text> ();
		redText.text = i.ToString();
		blueText.text = i.ToString();
		greenText.text = i.ToString();
		redTime.text = i.ToString();
		blueTime.text = i.ToString();
		greenTime.text = i.ToString();
		greenTextOnce.text = i.ToString ();
		greenTimeOnce.text = i.ToString ();
		blueTextOnce.text = i.ToString ();
		blueTimeOnce.text = i.ToString ();
		redTextOnce.text = i.ToString ();
		redTimeOnce.text = i.ToString ();
	}
		
}
