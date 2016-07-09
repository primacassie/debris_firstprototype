using UnityEngine;
using System.Collections;

public class depotAnimation : MonoBehaviour {

	Animator anim;
	private bool blueA;
	private bool greenA;
	private bool redA;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		anim.enabled = false;
	}
	
	public void redAnimation(){
		anim.enabled = true;
		anim.Play ("depotAnimR");
	}

	public void greenAnimation(){
		anim.enabled = true;
		anim.Play ("depotAnimG");
	}

	public void blueAnimation(){
		anim.enabled = true;
		anim.Play ("depotAnimB");
	}
}
