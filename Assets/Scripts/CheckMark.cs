using UnityEngine;
using System.Collections;

public class CheckMark : MonoBehaviour {

	public static bool nextStep;

	void OnMouseDown(){
		nextStep = true;
		Destroy (this.gameObject);
	}
}
