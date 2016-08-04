using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MenuToStart : MonoBehaviour {
	void OnMouseDown(){
//		this.gameObject.AddComponent<AudioSource> ();
//		AudioSource as= this.gameObject.GetComponent<AudioSource>();
//		= Resources.Load<AudioSource> ("Audio/click2");
		this.gameObject.GetComponent<AudioSource>().Play();
		SceneManager.LoadScene ("start");
	}
}
