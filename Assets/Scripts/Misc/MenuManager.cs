using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject help;

    public void Start()
    {
        mainMenu.active = true;
        help.active = false;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }

    public void Help()
    {
        mainMenu.active = false;
        help.active = true;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Back()
    {
        mainMenu.active = true;
        help.active = false;
    }
}
