using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class storeTruck : MonoBehaviour {
	private GameObject sT;
	public static bool refreshed;

	void Awake(){
		sT = GameObject.Find ("storeTruck");
		initializeValue (RefreshButton.refresh);
	}

	public void addTruck(int num){
		GameObject addGO = new GameObject ();
		addGO.AddComponent<StoreTruckClick> ();
		addGO.AddComponent<BoxCollider2D> ();
		addGO.GetComponent<BoxCollider2D> ().size = new Vector2 (60, 40);
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
			//addIm.GetComponent<Image> ().color = new Color (1, 0, 0);
			//addIm.GetComponent<Image>().sprite=Resources.Load<Sprite> ("Image/truckIcon") as Sprite;

			addGOtext.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (-wid / 2 + 45, -hei / 2 + 62 + 50 * num);
			addGOtext.GetComponent<RectTransform> ().sizeDelta = new Vector2 (60f, 40f);
			Text addText = addGOtext.AddComponent<Text> ();
			string truckText = "0/100";
			addText.GetComponent<Text> ().text = truckText;
			addText.GetComponent<Text> ().fontSize = 20;
			//addText.GetComponent<Text> ().font = Resources.GetBuiltinResource (typeof(Font), "Arial.ttf") as Font;
			addText.GetComponent<Text> ().font =Resources.Load<Font>("Font/AGENCYR") as Font;
			addText.GetComponent<Text> ().fontStyle = FontStyle.Normal;
			addText.GetComponent<Text> ().alignment = TextAnchor.MiddleCenter;
			//addText.GetComponent<Text> ().color = new Color (1, 0, 0, 1);
			addText.GetComponent<Text> ().fontStyle = FontStyle.Bold;
			string htmlValue = "#db4f69";
			Color newCol;
			if (ColorUtility.TryParseHtmlString (htmlValue, out newCol)) {
				addIm.GetComponent<Image> ().color = newCol;
				addText.GetComponent<Text> ().color = newCol;
			}
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
			string htmlValue = "#3b73e1";
			Color newCol;
//			addIm.GetComponent<Image> ().color = new Color (0, 0, 1);
			//addIm.GetComponent<Image>().sprite=Resources.Load<Sprite> ("Image/truckIcon") as Sprite;
	
			addGOtext.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (-wid / 2 + 45+90, -hei / 2 + 62 + 50 * num);
			addGOtext.GetComponent<RectTransform> ().sizeDelta = new Vector2 (60f, 40f);
			Text addText = addGOtext.AddComponent<Text> ();
			string truckText = "0/100";
			addText.GetComponent<Text> ().text = truckText;
			addText.GetComponent<Text> ().fontSize = 20;
			//addText.GetComponent<Text> ().font = Resources.GetBuiltinResource (typeof(Font), "Arial.ttf") as Font;
			addText.GetComponent<Text> ().font =Resources.Load<Font>("Font/AGENCYR") as Font;
			addText.GetComponent<Text> ().fontStyle = FontStyle.Normal;
			//addText.GetComponent<Text> ().font = Resources.GetBuiltinResource (typeof(Font), "Arial.ttf") as Font;
			addText.GetComponent<Text> ().alignment = TextAnchor.MiddleCenter;
			addText.GetComponent<Text> ().fontStyle = FontStyle.Bold;
			if (ColorUtility.TryParseHtmlString (htmlValue, out newCol)) {
				addIm.GetComponent<Image> ().color = newCol;
				addText.GetComponent<Text> ().color = newCol;
			}
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
			//addIm.GetComponent<Image> ().color = new Color (0, 1, 0);
			//addIm.GetComponent<Image>().sprite=Resources.Load<Sprite> ("Image/truckIcon") as Sprite;

			addGOtext.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (-wid / 2 + 45+180, -hei / 2 + 62 + 50 * num);
			addGOtext.GetComponent<RectTransform> ().sizeDelta = new Vector2 (60f, 40f);
			Text addText = addGOtext.AddComponent<Text> ();
			string truckText = "0/100";
			addText.GetComponent<Text> ().text = truckText;
			addText.GetComponent<Text> ().fontSize = 20;
			//addText.GetComponent<Text> ().font = Resources.GetBuiltinResource (typeof(Font), "Arial.ttf") as Font;
			addText.GetComponent<Text> ().font =Resources.Load<Font>("Font/AGENCYR") as Font;
			addText.GetComponent<Text> ().fontStyle = FontStyle.Normal;
			//addText.GetComponent<Text> ().font = Resources.GetBuiltinResource (typeof(Font), "Arial.ttf") as Font;
			addText.GetComponent<Text> ().alignment = TextAnchor.MiddleCenter;
			//addText.GetComponent<Text> ().color = new Color (0, 1, 0, 1);
			addText.GetComponent<Text> ().fontStyle = FontStyle.Bold;
			string htmlValue = "#33e786";
			Color newCol;
			if (ColorUtility.TryParseHtmlString (htmlValue, out newCol)) {
				addIm.GetComponent<Image> ().color = newCol;
				addText.GetComponent<Text> ().color = newCol;
			}
		}
	}

	private void addRedTruck(int num){
		GameObject addGO = new GameObject ();
		addGO.AddComponent<StoreTruckClick> ();
		addGO.AddComponent<BoxCollider2D> ();
		addGO.GetComponent<BoxCollider2D> ().size = new Vector2 (60, 40);
		GameObject addGOtext = new GameObject ();
		//add transform to two objects
		addGO.AddComponent<RectTransform> ();
		addGOtext.AddComponent<RectTransform> ();

		//set size two two object
		addGO.transform.SetParent(sT.transform,false);
		addGOtext.transform.SetParent (sT.transform, false);
		float wid = sT.GetComponent<RectTransform> ().rect.width;
		float hei = sT.GetComponent<RectTransform> ().rect.height;
		string goName = "redTruckImage" + num.ToString ();
		addGO.name = goName;
		addGO.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (-wid/2+45, -hei/2+30+50*num);
		addGO.GetComponent<RectTransform> ().sizeDelta = new Vector2 (60f, 40f);
		Image addIm = addGO.AddComponent<Image> ();
		//addIm.GetComponent<Image> ().color = new Color (1, 0, 0);
		//addIm.GetComponent<Image>().sprite=Resources.Load<Sprite> ("Image/truckIcon") as Sprite;

		addGOtext.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (-wid / 2 + 45, -hei / 2 + 62 + 50 * num);
		addGOtext.GetComponent<RectTransform> ().sizeDelta = new Vector2 (60f, 40f);
		string htmlValue = "#db4f69";
		Color newCol;
		if (ColorUtility.TryParseHtmlString (htmlValue, out newCol)) {
			addIm.GetComponent<Image> ().color = newCol;
		}
	}

	private void addBlueTruck(int num){
		GameObject addGO = new GameObject ();
		addGO.AddComponent<StoreTruckClick> ();
		addGO.AddComponent<BoxCollider2D> ();
		addGO.GetComponent<BoxCollider2D> ().size = new Vector2 (60, 40);
		GameObject addGOtext = new GameObject ();
		//add transform to two objects
		addGO.AddComponent<RectTransform> ();
		addGOtext.AddComponent<RectTransform> ();

		//set size two two object
		addGO.transform.SetParent(sT.transform,false);
		addGOtext.transform.SetParent (sT.transform, false);
		float wid = sT.GetComponent<RectTransform> ().rect.width;
		float hei = sT.GetComponent<RectTransform> ().rect.height;
		string goName = "blueTruckImage" + num.ToString ();
		addGO.name = goName;
		addGO.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (-wid/2+45+90, -hei/2+30+50*num);
		addGO.GetComponent<RectTransform> ().sizeDelta = new Vector2 (60f, 40f);
		Image addIm = addGO.AddComponent<Image> ();
		string htmlValue = "#3b73e1";
		Color newCol;
		//			addIm.GetComponent<Image> ().color = new Color (0, 0, 1);
		//addIm.GetComponent<Image>().sprite=Resources.Load<Sprite> ("Image/truckIcon") as Sprite;

		addGOtext.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (-wid / 2 + 45+90, -hei / 2 + 62 + 50 * num);
		addGOtext.GetComponent<RectTransform> ().sizeDelta = new Vector2 (60f, 40f);
		if (ColorUtility.TryParseHtmlString (htmlValue, out newCol)) {
			addIm.GetComponent<Image> ().color = newCol;
		}
	}

	private void initializeValue(bool bValue){
		if (!bValue) {
			addRedTruck (gameControll.redTruckNum);
			gameControll.redTruckNum++;
			addRedTruck (gameControll.redTruckNum);
			gameControll.redTruckNum++;
			addBlueTruck (gameControll.blueTruckNum);
			gameControll.blueTruckNum++;
			addBlueTruck (gameControll.blueTruckNum);
			gameControll.blueTruckNum++;
			refreshed = true;
		}
	}
}
