using UnityEngine;

namespace DefaultNamespace
{
    public class MenuLogic : MonoBehaviour
    {
        [SerializeField] private GameObject mainMenu = null;
        [SerializeField] private GameObject optionsMenu = null;
        [SerializeField] private GameObject informationMenu = null;
        private GameObject _currentMenu;

        private void Start()
        {
            _currentMenu = mainMenu;
            _currentMenu.SetActive(true);
            optionsMenu.SetActive(false);
            informationMenu.SetActive(false);
        }


        public void LoadInfoScene()
        {
            SwapCurrentMenu(informationMenu);
        }

        public void LoadOptionScene()
        {
            SwapCurrentMenu(optionsMenu);
        }

        public void BackToMainMenu()
        {
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
    }
}