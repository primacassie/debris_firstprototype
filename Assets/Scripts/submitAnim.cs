using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class submitAnim : MonoBehaviour {

    private Transform[] wayPointList;

    private int currentWayPoint = 0;
    Transform targetWayPoint;

    public float speed = 4f;
    // Use this for initialization
    void Start () {
        string objName = this.gameObject.name;
        Debug.Log(this.gameObject.name);
        if (submitButton.animDic.ContainsKey(objName))
        {
            Debug.Log("work?");
            List<int> l=submitButton.animDic[objName];
            int[] arr = l.ToArray();
            for(int i = 0; i < arr.Length; i++)
            {
                string nodeName = "node" + arr[i].ToString();
                Debug.Log(nodeName);
                if (arr[i] == 1)
                {
                    nodeName = "depot";
                }
                wayPointList[i] = GameObject.Find(nodeName).GetComponent<RectTransform>();
            }
        }
	}
	
	// Update is called once per frame
	//void Update () {
 //       if (currentWayPoint < this.wayPointList.Length)
 //       {
 //           if (targetWayPoint == null)
 //               targetWayPoint = wayPointList[currentWayPoint];
 //           walk();
 //       }
 //   }

    void walk()
    {
        // rotate towards the target
        transform.forward = Vector3.RotateTowards(transform.forward, targetWayPoint.position - transform.position, speed * Time.deltaTime, 0.0f);

        // move towards the target
        transform.position = Vector3.MoveTowards(transform.position, targetWayPoint.position, speed * Time.deltaTime);

        if (transform.position == targetWayPoint.position)
        {
            currentWayPoint++;
            targetWayPoint = wayPointList[currentWayPoint];
        }
    }
}
