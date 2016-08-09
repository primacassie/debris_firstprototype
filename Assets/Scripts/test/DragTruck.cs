using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class DragTruck: MonoBehaviour {
	void Update(){
		float slope = 1.0f;
		Vector3 dir= new Vector3 (0, 0,360-Mathf.Atan (slope) * Mathf.Rad2Deg);
		Debug.Log (dir);
		if (Mathf.Abs(transform.eulerAngles.z-dir.z)>1.0f) {
			transform.Rotate (new Vector3 (0, 0, 0.5f));
		}
		//Debug.Log (transform.eulerAngles);
		if (Mathf.Abs(transform.eulerAngles.z-dir.z)<1.0f) {
			transform.Translate (-0.01f, 0, 0);
		}
	}
}