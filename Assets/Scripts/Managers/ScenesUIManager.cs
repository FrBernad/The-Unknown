using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesUIManager : MonoBehaviour
{
    static public ScenesUIManager instance;

   private void Awake()
    {
        if (instance != null) Destroy(gameObject);
        instance = this;
    }

    public void LoadMenuScene() => SceneManager.LoadScene("Main Menu");

    public void LoadLevelScene()
    {
        SceneManager.LoadScene("Loading Screen");
    }
    public void CloseGame() => Application.Quit();
}