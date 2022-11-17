using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class ScenesUIManager : MonoBehaviour
    {
        public static ScenesUIManager instance;

        private void Awake()
        {
            if (instance != null) Destroy(gameObject);
            instance = this;
        }

        public void LoadMenuScene()
        {
            SceneManager.LoadScene("Main Menu");
        }

        public void LoadLevelScene()
        {
            SceneManager.LoadScene("Loading Screen");
        }

        public void CloseGame()
        {
            Application.Quit();
        }
    }
}