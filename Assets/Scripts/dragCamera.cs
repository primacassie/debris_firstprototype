using UnityEngine;
using System.Collections;

public class dragCamera : MonoBehaviour {
	private Vector3 startPos;
	private Vector3 standardPos;


	void OnMouseDown(){
		startPos = Input.mousePosition;
	}

	private bool canMove(Vector3 nowPos){
		if (Mathf.Abs (nowPos.x - startPos.x) > 1f || Mathf.Abs (nowPos.y - startPos.y) > 1f)
			return true;
		else
			return false;
//			return false;
	}
		

	void OnMouseDrag(){
		//Time.timeScale = 4f;
		//Vector3 inter = new Vector3 (1f/60f*(pMouse.x-240.0f), 1f/60f*(pMouse.y-150f), Camera.main.transform.position.z);
		Vector3 pMouse = Input.mousePosition;
		int cameraSize = (int) Camera.main.orthographicSize;
		if (canMove (pMouse)) {
			Vector3 inter=new Vector3();
			int whichCase = 0;     //whichCase created here to control the move speed and move range of different size of camera
			switch (cameraSize) {
			case 1:
				whichCase = 1;
				inter = (Input.mousePosition - startPos) / 18.75f;
				goto case 0;
			case 2:
				whichCase = 2;
				inter = (Input.mousePosition - startPos) / 37.5f;
				goto case 0;
			case 3:
				whichCase = 3;
				inter = (Input.mousePosition - startPos) / 75;
				goto case 0;
			case 4:
				whichCase = 4;
				inter = (Input.mousePosition - startPos) / 150;
				//Debug.Log (Camera.main.transform.position.x);
				goto case 0;
			case 0:
				if ((Mathf.Abs (Camera.main.transform.position.x) <= 1.6*(5-whichCase) && Mathf.Abs (Camera.main.transform.position.y) <= 1*(5-whichCase))
					|| (Mathf.Abs (Camera.main.transform.position.x) > 1.6*(5-whichCase) && Mathf.Abs (Camera.main.transform.position.y) <= 1*(5-whichCase) && Mathf.Abs (Camera.main.transform.position.x - inter.x) < Mathf.Abs (Camera.main.transform.position.x))
					|| (Mathf.Abs (Camera.main.transform.position.x) <= 1.6*(5-whichCase) && Mathf.Abs (Camera.main.transform.position.y) > 1*(5-whichCase) && Mathf.Abs (Camera.main.transform.position.y - inter.y) < Mathf.Abs (Camera.main.transform.position.y))
					|| (Mathf.Abs (Camera.main.transform.position.x) > 1.6*(5-whichCase) && Mathf.Abs (Camera.main.transform.position.y) > 1*(5-whichCase) && Mathf.Abs (Camera.main.transform.position.x - inter.x) < Mathf.Abs (Camera.main.transform.position.x) && Mathf.Abs (Camera.main.transform.position.y - inter.y) < Mathf.Abs (Camera.main.transform.position.y))) {
					Camera.main.transform.position -= (inter * ((float) whichCase*whichCase /50));  //use squre of which case to control the speed
					//Debug.Log ("first if");
				} else if (Camera.main.transform.position.x > 1.6*(5-whichCase) && Mathf.Abs (Camera.main.transform.position.y) <= 1*(5-whichCase) && inter.x < 0) {
					inter.x = 0;
					//Debug.Log ("second if");
					Camera.main.transform.position -= (inter * ((float) whichCase*whichCase /50));
				} else if (Camera.main.transform.position.x < -1.6*(5-whichCase) && Mathf.Abs (Camera.main.transform.position.y) <= 1*(5-whichCase) && inter.x > 0) {
					//Debug.Log ("third if");
					inter.x = 0;
					Camera.main.transform.position -= (inter * ((float) whichCase*whichCase /50));
				} else if (Mathf.Abs (Camera.main.transform.position.x) <= 1.6*(5-whichCase) && Camera.main.transform.position.y < -1*(5-whichCase) && inter.y > 0) {
					//Debug.Log ("fourth if");
					inter.y = 0;
					Camera.main.transform.position -= (inter * ((float) whichCase*whichCase /50));
				} else if (Mathf.Abs (Camera.main.transform.position.x) <= 1.6*(5-whichCase) && Camera.main.transform.position.y > 1*(5-whichCase) && inter.y < 0) {
					//Debug.Log ("5 if");
					inter.y = 0;
					Camera.main.transform.position -= (inter * ((float) whichCase*whichCase /50));
				}
				break;
			}
		}
	}

//	void OnMouseUp(){
//	}
}
