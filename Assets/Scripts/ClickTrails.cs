using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class ClickTrails : MonoBehaviour {
    void OnMouseDown()
    {
        
    }

    void OnMouseEnter()
    {
        string name = this.gameObject.name;
        string resultString = Regex.Match(name, @"\d+").Value;
        int num = int.Parse(resultString);
    }

    void OnMouseExit()
    {

    }
}
