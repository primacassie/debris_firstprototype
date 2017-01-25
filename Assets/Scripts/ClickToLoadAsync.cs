using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClickToLoadAsync : MonoBehaviour {

    public Slider loadingBar;
    public GameObject LoadingImage;

    private AsyncOperation async;
    private Scene sceneName;
    public static int solution;
    // Use this for initialization

    private void Awake()
    {
        async = new AsyncOperation();
        sceneName = SceneManager.GetActiveScene();
        Debug.Log(sceneName.name);
    }

    public void ClickAsync()
    {
        LoadingImage.SetActive(true);
        StartCoroutine(LoadLevelWithBar());
    }

    IEnumerator LoadLevelWithBar()
    {
        if (sceneName.name == "menu")
        {
            async = SceneManager.LoadSceneAsync("LevelChoose");
        }

        if (sceneName.name == "LevelChoose")
        {
            if (this.name == "s1")
            {
                solution = 1;
            }
            else if (this.name == "s2")
            {
                solution = 2;
            }
            else if (this.name == "s3")
            {
                solution = 3;
            }
            else
            {
                solution = 0;
            }
            async = SceneManager.LoadSceneAsync("start");
        }

        while (!async.isDone)
        {
            loadingBar.value = async.progress;
            yield return null;
        }
    }

}
