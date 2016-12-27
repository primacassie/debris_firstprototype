using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RefreshButton : MonoBehaviour{

    void OnMouseDown()
    {
        SceneManager.LoadScene("start");
    }
}
