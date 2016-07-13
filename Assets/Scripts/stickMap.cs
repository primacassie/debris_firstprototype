using UnityEngine;
using System.Collections;

public class stickMap : MonoBehaviour {
	private GameObject rm;   //get the right mini map panel
	private Camera cam;
	Vector3 screenPos;

	void Start(){
		rm= GameObject.Find("rightMap");
		cam = GetComponent<Camera> ();
		//Debug.Log (rm.transform.position);
		screenPos=cam.WorldToScreenPoint(rm.transform.position);
	}

	void Update(){
		stick ();
	}

	private void stick(){
		int camSize = (int) cam.orthographicSize;
		rm.transform.position = cam.ScreenToWorldPoint (screenPos);
//		if (camSize != 5) {
//			rm.transform.position = cam.ScreenToWorldPoint (screenPos);
//		}
		switch (camSize) {
		case 4:
			rm.transform.localScale = new Vector2 (0.8f, 0.8f);
			break;
		case 3:
			rm.transform.localScale = new Vector2 (0.6f, 0.6f);
			break;
		case 2:
			rm.transform.localScale = new Vector2 (0.4f, 0.4f);
			break;
		case 1:
			rm.transform.localScale = new Vector2 (0.2f, 0.2f);
			break;
		default:
			rm.transform.localScale = new Vector2 (1f, 1f);
			break;
		}
	}
}