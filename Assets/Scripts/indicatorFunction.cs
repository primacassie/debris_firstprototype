using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class indicatorFunction : MonoBehaviour {
	private int[] indictorPath;

	void Start(){
		indictorPath = Node.nodeArray;
	}


	void OnMouseDown(){
		if (!gameControll.redTruck && !gameControll.greenTruck && !gameControll.blueTruck) {
			setCancelPathButton (indictorPath);
			for (int i = 0; i < indictorPath.Length-1; i++) {
				int num1 = indictorPath [i];
				int num2 = indictorPath [i + 1];
				string name= "pathAnim"+num1.ToString()+num2.ToString();
//				GameObject obj = GameObject.Find (name);
//				obj.GetComponent<RectTransform> ().sizeDelta = new Vector2 (0.3f, 0.3f);
			}
		}
	}

	public static void setCancelPathButton(int[] nodeArr)
	{
		float x = 0.0f;
		float y = 0.0f;
		HashSet<int> set = new HashSet <int> ();
		foreach (int num in nodeArr) {
			if (set.Add (num)) {
				string numStr = "node" + num.ToString ();
				if (num == 1) {
					numStr = "depot";
				}
				x += GameObject.Find (numStr).transform.position.x;
				y += GameObject.Find (numStr).transform.position.y;
			}
		}
		//int i=0;
		int count=set.Count;
		x = x / count;
		y = y / count;
		float z = 100f;
		Vector2 v4 = getSmallest (Node.v1, Node.v2, Node.v3, x, y);
		x = v4.x;
		y = v4.y;
		GameObject cancelMark = new GameObject ();
		cancelMark.name = "cancelMark";
		Transform parentTransform = GameObject.Find ("gamePanel").GetComponent<Transform> ();
		cancelMark.transform.SetParent (parentTransform);
		cancelMark.transform.position = new Vector3 (x, y, z);
		cancelMark.transform.localScale = new Vector3 (0.3f, 0.3f, 1f);
		BoxCollider2D collider = cancelMark.AddComponent<BoxCollider2D> ();
		collider.enabled = true;
		collider.size = new Vector2 (100, 100);
		Image cm = cancelMark.AddComponent<Image> ();
		cm.sprite = Resources.Load<Sprite> ("Image/cancel") as Sprite;
		//cm.color = new Color (1, 0, 0);
	}

	private static Vector2 getSmallest(Vector2 f1,Vector2 f2,Vector2 f3,float x, float y){
		if (Mathf.Abs(f1.x-x)+ Mathf.Abs(f1.y-y)<= Mathf.Abs(f2.x-x)+ Mathf.Abs(f2.y-y) && Mathf.Abs(f1.x-x)+ Mathf.Abs(f1.y-y) <=Mathf.Abs(f3.x-x)+ Mathf.Abs(f3.y-y)) {
			return f1;
		} else if (Mathf.Abs(f2.x-x)+ Mathf.Abs(f2.y-y) <= Mathf.Abs(f3.x-x)+ Mathf.Abs(f3.y-y)) {
			return f2;
		} else {
			return f3;
		}
	}
}
