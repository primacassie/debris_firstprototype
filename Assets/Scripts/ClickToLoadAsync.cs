using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClickToLoadAsync : MonoBehaviour {

    public Slider loadingBar;
    public GameObject LoadingImage;

    private AsyncOperation async;
	// Use this for initialization

    public void ClickAsync()
    {
        LoadingImage.SetActive(true);
        StartCoroutine(LoadLevelWithBar());
    }

    IEnumerator LoadLevelWithBar()
    {
        async = SceneManager.LoadSceneAsync("start");
        while (!async.isDone)
        {
            loadingBar.value = async.progress;
            yield return null;
        }
    }

}
