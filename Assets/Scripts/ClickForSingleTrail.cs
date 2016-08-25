using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class ClickForSingleTrail : ClickTrails {

	bool startAnim;
	bool closeAnim;
	Vector3 firstObj;
	float height;
	int countClick;

	void Start(){
		firstObj = GameObject.Find ("singleTrail1").transform.position;
		height = GameObject.Find ("singleTrail1").GetComponent<RectTransform> ().rect.height;
		countClick = 0;
	}

	void Update(){
		if (startAnim) {
			AnimStart ();
		}
		if (closeAnim) {
			AnimClose ();
		}
	}

	void OnMouseDown(){
		int num = submitButton.ForTrailRed.Count;
		if (num > 1) {
			countClick++;
			if (countClick % 2 == 1) {
				startAnim = true;
				closeAnim = false;
			} else if (countClick % 2 == 0) {
				closeAnim = true;
				startAnim = false;
			}
		}
	}

	void AnimStart(){
		//int num = submitButton.ForTrailRed.Count;
		GameObject[] objArr = GameObject.FindGameObjectsWithTag ("Trails");
		foreach (GameObject obj in objArr) {
			string name = obj.name;
			string resultString = Regex.Match(name, @"\d+").Value;
			int i = int.Parse (resultString);
			Vector3 newPos = new Vector3 (firstObj.x, firstObj.y + (height+10) * (i - 1),0);
			if (i != 1 && obj.transform.position!=newPos) {
				obj.transform.position = Vector3.MoveTowards(obj.transform.position,newPos,2*Time.deltaTime);
			}
		}
	}

	void AnimClose(){
		//int num = submitButton.ForTrailRed.Count;
		GameObject[] objArr = GameObject.FindGameObjectsWithTag ("Trails");
		foreach (GameObject obj in objArr) {
			string name = obj.name;
			string resultString = Regex.Match(name, @"\d+").Value;
			int i = int.Parse (resultString);
			if (i != 1 && obj.transform.position!=firstObj) {
				obj.transform.position = Vector3.MoveTowards(obj.transform.position,firstObj,2*Time.deltaTime);
			}
		}
	}
}
