using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class submitAnim : MonoBehaviour {

	private Transform[] wayPointList;
	//private Vector2[] wayPointVector;

    private int currentWayPoint = 0;
	Transform targetWayPoint;
	//Vector2 targetVector;

    public float speed = 3f;
    // Use this for initialization
    void Start () {
        string objName = this.gameObject.name;
        //Debug.Log(this.gameObject.name);
        if (submitButton.animDic.ContainsKey(objName))
        {
            List<int> l=submitButton.animDic[objName];
            int[] arr = l.ToArray();
			wayPointList = new Transform[arr.Length];
			//wayPointVector = new Vector2[arr.Length];
            for(int i = 0; i < arr.Length; i++)
            {
                string nodeName = "node" + arr[i].ToString();
                Debug.Log(nodeName);
                if (arr[i] == 1)
                {
                    nodeName = "depot";
                }
				wayPointList [i] = GameObject.Find (nodeName).transform;
				//wayPointVector [i] = new Vector3(GameObject.Find (nodeName).transform.position.x,GameObject.Find (nodeName).transform.position.y,0);
				//wayPointVector[i]=GameObject.Find(nodeName).transform.position;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (currentWayPoint < this.wayPointList.Length)
        {
            if (targetWayPoint == null)
                targetWayPoint = wayPointList[currentWayPoint];
            walk();
        }
    }

    void walk()
    {
//		if (currentWayPoint < wayPointList.Length-1) {
//			float slope = (targetWayPoint.position.y - transform.position.y) / (targetWayPoint.position.x - transform.position.x);
//			Vector3 dir= new Vector3 (0, 0, Mathf.Atan (slope) * Mathf.Rad2Deg);
//			if (transform.position.x < targetWayPoint.position.x)
//			{
//				dir = new Vector3 (0, 0, (Mathf.Atan (slope) * Mathf.Rad2Deg - 180f+90)%180);
//			}else
//			{
//				dir = new Vector3 (0, 0, (Mathf.Atan (slope) * Mathf.Rad2Deg+90)%180);
//			}
//			//Debug.Log (dir.z);
////			if (Mathf.Abs(Mathf.Abs(transform.eulerAngles.z)-Mathf.Abs(dir.z)) >1) {
////				//Debug.Log ("dir.z "+dir.z);
////				//Debug.Log ("transform.z "+transform.eulerAngles.z);
////				transform.Rotate(new Vector3(0,0,1));
////			}
////			if (Mathf.Abs(Mathf.Abs(transform.eulerAngles.z)-Mathf.Abs(dir.z)) <= 1) {
////				//Debug.Log ("reach inside");
////				Debug.Log(this.gameObject.name+" " + dir.z);
////				transform.position = Vector3.MoveTowards (transform.position, targetWayPoint.position, speed * Time.deltaTime);
////			} 
//			if (Mathf.Abs((transform.eulerAngles.z-180)-dir.z) >1) {
//				//Debug.Log ("dir.z "+dir.z);
//				//Debug.Log ("transform.z "+transform.eulerAngles.z);
//				//Debug.Log(this.gameObject.name+" " + transform.eulerAngles.z);
//				transform.Rotate(new Vector3(0,0,1));
//			}
//			if (Mathf.Abs((transform.eulerAngles.z-180)-dir.z) <= 1) {
//				//Debug.Log ("reach inside");
//				transform.position = Vector3.MoveTowards (transform.position, targetWayPoint.position, speed * Time.deltaTime);
//			} 
//		}else {
//			transform.position = Vector3.MoveTowards (transform.position, targetWayPoint.position, speed * Time.deltaTime);
//		}
		float slope = (targetWayPoint.position.y - transform.position.y) / (targetWayPoint.position.x - transform.position.x);
		Vector3 dir= new Vector3 (0, 0, Mathf.Atan (slope) * Mathf.Rad2Deg);
		if (transform.position.x < targetWayPoint.position.x)
		{
			dir = new Vector3 (0, 0, (Mathf.Atan (slope) * Mathf.Rad2Deg+90)%180);
		}else
		{
			dir = new Vector3 (0, 0, (Mathf.Atan (slope) * Mathf.Rad2Deg+90+180)%180);
		}
		//Debug.Log (dir.z);
		//			if (Mathf.Abs(Mathf.Abs(transform.eulerAngles.z)-Mathf.Abs(dir.z)) >1) {
		//				//Debug.Log ("dir.z "+dir.z);
		//				//Debug.Log ("transform.z "+transform.eulerAngles.z);
		//				transform.Rotate(new Vector3(0,0,1));
		//			}
		//			if (Mathf.Abs(Mathf.Abs(transform.eulerAngles.z)-Mathf.Abs(dir.z)) <= 1) {
		//				//Debug.Log ("reach inside");
		//				Debug.Log(this.gameObject.name+" " + dir.z);
		//				transform.position = Vector3.MoveTowards (transform.position, targetWayPoint.position, speed * Time.deltaTime);
		//			} 
		if (Mathf.Abs((transform.eulerAngles.z-180)-dir.z) >1) {
			//Debug.Log ("dir.z "+dir.z);
			//Debug.Log ("transform.z "+transform.eulerAngles.z);
			//Debug.Log(this.gameObject.name+" " + transform.eulerAngles.z);
			transform.Rotate(new Vector3(0,0,1));
		}
		if (Mathf.Abs((transform.eulerAngles.z-180)-dir.z) <= 1) {
			//Debug.Log ("reach inside");
			transform.position = Vector3.MoveTowards (transform.position, targetWayPoint.position, speed * Time.deltaTime);
		} 

		//transform.forward = Vector3.RotateTowards (transform.forward,rotateV, speed * Time.deltaTime, 0.0f);
        // move towards the target


//		transform.position = Vector2.MoveTowards(transform.position, targetVector, speed * Time.deltaTime);
		if (transform.position == targetWayPoint.position)
        {
			//Debug.Log ("did it");
			if (currentWayPoint < wayPointList.Length-1) {
				currentWayPoint++;
				targetWayPoint = wayPointList[currentWayPoint];
				//transform.Rotate (new Vector3 (0, 0, 1));
			}
        }

    }
}
