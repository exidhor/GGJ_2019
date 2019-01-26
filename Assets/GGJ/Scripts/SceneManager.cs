using UnityEngine;
using System.Collections;
using Tools;

public class SceneManager : MonoSingleton<SceneManager>
{
    void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    public void Fade()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1, UnityEngine.SceneManagement.LoadSceneMode.Additive);

        Invoke("ReloadGame", 0.5f);
    }

    void ReloadGame()
    {
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(0);


        Invoke("ReloadMainScene", 0.2f);
    }

    void ReloadMainScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0, UnityEngine.SceneManagement.LoadSceneMode.Additive);

        Invoke("UnloadScene", 1f);
    }

    void UnloadScene()
    {
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(1);
    }
}
