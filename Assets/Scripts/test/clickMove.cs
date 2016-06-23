using UnityEngine;
using System.Collections;

public class clickMove : MonoBehaviour {

	// Use this for initialization
	//Vector3 newPosition;
	void Start () {
		GameObject[] path = GameObject.FindGameObjectsWithTag ("path");
		ArrayList pathStore = new ArrayList();
		foreach (GameObject go in path) {
			pathStore.Add (go.GetComponent<Graph> ().node);
		}
		foreach (int[] ar in pathStore) {
			Debug.Log (ar [0] + " and " + ar [1]); 
		}
	}

	public void OnMouseDown(){
		Debug.Log ("123");
	}
	
	// Update is called once per frame
//	void Update () {
//		if (Input.GetMouseButtonDown (0)) {
//			transform.position = Input.mousePosition;
//		}
//	}
}
