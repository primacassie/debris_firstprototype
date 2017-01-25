using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RefreshButton : MonoBehaviour{
	public static bool refresh;

    void OnMouseDown()
    {
        if (this.name == "RefreshButton")
        {
            refresh = true;
            SceneManager.LoadScene("start");
        }

        if (this.name == "NewSolution")
        {
            SceneManager.LoadScene("start");
        }

    }
}
