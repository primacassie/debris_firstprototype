using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class tutorial : MonoBehaviour {

    // Use this for initialization
    private int num = 1;
    private Image i;

    void Start () {
        i = GetComponent<Image>();
        i.sprite= Resources.Load<Sprite>("Tutorial/Slide1") as Sprite;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0) && num<6)
        {
            num++;
            string page = "Tutorial/Slide" + num.ToString();
            i.sprite= Resources.Load<Sprite>(page) as Sprite;
        }

        if(Input.GetMouseButtonDown(1) && num > 1)
        {
            num--;
            string page = "Tutorial/Slide" + num.ToString();
            i.sprite = Resources.Load<Sprite>(page) as Sprite;
        }

        if (Input.GetMouseButtonDown(1) && num == 1)
        {
            SceneManager.LoadScene("menu");
        }

    }
}
