using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine.UI;

public class givenSolution : MonoBehaviour {

    GameObject go;
    private float minP;
    private static float maxT;
    private static int inters;
    
    void Start()
    {
        go = GameObject.Find("givenSolutionPanel");
    }

    void OnMouseDown()
    {
        GameObject solutionImage = GameObject.Find("ImageForGivenSolution");
        solutionImage.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        solutionImage.GetComponent<Image>().raycastTarget = true;
        GameObject gobackButton = GameObject.Find("goback");
        gobackButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        gobackButton.GetComponent<BoxCollider2D>().enabled = true;
    }

    void OnMouseEnter()
    {
        go.transform.Find("profit").GetComponent<Image>().enabled = true;
        go.transform.Find("time").GetComponent<Image>().enabled = true;
        go.transform.Find("intersection").GetComponent<Image>().enabled = true;
        go.transform.Find("informationProfitText").GetComponent<Text>().enabled = true;
        go.transform.Find("informationTimeText").GetComponent<Text>().enabled = true;
        go.transform.Find("informationIntersectionText").GetComponent<Text>().enabled = true;
    }

    void OnMouseExit()
    {
        go.transform.Find("profit").GetComponent<Image>().enabled = false;
        go.transform.Find("time").GetComponent<Image>().enabled = false;
        go.transform.Find("intersection").GetComponent<Image>().enabled = false;
        go.transform.Find("informationProfitText").GetComponent<Text>().enabled = false;
        go.transform.Find("informationTimeText").GetComponent<Text>().enabled = false;
        go.transform.Find("informationIntersectionText").GetComponent<Text>().enabled = false;
    }
}
