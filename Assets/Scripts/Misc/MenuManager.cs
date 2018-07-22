using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject howToPlay;

    public void Start()
    {
        mainMenu.active = true;
        howToPlay.active = false;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }

    public void HowToPlay()
    {
        mainMenu.active = false;
        howToPlay.active = true;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Back()
    {
        mainMenu.active = true;
        howToPlay.active = false;
    }
}
