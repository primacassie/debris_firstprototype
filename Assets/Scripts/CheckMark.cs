using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CheckMark : MonoBehaviour {

	public static bool nextStep;
	private int node1;
	private int node2;
	private float dist;
	private float lineDrawSpeed;
	private float counter;
	private Vector3 origin;
	private Vector3 destination;
	private LineRenderer lr;
	private bool startUpdate;


	//use a queue to store two nodes
	private Queue<int> twoNode=new Queue<int>();


	void Start()
	{
		lineDrawSpeed = 10;
	}

	private void rectAnimation(int num1, int num2)
	{
		node1 = num1;
		node2 = num2;
		string strNode1 = "node" + node1;
		if (node1 == 1)
		{
			strNode1 = "depot";
		}
		string strNode2 = "node" + node2;
		if (node2 == 1)
		{
			strNode2 = "depot";
		}
		GameObject node1G = GameObject.Find(strNode1);
		GameObject node2G = GameObject.Find(strNode2);
		origin = node1G.transform.position;
		destination = node2G.transform.position;
		float slope = (origin.y - destination.y) / (origin.x - destination.x);
		float d = 0.15f;
		if (origin.y > destination.y) {
			origin = new Vector3 (origin.x + d*Mathf.Sin(Mathf.Atan(slope)), origin.y- d*Mathf.Cos(Mathf.Atan(slope)), origin.z);
			destination = new Vector3 (destination.x + d*Mathf.Sin(Mathf.Atan(slope)), destination.y-d*Mathf.Cos(Mathf.Atan(slope)), destination.z);
		}
		if (origin.y < destination.y) {
			origin = new Vector3 (origin.x - d*Mathf.Sin(Mathf.Atan(slope)), origin.y+d*Mathf.Cos(Mathf.Atan(slope)), origin.z);
			destination = new Vector3 (destination.x - d*Mathf.Sin(Mathf.Atan(slope)), destination.y+d*Mathf.Cos(Mathf.Atan(slope)), destination.z);
		}
		if (origin.y == destination.y) {
			if (origin.x < destination.x) {
				origin = new Vector3 (origin.x , origin.y-d, origin.z);
				destination = new Vector3 (destination.x , destination.y-d, destination.z);
			} else if (origin.x > destination.x) {
				origin = new Vector3 (origin.x , origin.y+d, origin.z);
				destination = new Vector3 (destination.x , destination.y+d, destination.z);
			}
		}
		lr = GetComponent<LineRenderer>();
		lr.SetPosition(0, origin);
		lr.SetWidth(.1f, .1f);
		if (gameControll.redTruck)
		{
			lr.material = Resources.Load<Material>("Materials/redAnim") as Material;
		}else if (gameControll.greenTruck)
		{
			lr.material = Resources.Load<Material>("Materials/greenAnim") as Material;
		}
		else if (gameControll.blueTruck)
		{
			lr.material = Resources.Load<Material>("Materials/blueAnim") as Material;
		}
		dist = Vector3.Distance(origin, destination);
		startUpdate = true;
	}

	void OnMouseDown(){
		nextStep = true;
		pathCap.desableSlider();
		StartCoroutine (waitTime ());
	}

	IEnumerator waitTime()
	{
		for (int i=0; i < Node.nodeArray.Length; i++) {
			//yield return new WaitForSeconds (2);
			if (twoNode.Count == 0) {
				twoNode.Enqueue (Node.nodeArray [i]);
			} else if (twoNode.Count == 1) {
				twoNode.Enqueue (Node.nodeArray [i]);
				int num1 = twoNode.Peek ();
				int num2 = Node.nodeArray [i];
				string pathname = pathName (num1, num2, gameControll.redTruck, gameControll.greenTruck, gameControll.blueTruck);
				GameObject path = new GameObject ();
				path.name = pathname;
				path.AddComponent<LineRenderer> ();
				path.AddComponent<CheckMark> ().rectAnimation (num1, num2);
			} else if (twoNode.Count == 2) {
				twoNode.Dequeue ();
				twoNode.Enqueue (Node.nodeArray [i]);
				int num1 = twoNode.Peek ();
				int num2 = Node.nodeArray [i];
				Debug.Log ("num of num1: " + num1);
				Debug.Log ("num of num2: " + num2);
				string pathname = pathName (num1, num2, gameControll.redTruck, gameControll.greenTruck, gameControll.blueTruck);
				//Debug.Log (pathname+ " in second node");
				GameObject path = new GameObject ();
				path.name = pathname;
				path.AddComponent<LineRenderer> ();
				yield return new WaitForSeconds (2);
				path.AddComponent<CheckMark> ().rectAnimation (num1, num2);
				if (num2 == 1) {
					StartCoroutine (waitToDestroy ());
				}
			}
		}
	}

	IEnumerator waitToDestroy(){
		yield return new WaitForSeconds (2);
		Destroy (this.gameObject);
	}

	void Update()
	{
		if ((counter < dist) && startUpdate)
		{
			counter += .1f / lineDrawSpeed;
			float x = Mathf.Lerp(0, dist, counter);
			Vector3 pointAlongline = x * Vector3.Normalize(destination - origin) + origin;
			lr.SetPosition(1, pointAlongline);
		}
	}

	private string pathName(int num1,int num2,bool red,bool green,bool blue){
		string name="";
		if (red) {
			name = "redPath" + num1.ToString () + num2.ToString ();
		}else if (green) {
			name = "greenPath" + num1.ToString () + num2.ToString ();
		}else if (blue) {
			name = "bluePath" + num1.ToString () + num2.ToString ();
		}
		return name;
	}
}
