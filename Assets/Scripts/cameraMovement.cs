using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class cameraMovement : MonoBehaviour {
	private float cam;
	private Slider sl;
	private float lastNum;

	void Start(){
		sl = GameObject.Find ("zoom").GetComponent<Slider>();
		sl.maxValue = Camera.main.orthographicSize;
		sl.minValue = 1;
		sl.value = sl.maxValue;
		lastNum = sl.maxValue;
	}

	public void zoom(float cameraView){
		if (cameraView < lastNum && Camera.main.orthographicSize>1) {
			Camera.main.orthographicSize--;
		} else if(cameraView>lastNum && Camera.main.orthographicSize<5){
			Camera.main.orthographicSize++;
		}
	}


}
