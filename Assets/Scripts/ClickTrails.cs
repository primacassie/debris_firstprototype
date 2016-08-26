using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine.UI;

public class ClickTrails : MonoBehaviour {
	GameObject go;
    public static float minP;
    public static float maxT;
    public static int inters;

    void OnMouseDown()
    {
        string name = this.gameObject.name;
        string resultString = Regex.Match(name, @"\d+").Value;
        int num = int.Parse(resultString);
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Trails");
        foreach(GameObject obj in objs)
        {
            Behaviour halo = (Behaviour)obj.GetComponent("Halo");
            halo.enabled = false;
        }
        Behaviour halo1 = (Behaviour)this.gameObject.GetComponent("Halo");
        halo1.enabled = true;
        inters=submitButton.forInter[num - 1];
        minP = submitButton.forProf[num - 1];
        maxT = submitButton.forTime[num - 1];
    }

    void OnMouseEnter()
    {
        string name = this.gameObject.name;
        string resultString = Regex.Match(name, @"\d+").Value;
        int num = int.Parse(resultString);
		string ans = "InformationPanel" + resultString;
		if (num != 1) {
			go = Instantiate (Resources.Load ("Prefab/InformationPanel")) as GameObject;
			go.name = ans;
			GameObject firstObj = GameObject.Find ("InformationPanel1");
			Vector2 pos1 = firstObj.GetComponent<RectTransform> ().anchoredPosition;
			go.transform.SetParent (this.gameObject.transform.parent.transform, false);
			//float width = firstObj.GetComponent<RectTransform> ().rect.width;
			float height = firstObj.GetComponent<RectTransform> ().rect.height;
			Vector2 pos2 = new Vector2 (pos1.x, pos1.y + (height + 10) * (num - 1));
			go.GetComponent<RectTransform> ().anchoredPosition = pos2;
			go.GetComponent<RectTransform> ().localScale = new Vector2 (1.1f, 1f);
		} else if (num == 1) {
			go=GameObject.Find ("InformationPanel1");
		}
		go.transform.Find ("profit").GetComponent<Image> ().enabled = true;
		go.transform.Find ("time").GetComponent<Image> ().enabled = true;
		go.transform.Find ("intersection").GetComponent<Image> ().enabled = true;
		go.transform.Find ("informationProfitText").GetComponent<Text> ().enabled = true;
		go.transform.Find ("informationProfitText").GetComponent<Text> ().text = submitButton.forProf [num - 1].ToString();
		go.transform.Find ("informationTimeText").GetComponent<Text> ().enabled = true;
		go.transform.Find ("informationTimeText").GetComponent<Text> ().text = submitButton.forTime [num - 1].ToString();
		go.transform.Find ("informationIntersectionText").GetComponent<Text> ().enabled = true;
		go.transform.Find ("informationIntersectionText").GetComponent<Text> ().text = submitButton.forInter [num - 1].ToString();
    }

    void OnMouseExit()
    {
		if (Regex.Match (go.name, @"\d+").Value == "1") {
			go.transform.Find ("profit").GetComponent<Image> ().enabled = false;
			go.transform.Find ("time").GetComponent<Image> ().enabled = false;
			go.transform.Find ("intersection").GetComponent<Image> ().enabled = false;
			go.transform.Find ("informationProfitText").GetComponent<Text> ().enabled = false;
			go.transform.Find ("informationTimeText").GetComponent<Text> ().enabled = false;
			go.transform.Find ("informationIntersectionText").GetComponent<Text> ().enabled = false;
		} else {
			Destroy (go);
		}
    }
}
