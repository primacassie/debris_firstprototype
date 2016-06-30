using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class storeTruck : MonoBehaviour {
	private GameObject sT;

	void Start(){
		sT = GameObject.Find ("storeTruck");
	}

	public void addTruck(int num){
		GameObject addGO = new GameObject ();
		GameObject addGOtext = new GameObject ();
		//add transform to two objects
		addGO.AddComponent<RectTransform> ();
		addGOtext.AddComponent<RectTransform> ();

		//set size two two object
		addGO.transform.SetParent(sT.transform,false);
		addGOtext.transform.SetParent (sT.transform, false);
		float wid = sT.GetComponent<RectTransform> ().rect.width;
		float hei = sT.GetComponent<RectTransform> ().rect.height;
		//Debug.Log ("width is" + wid + "height is " + hei);
		if (gameControll.redTruck) {
			string goName = "redTruckImage" + num.ToString ();
			addGO.name = goName;
			string goText = "redTruckText" + num.ToString ();
			addGOtext.name = goText;
			addGOtext.tag = "truckText";
			addGO.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (-wid/2+45, -hei/2+30+50*num);
			addGO.GetComponent<RectTransform> ().sizeDelta = new Vector2 (60f, 40f);
			Image addIm = addGO.AddComponent<Image> ();
			addIm.GetComponent<Image> ().color = new Color (1, 0, 0);
			//addIm.GetComponent<Image>().sprite=Resources.Load<Sprite> ("Image/truckIcon") as Sprite;

			addGOtext.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (-wid / 2 + 45, -hei / 2 + 70 + 50 * num);
			addGOtext.GetComponent<RectTransform> ().sizeDelta = new Vector2 (60f, 40f);
			Text addText = addGOtext.AddComponent<Text> ();
			addText.GetComponent<Text> ().text = gameControll.carCap.ToString();
			addText.GetComponent<Text> ().fontSize = 30;
			addText.GetComponent<Text> ().font = Resources.GetBuiltinResource (typeof(Font), "Arial.ttf") as Font;
			addText.GetComponent<Text> ().alignment = TextAnchor.MiddleCenter;
			addText.GetComponent<Text> ().color = new Color (1, 0, 0, 1);
			addText.GetComponent<Text> ().fontStyle = FontStyle.Bold;
		}

		if (gameControll.blueTruck) {
			string goName = "blueTruckImage" + num.ToString ();
			addGO.name = goName;
			string goText = "blueTruckText" + num.ToString ();
			addGOtext.name = goText;
			addGOtext.tag = "truckText";
			addGO.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (-wid/2+45+90, -hei/2+30+50*num);
			addGO.GetComponent<RectTransform> ().sizeDelta = new Vector2 (60f, 40f);
			Image addIm = addGO.AddComponent<Image> ();
			addIm.GetComponent<Image> ().color = new Color (0, 0, 1);
			//addIm.GetComponent<Image>().sprite=Resources.Load<Sprite> ("Image/truckIcon") as Sprite;

			addGOtext.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (-wid / 2 + 45+90, -hei / 2 + 70 + 50 * num);
			addGOtext.GetComponent<RectTransform> ().sizeDelta = new Vector2 (60f, 40f);
			Text addText = addGOtext.AddComponent<Text> ();
			addText.GetComponent<Text> ().text = gameControll.carCap.ToString();
			addText.GetComponent<Text> ().fontSize = 30;
			addText.GetComponent<Text> ().font = Resources.GetBuiltinResource (typeof(Font), "Arial.ttf") as Font;
			addText.GetComponent<Text> ().alignment = TextAnchor.MiddleCenter;
			addText.GetComponent<Text> ().color = new Color (0, 0, 1, 1);
			addText.GetComponent<Text> ().fontStyle = FontStyle.Bold;
		}

		if (gameControll.greenTruck) {
			string goName = "greenTruckImage" + num.ToString ();
			addGO.name = goName;
			string goText = "greenTruckText" + num.ToString ();
			addGOtext.tag = "truckText";
			addGOtext.name = goText;
			addGO.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (-wid/2+45+180, -hei/2+30+50*num);
			addGO.GetComponent<RectTransform> ().sizeDelta = new Vector2 (60f, 40f);
			Image addIm = addGO.AddComponent<Image> ();
			addIm.GetComponent<Image> ().color = new Color (0, 1, 0);
			//addIm.GetComponent<Image>().sprite=Resources.Load<Sprite> ("Image/truckIcon") as Sprite;

			addGOtext.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (-wid / 2 + 45+180, -hei / 2 + 70 + 50 * num);
			addGOtext.GetComponent<RectTransform> ().sizeDelta = new Vector2 (60f, 40f);
			Text addText = addGOtext.AddComponent<Text> ();
			addText.GetComponent<Text> ().text = gameControll.carCap.ToString();
			addText.GetComponent<Text> ().fontSize = 30;
			addText.GetComponent<Text> ().font = Resources.GetBuiltinResource (typeof(Font), "Arial.ttf") as Font;
			addText.GetComponent<Text> ().alignment = TextAnchor.MiddleCenter;
			addText.GetComponent<Text> ().color = new Color (0, 1, 0, 1);
			addText.GetComponent<Text> ().fontStyle = FontStyle.Bold;
		}
	}
}
