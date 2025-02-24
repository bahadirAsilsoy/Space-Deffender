using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject menu;
    public GameObject secim;

    public void Start()
    {
        menu.SetActive(true);
        secim.SetActive(false);
    }

    public void playGame()
    {
        menu.SetActive(false);
        secim.SetActive(true);
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
