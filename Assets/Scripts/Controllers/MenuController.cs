using UnityEngine;

namespace DefaultNamespace
{
    public class MenuLogic : MonoBehaviour
    {
        [SerializeField] private GameObject mainMenu;
        [SerializeField] private GameObject controlsMenu;
        [SerializeField] private GameObject informationMenu;

        [SerializeField] private AudioClip backClip;
        [SerializeField] private AudioClip clickClip;

        private AudioSource _audioSource;


        private GameObject _currentMenu;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.loop = false;
            _currentMenu = mainMenu;
            _currentMenu.SetActive(true);
            controlsMenu.SetActive(false);
            informationMenu.SetActive(false);
        }

        public void LoadGame()
        {
            clickSound(clickClip);
            ScenesUIManager.instance.LoadLevelScene();
        }

        public void QuitGame()
        {
            clickSound(clickClip);
            ScenesUIManager.instance.CloseGame();
        }

        public void LoadInfoScene()
        {
            clickSound(clickClip);
            SwapCurrentMenu(informationMenu);
        }

        public void LoadControlScene()
        {
            clickSound(clickClip);
            SwapCurrentMenu(controlsMenu);
        }

        public void BackToMainMenu()
        {
            clickSound(backClip);
            _currentMenu.SetActive(false);
            _currentMenu = mainMenu;
            _currentMenu.SetActive(true);
        }

        private void SwapCurrentMenu(GameObject menuObject)
        {
            _currentMenu = menuObject;
            mainMenu.SetActive(false);
            _currentMenu.SetActive(true);
        }

        private void clickSound(AudioClip audioClip)
        {
            _audioSource.clip = audioClip;
            _audioSource.Play();
        }
    }
}