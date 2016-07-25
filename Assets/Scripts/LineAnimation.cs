using UnityEngine;
using System.Collections;

public class LineAnimation : MonoBehaviour
{

    private int node1;
    private int node2;
    private float dist;
    private float lineDrawSpeed;
    private float counter;
    private Vector3 origin;
    private Vector3 destination;
    private LineRenderer lr;
    private bool startUpdate;

    //t is a time clock to control the lerp time
    private float t = 0;

    //duration is the continuing time 
    private float duration = 5;

    void Start()
    {
        lineDrawSpeed = 10;
    }

    public void rectAnimation(int num1, int num2)
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

    // Update is called once per frame
    void Update()
    {
        if ((counter < dist) && startUpdate)
        {
            counter += .1f / lineDrawSpeed;
            float x = Mathf.Lerp(0, dist, counter);
            Vector3 pointAlongline = x * Vector3.Normalize(destination - origin) + origin;
            lr.SetPosition(1, pointAlongline);
        }
        //if(counter >= dist)
        //{
        //    lr.material=Resources.Load<Material>("Materials/blueAnim") as Material;
        //    //lr.material.color = Color.Lerp(Color.red, Color.blue, t);
        //    //if (t < 1)
        //    //{ // while t below the end limit...
        //    //  // increment it at the desired rate every update:
        //    //    t += Time.deltaTime / duration;
        //    //}
        //}
		//if (counter >= dist) {
		//	lr.material=Resources.Load<Material>("Materials/blueAnim") as Material;
		//}
    }



}
