using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class submitAnim : MonoBehaviour {

	private Transform[] wayPointList;
	//private Vector2[] wayPointVector;

    private int currentWayPoint = 0;
    Transform targetWayPoint;
	//Vector2 targetVector;

    public float speed = 4f;
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
				wayPointList[i] = GameObject.Find(nodeName).transform;
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
//			if (targetVector == null)
//				targetVector = wayPointVector [currentWayPoint];
            walk();
        }
    }

    void walk()
    {
        // rotate towards the target
		//transform.forward = Vector3.RotateTowards(transform.forward, targetWayPoint.position- transform.position, speed * Time.deltaTime, 0.0f);
//		float slope = (transform.position.y - targetWayPoint.position.y) / (transform.position.x - targetWayPoint.position.x);
//		Vector3 rotateV = new Vector3 ();
//		if (transform.position.x < targetWayPoint.position.x)
//		{
////			int i = 0;
////			if (i == 0) {
////				transform.Rotate (new Vector3 (0, 0, Mathf.Atan (slope) * Mathf.Rad2Deg - 180f));
////				i++;
////			}
//			rotateV=new Vector3(Mathf.Atan(slope) * Mathf.Rad2Deg-180f,0,0);
//			//rotateV=new Vector3(0,Mathf.Atan(slope) * Mathf.Rad2Deg-180f,0 );
//		}else
//		{
////			int i = 0;
////			if (i == 0) {
////				transform.Rotate (new Vector3 (0, 0, Mathf.Atan (slope) * Mathf.Rad2Deg));
////				i++;
////			}
//			rotateV=new Vector3(Mathf.Atan(slope) * Mathf.Rad2Deg,0,0);
//		}

		//transform.forward = Vector3.RotateTowards (transform.forward,rotateV, speed * Time.deltaTime, 0.0f);
        // move towards the target
		transform.position = Vector3.MoveTowards(transform.position, targetWayPoint.position, speed * Time.deltaTime);


//		transform.position = Vector2.MoveTowards(transform.position, targetVector, speed * Time.deltaTime);
        if (transform.position == targetWayPoint.position)
        {
			if (currentWayPoint < wayPointList.Length-1) {
				currentWayPoint++;
				targetWayPoint = wayPointList[currentWayPoint];
//				targetVector = wayPointVector [currentWayPoint];
			}
        }

    }
}
