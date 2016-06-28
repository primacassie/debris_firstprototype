using UnityEngine;
using System.Collections;

public class Graph :MonoBehaviour {
	public int capacity;
	public float time;
	public int[] node = new int[2];

	void Awake(){
		//time = 2 * capacity;
	}
}
