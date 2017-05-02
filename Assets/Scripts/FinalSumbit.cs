using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinalSumbit : MonoBehaviour {
    private Transform tar;
    GameObject prof;
    GameObject time;
    GameObject intersection;
    GameObject profText;
    GameObject truckTime;
    GameObject interText;
    float minP;
    float maxT;
    int inters;
    // Use this for initialization
    void Start()
    {
        tar = GameObject.Find("depot").transform;
        //tar=new Vector3(-6,0,0);
        prof = new GameObject();
        time = new GameObject();
        intersection = new GameObject();
        profText = new GameObject();
        truckTime = new GameObject();
        interText = new GameObject();
    }

    void OnMouseDown()
    {

        minP = ClickTrails.minP;
        maxT = ClickTrails.maxT;
        inters = ClickTrails.inters;
		int[,] arr = gameControll.capArray;
		int sum = 0;
		for (int i = 0; i < arr.GetLength(0); i++)
		{
			for (int j = 0; j < arr.GetLength(1); j++)
			{
				sum += arr[i, j];
				//                if (arr[i, j] != 0)
				//                {
				//                    node1 = i;
				//                    node2 = j;
				//                    cap = arr[i, j];
				//                }
			}
		}
		if (sum == 0) {
			BlackHoleAnim();
		}
    }

    // Update is called once per frame
    void Update()
    {
        //int i = 0;
        //int j = 0;
        //foreach(GameObject obj in GameObject.FindGameObjectsWithTag("finalTruck"))
        //{
        //    i++;
        //    if (obj.transform.position == GameObject.Find("depot").transform.position)
        //    {
        //        j++;
        //    }
        //    if (i == j)
        //    {
        //        BlackHoleAnim();
        //    }
        //}
        if (Camera.main.GetComponent<stickMap>().enabled == true)
        {
            NodeMove();
        }

    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(1);
        if (prof.activeSelf == false)
        {
            prof.SetActive(true);
            time.SetActive(true);
            intersection.SetActive(true);
            profText.SetActive(true);
            truckTime.SetActive(true);
            interText.SetActive(true);
        }
        Transform tarProf = GameObject.Find("ForFinalProfit").transform;
        Transform tarTime = GameObject.Find("ForFinalTime").transform;
        Transform tarIntersection = GameObject.Find("ForFinalIntersection").transform;
        Transform tarProfText = GameObject.Find("ForFinalProfitText").transform;
        Transform tarTimeText = GameObject.Find("ForFinalTimeText").transform;
        Transform tarIntersectionText = GameObject.Find("ForFinalIntersectionText").transform;
        prof.transform.position = Vector3.MoveTowards(prof.transform.position, tarProf.position, 3 * Time.deltaTime);
        time.transform.position = Vector3.MoveTowards(time.transform.position, tarTime.position, 3 * Time.deltaTime);
        intersection.transform.position = Vector3.MoveTowards(intersection.transform.position, tarIntersection.position, 3 * Time.deltaTime);
        profText.transform.position = Vector3.MoveTowards(profText.transform.position, tarProfText.position, 3 * Time.deltaTime);
        truckTime.transform.position = Vector3.MoveTowards(truckTime.transform.position, tarTimeText.position, 3 * Time.deltaTime);
        interText.transform.position = Vector3.MoveTowards(interText.transform.position, tarIntersectionText.position, 3 * Time.deltaTime);
        if (truckTime.transform.position == tarTimeText.transform.position)
        {
            //Application.targetFrameRate = 1;
            while (float.Parse(profText.GetComponent<Text>().text) < minP)
            {
                float t = float.Parse(profText.GetComponent<Text>().text);
                t += 1;
                if (Mathf.Abs(minP - t) <= 10)
                {
                    t = minP;
                }
                profText.GetComponent<Text>().text = t.ToString();
            }
            while (float.Parse(truckTime.GetComponent<Text>().text) < maxT)
            {
                float t = float.Parse(truckTime.GetComponent<Text>().text);
                t += 1;
                if (Mathf.Abs(maxT - t) <= 10)
                {
                    t = maxT;
                }
                truckTime.GetComponent<Text>().text = t.ToString();
            }
            while (float.Parse(interText.GetComponent<Text>().text) < inters)
            {
                float t = float.Parse(interText.GetComponent<Text>().text);
                t += 1;
                interText.GetComponent<Text>().text = t.ToString();
            }

        }
        StartCoroutine(wait1());
    }

    IEnumerator wait1()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("end");
    }

    private void NodeMove()
    {
        bool start = false;
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Node"))
        {
            obj.transform.position = Vector3.MoveTowards(obj.transform.position, tar.position, 3 * Time.deltaTime);
            if (obj.transform.position == tar.position)
            {
                Color c = obj.GetComponent<Image>().color;
                c.a = 0;
                obj.GetComponent<Image>().color = c;
            }
        }
        GameObject.Find("leftPanel").transform.position = Vector3.MoveTowards(GameObject.Find("leftPanel").transform.position, tar.position, 3 * Time.deltaTime);
        if (GameObject.Find("leftPanel").transform.position == tar.position)
        {
            start = true;
            Color c = GameObject.Find("leftPanel").GetComponent<Image>().color;
            c.a = 0;
            //            GameObject.Find("leftPanel").GetComponent<Image>().color = c;
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("leftPanel"))
            {
                if (obj.GetComponent<Image>() != null)
                {
                    obj.GetComponent<Image>().color = c;
                }
                else if (obj.GetComponent<Text>() != null)
                {
                    obj.GetComponent<Text>().color = c;
                }
            }
        }
        if (start)
        {
            //            GameObject prof=GameObject.Find("redProfit");
            //            GameObject time = GameObject.Find("redtime");
            //			GameObject intersection = GameObject.Find ("intersection");
            //			GameObject profText = GameObject.Find ("redTruckProfit");
            //			GameObject timeText = GameObject.Find ("redTruckTime");
            //			Color c = prof.GetComponent<Image> ().color;
            //			c.a = 1;
            //			prof.GetComponent<Image> ().color = c;
            //			time.GetComponent<Image> ().color = c;
            //			intersection.GetComponent<Text> ().color = c;
            //			profText.GetComponent<Text> ().color = c;
            //			timeText.GetComponent<Text> ().color = c;
            //			prof.transform.localScale = new Vector2 (3, 3);
            //			time.transform.localScale = new Vector2 (3, 3);
            //			intersection.transform.localScale = new Vector2 (3, 3);
            //			profText.transform.localScale = new Vector2 (3, 3);
            //			timeText.transform.localScale = new Vector2 (3, 3);
            StartCoroutine(wait());
        }
    }

    void BlackHoleAnim()
    {
        Camera.main.GetComponent<stickMap>().enabled = true;
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("linerender"))
        {
            Destroy(obj);
        }
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("indi"))
        {
            Destroy(obj);
        }
        Destroy(GameObject.Find("Toggle"));
        prof.transform.SetParent(GameObject.Find("gamePanel").transform);
        prof.transform.position = GameObject.Find("depot").transform.position;
        time.transform.SetParent(GameObject.Find("gamePanel").transform);
        time.transform.position = GameObject.Find("depot").transform.position;
        intersection.transform.SetParent(GameObject.Find("gamePanel").transform);
        intersection.transform.position = GameObject.Find("depot").transform.position;
        if (prof.GetComponent<Image>() == null)
            prof.AddComponent<Image>();
        prof.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/profit") as Sprite;
        prof.GetComponent<RectTransform>().localScale = new Vector2(1.3f, 1.3f);
        prof.GetComponent<RectTransform>().sizeDelta = new Vector2(80f, 80f);
        if (time.GetComponent<Image>() == null)
            time.AddComponent<Image>();
        time.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/time") as Sprite;
        time.GetComponent<RectTransform>().localScale = new Vector2(1.3f, 1.3f);
        time.GetComponent<RectTransform>().sizeDelta = new Vector2(80f, 80f);
        if (intersection.GetComponent<Image>() == null)
            intersection.AddComponent<Image>();
        intersection.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/overlaping") as Sprite;
        intersection.GetComponent<RectTransform>().localScale = new Vector2(1.3f, 1.3f);
        intersection.GetComponent<RectTransform>().sizeDelta = new Vector2(80f, 80f);
        profText.transform.SetParent(GameObject.Find("gamePanel").transform);
        profText.transform.position = GameObject.Find("depot").transform.position;
        if (profText.GetComponent<Text>() == null)
            profText.AddComponent<Text>();
        profText.GetComponent<Text>().text = "0";
        profText.GetComponent<RectTransform>().localScale = new Vector2(1.3f, 1.3f);
        profText.GetComponent<Text>().fontSize = 40;
        //addText.GetComponent<Text> ().font = Resources.GetBuiltinResource (typeof(Font), "Arial.ttf") as Font;
        profText.GetComponent<Text>().font = Resources.Load<Font>("Font/AGENCYR") as Font;
        profText.GetComponent<Text>().fontStyle = FontStyle.Normal;
        profText.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
        //addText.GetComponent<Text> ().color = new Color (1, 0, 0, 1);
        profText.GetComponent<Text>().fontStyle = FontStyle.Bold;
        truckTime.transform.SetParent(GameObject.Find("gamePanel").transform);
        truckTime.transform.position = GameObject.Find("depot").transform.position;
        if (truckTime.GetComponent<Text>() == null)
            truckTime.AddComponent<Text>();
        truckTime.GetComponent<Text>().text = "0";
        truckTime.GetComponent<RectTransform>().localScale = new Vector2(1.3f, 1.3f);
        truckTime.GetComponent<Text>().fontSize = 40;
        //addText.GetComponent<Text> ().font = Resources.GetBuiltinResource (typeof(Font), "Arial.ttf") as Font;
        truckTime.GetComponent<Text>().font = Resources.Load<Font>("Font/AGENCYR") as Font;
        truckTime.GetComponent<Text>().fontStyle = FontStyle.Normal;
        truckTime.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
        //addText.GetComponent<Text> ().color = new Color (1, 0, 0, 1);
        truckTime.GetComponent<Text>().fontStyle = FontStyle.Bold;
        interText.transform.SetParent(GameObject.Find("gamePanel").transform);
        interText.transform.position = GameObject.Find("depot").transform.position;
        if (interText.GetComponent<Text>() == null)
            interText.AddComponent<Text>();
        interText.GetComponent<Text>().text = "0";
        interText.GetComponent<RectTransform>().localScale = new Vector2(1.3f, 1.3f);
        interText.GetComponent<Text>().fontSize = 40;
        //addText.GetComponent<Text> ().font = Resources.GetBuiltinResource (typeof(Font), "Arial.ttf") as Font;
        interText.GetComponent<Text>().font = Resources.Load<Font>("Font/AGENCYR") as Font;
        interText.GetComponent<Text>().fontStyle = FontStyle.Normal;
        interText.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
        //addText.GetComponent<Text> ().color = new Color (1, 0, 0, 1);
        interText.GetComponent<Text>().fontStyle = FontStyle.Bold;
        prof.SetActive(false);
        time.SetActive(false);
        intersection.SetActive(false);
        profText.SetActive(false);
        truckTime.SetActive(false);
        interText.SetActive(false);
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("path"))
        {
            Destroy(obj);
        }
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("cap"))
        {
            Destroy(obj);
        }
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("finalTruck"))
        {
            Destroy(obj);
        }
        Destroy(GameObject.Find("miniMap"));
    }
}
