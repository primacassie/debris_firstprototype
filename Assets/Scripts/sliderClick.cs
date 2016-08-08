using UnityEngine;
using System.Collections;

public class sliderClick : MonoBehaviour {
    public static string sliderName;
    void OnMouseDown()
    {
        sliderName = this.gameObject.name;
        //Debug.Log(sliderName);
    }
}
