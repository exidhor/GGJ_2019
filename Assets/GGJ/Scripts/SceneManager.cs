using UnityEngine;
using System.Collections;
using Tools;

public class SceneManager : MonoSingleton<SceneManager>
{
    public bool hasLoad;

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
        //hasLoad = true;

        //UnityEngine.SceneManagement.SceneManager.LoadScene(1, UnityEngine.SceneManagement.LoadSceneMode.Additive);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);

        //Invoke("ReloadGame", 0.5f);
    }

    //void ReloadGame()
    //{
    //    UnityEngine.SceneManagement.SceneManager.UnloadScene(0);


    //    Invoke("ReloadMainScene", 1f);
    //}

    //void ReloadMainScene()
    //{
    //    UnityEngine.SceneManagement.SceneManager.LoadScene(0, UnityEngine.SceneManagement.LoadSceneMode.Additive);

    //    Invoke("UnloadScene", 2f);
    //}

    //void UnloadScene()
    //{
    //    UnityEngine.SceneManagement.SceneManager.UnloadScene(1);
    //}
}
