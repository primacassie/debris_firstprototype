using UnityEngine;
using System.Collections;

public class NodeAnimation : MonoBehaviour {

	Animator anim;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		anim.enabled = false;
	}

	public void redAnimation(){
		anim.enabled = true;
		anim.Play ("NodeAnimR");
	}

	public void greenAnimation(){
		anim.enabled = true;
		anim.Play ("NodeAnimG");
	}

	public void blueAnimation(){
		anim.enabled = true;
		anim.Play ("NodeAnimB");
	}
}