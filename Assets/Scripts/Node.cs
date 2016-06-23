using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node : MonoBehaviour {
	public int num;
	public Node(int n){
		this.num = n;
	}

	public int get(){
		return num;
	}

	void OnMouseDown(){
		//Debug.Log (num);
		int temp;
		Queue<int> t = gameControll.twoNode;
		int size = t.Count;
		if (size < 2) {
			gameControll.twoNode.Enqueue (num);
			Debug.Log ("this is the node" + num);
		} else if (size == 2) {
			temp = gameControll.twoNode.Dequeue ();
			Debug.Log ("remove" + temp);
			int firstNode = gameControll.twoNode.Peek ();
			gameControll.twoNode.Enqueue (num);
			Debug.Log ("this is the second node"+num);
			gameControll.validPath (firstNode, num);
		}
	}
}
