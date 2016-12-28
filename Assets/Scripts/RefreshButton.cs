using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RefreshButton : MonoBehaviour{
	public static bool refresh;

    void OnMouseDown()
    {
		refresh = true;
        SceneManager.LoadScene("start");
    }
}
