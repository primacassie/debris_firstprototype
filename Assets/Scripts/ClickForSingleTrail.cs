using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine.UI;

public class ClickForSingleTrail : ClickTrails {

	//bool startAnim;
	//bool closeAnim;
	//Vector3 firstObj;
	//float height;
	//int countClick;

	//void Start(){
 //       firstObj = GameObject.Find("singleTrail1").transform.position;
 //       height = GameObject.Find("singleTrail1").GetComponent<RectTransform>().rect.y;
 //       countClick = 0;
	//}

	//void Update(){
	//	if (startAnim) {
	//		AnimStart ();
	//	}
	//	if (closeAnim) {
	//		AnimClose ();
	//	}
	//}

	//void OnMouseDown(){
	//	int num = submitButton.ForTrailRed.Count;
	//	if (num > 1) {
	//		countClick++;
	//		if (countClick % 2 == 1) {
	//			startAnim = true;
	//			closeAnim = false;
	//		} else if (countClick % 2 == 0) {
	//			closeAnim = true;
	//			startAnim = false;
	//		}
	//	}
	//}

	//void AnimStart(){
	//	//int num = submitButton.ForTrailRed.Count;
	//	GameObject[] objArr = GameObject.FindGameObjectsWithTag ("Trails");
	//	foreach (GameObject obj in objArr) {
 //           obj.GetComponent<Image>().enabled = true;
 //           obj.GetComponent<BoxCollider2D>().enabled = true;
	//		string name = obj.name;
	//		string resultString = Regex.Match(name, @"\d+").Value;
	//		int i = int.Parse (resultString);
 //           Vector3 newPos = new Vector3 (firstObj.x, firstObj.y + 5 ,0);
	//		if (i != 1) {
	//			obj.transform.position = Vector3.MoveTowards(obj.transform.position,newPos,3*Time.deltaTime);
	//		}
	//	}
	//}

	//void AnimClose(){
	//	//int num = submitButton.ForTrailRed.Count;
	//	GameObject[] objArr = GameObject.FindGameObjectsWithTag ("Trails");
	//	foreach (GameObject obj in objArr) {
	//		string name = obj.name;
	//		string resultString = Regex.Match(name, @"\d+").Value;
	//		int i = int.Parse (resultString);
	//		if (i != 1 && obj.transform.position!=firstObj) {
	//			obj.transform.position = Vector3.MoveTowards(obj.transform.position,firstObj,3*Time.deltaTime);
	//		}
 //           if (obj.transform.position == firstObj && i!=1)
 //           {
 //               obj.GetComponent<Image>().enabled = false;
 //               obj.GetComponent<BoxCollider2D>().enabled = false;
 //           }
	//	}
	//}
}
