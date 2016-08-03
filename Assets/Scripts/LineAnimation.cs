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
		float slope = (origin.y - destination.y) / (origin.x - destination.x);
		float d = 0.165f;
		if (origin.x > destination.x) {
			origin = new Vector3 (origin.x - d*Mathf.Sin(Mathf.Atan(slope)), origin.y+ d*Mathf.Cos(Mathf.Atan(slope)), origin.z);
			destination = new Vector3 (destination.x -d*Mathf.Sin(Mathf.Atan(slope)), destination.y+d*Mathf.Cos(Mathf.Atan(slope)), destination.z);
		}
		if (origin.x < destination.x) {
			origin = new Vector3 (origin.x + d*Mathf.Sin(Mathf.Atan(slope)), origin.y-d*Mathf.Cos(Mathf.Atan(slope)), origin.z);
			destination = new Vector3 (destination.x + d*Mathf.Sin(Mathf.Atan(slope)), destination.y-d*Mathf.Cos(Mathf.Atan(slope)), destination.z);
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
        lr.SetWidth(.15f, .15f);
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
		dist = Vector2.Distance(origin, destination); 
        startUpdate = true;
		StartCoroutine (toGradientColor (num1, num2));
    }

	IEnumerator toGradientColor(int num1,int num2){
		yield return new WaitForSeconds (2);
		string desObj = "newPathAnim" + num1.ToString () + num2.ToString ();
		if (GameObject.Find (desObj) != null) {
			Destroy (GameObject.Find (desObj));
		}
		string existObj = "pathAnim" + num1.ToString () + num2.ToString ();
		if (GameObject.Find (existObj) != null) {
			lr = GameObject.Find (existObj).GetComponent<LineRenderer> ();
			lr.enabled = true;
			if (Node.redPathArray[num1, num2] && !Node.greenPathArray[num1, num2] && !Node.bluePathArray[num1, num2])
			{
				lr.material = Resources.Load<Material>("Materials/redAnim") as Material;
			}

			if (!Node.redPathArray[num1, num2] && Node.greenPathArray[num1, num2] && !Node.bluePathArray[num1, num2])
			{
				lr.material = Resources.Load<Material>("Materials/greenAnim") as Material;
			}

			if (!Node.redPathArray[num1, num2] && !Node.greenPathArray[num1, num2] && Node.bluePathArray[num1, num2])
			{
				lr.material = Resources.Load<Material>("Materials/blueAnim") as Material;
			}

			if (Node.redPathArray[num1, num2] && !Node.greenPathArray[num1, num2] && Node.bluePathArray[num1, num2])
			{
				lr.material = Resources.Load<Material>("Materials/GradientRB") as Material;
			}

			if (Node.redPathArray[num1, num2] && Node.greenPathArray[num1, num2] && !Node.bluePathArray[num1, num2])
			{
				lr.material = Resources.Load<Material>("Materials/GradientRG") as Material;
			}

			if (!Node.redPathArray[num1, num2] && Node.greenPathArray[num1, num2] && Node.bluePathArray[num1, num2])
			{
				lr.material = Resources.Load<Material>("Materials/GradientBG") as Material;
			}

			if (Node.redPathArray[num1, num2] && Node.greenPathArray[num1, num2] && Node.bluePathArray[num1, num2])
			{
				lr.material = Resources.Load<Material>("Materials/GradientRGB") as Material;
			}
		}
	}

    // Update is called once per frame
    void Update()
    {
        if ((counter < dist) && startUpdate)
        {
//			if (counter > (dist / 4 * 3)) {
////				Debug.Log ("couter> " + counter);
////				Debug.Log ("couter> " + dist);
//				counter += .05f / lineDrawSpeed;
//			}else if (counter <= (dist / 4 * 3)) {
//				Debug.Log ("couter< " + counter);
//				Debug.Log ("couter< " + dist);
//				counter += .2f / lineDrawSpeed;
//			}
			counter += .2f / lineDrawSpeed;
			//counter += .01f / lineDrawSpeed;
            float x = Mathf.Lerp(0, dist, counter);
            Vector3 pointAlongline = x * Vector3.Normalize(destination - origin) + origin;
            lr.SetPosition(1, pointAlongline);
        }
    }
}
