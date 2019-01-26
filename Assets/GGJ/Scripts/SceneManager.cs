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
    }

    void ReloadGame()
    {
        // todo
    }
}
