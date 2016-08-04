using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class submitButton : MonoBehaviour {


    public static Dictionary<string,List<int>> animDic = new Dictionary<string,List<int>>();


 //   public float speed = 4f;

 //   private int current = 0;
 //   private int[] arr;
 //   Transform target;
 //   Transform[] tArr;
 //   private bool canUpdate;

 //   // Use this for initialization
 //   void Start () {
	//}
	
	//// Update is called once per frame
	//void Update () {
 //       //if (currentWayPoint < this.wayPointList.Length)
 //       //{
 //       //    if (targetWayPoint == null)
 //       //        targetWayPoint = wayPointList[currentWayPoint];
 //       //    finalAnimation();
 //       //}
 //       if (canUpdate)
 //       {
 //           if (current < arr.Length)
 //           {
 //               if (target == null)
 //                   target = tArr[current];
 //               finalAnimation();
 //           }
 //       }
 //   }

    void OnMouseDown()
    {
        //int redLength = Node.redAl.Count;
        //int blueLength = Node.blueAl.Count;
        //int greenLength = Node.greenAl.Count;
        //int[] redTimeArr = new int[redLength];
        //int[] blueTimeArr = new int[blueLength];
        //int[] greenTimeArr = new int[blueLength];
       
        submit();
    }


    public void submit()
    {
        int node1 = 0;
        int node2 = 0;
        int cap = 0;
        int[,] arr = gameControll.capArray;
        int sum = 0;
        for (int i = 0; i < arr.GetLength(0); i++)
        {
            for (int j = 0; j < arr.GetLength(1); j++)
            {
                sum += arr[i, j];
                if (arr[i, j] != 0)
                {
                    node1 = i;
                    node2 = j;
                    cap = arr[i, j];
                }
            }
        }
        if (sum == 0)
        {
            //Debug.Log("congratulations! you finish this game!");
            //GameObject.Find("ModalControl").GetComponent<testWindow>().takeAction("Congratulations! You finish this round!");
            finalAnimation();
        }
        else
        {
            //string temp = "there are still " + cap + " debris in the path " + node1 + node2 + ", please clean it!";
            Debug.Log("there are still " + cap + " debris in the path " + node1 + node2 + ", please clean it!");
            //GameObject.Find("ModalControl").GetComponent<testWindow>().takeAction(temp);
        }
    }

    public void finalAnimation()
    {
        int r = 0;
        foreach(List<int> l in Node.redAl)
        {
            GameObject go = new GameObject();
            go.transform.SetParent(GameObject.Find("Canvas").transform);
            go.AddComponent<Image>();
            go.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/truck_R256") as Sprite;
            go.AddComponent<submitAnim>();
            //go.AddComponent<Transform>();
            string truckName = "red" + r.ToString();
            go.name = truckName;
            animDic.Add(truckName, l);
            r++;
        }
        Debug.Log("r number "+r);
        int b = 0;
        foreach (List<int> l in Node.blueAl)
        {
            GameObject go = new GameObject();
            //go.AddComponent<Transform>();
            go.transform.SetParent(GameObject.Find("Canvas").transform);
            go.AddComponent<Image>();
            go.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/truck_R256") as Sprite;
            go.AddComponent<submitAnim>();
            string truckName = "blue" + b.ToString();
            go.name = truckName;
            animDic.Add(truckName, l);
            b++;
        }
        int g = 0;
        foreach (List<int> l in Node.greenAl)
        {
            GameObject go = new GameObject();
            go.transform.SetParent(GameObject.Find("Canvas").transform);
            go.AddComponent<Image>();
            go.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/truck_R256") as Sprite;
            go.AddComponent<submitAnim>();
            //go.AddComponent<Transform>();
            string truckName = "green" + g.ToString();
            go.name = truckName;
            animDic.Add(truckName, l);
            g++;
        }
    }
}
