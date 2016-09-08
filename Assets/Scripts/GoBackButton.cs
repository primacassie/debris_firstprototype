using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GoBackButton : MonoBehaviour {

    void OnMouseDown()
    {
        GameObject givenSolutionImage = GameObject.Find("ImageForGivenSolution");
        givenSolutionImage.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        givenSolutionImage.GetComponent<Image>().raycastTarget = false;
        GetComponent<Image>().color = new Color(1, 1, 1, 0);
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
