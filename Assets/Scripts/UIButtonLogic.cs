using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtonLogic : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject informationMenu;
    private GameObject _currentMenu;

    private void Start()
    {
        _currentMenu = mainMenu;
        _currentMenu.SetActive(true);
        optionsMenu.SetActive(false);
        informationMenu.SetActive(false);
    }

    public void LoadMenuScene() => SceneManager.LoadScene("Main Menu");

    public void LoadLevelScene()
    {
        SceneManager.LoadScene("Loading Screen");
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

    public void CloseGame() => Application.Quit();
}