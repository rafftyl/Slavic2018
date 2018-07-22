using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject help;
    public GameObject howToPlay;
    public GameObject dances;
    public GameObject controls;
    public GameObject credits;
    private GameObject[] menus;

    public void Start()
    {
        Assert.IsNotNull(mainMenu, "Missing main menu");
        Assert.IsNotNull(help, "Missing help");
        Assert.IsNotNull(howToPlay, "Missing how to play");
        Assert.IsNotNull(dances, "Missing dances");
        Assert.IsNotNull(controls, "Missing controls");
        Assert.IsNotNull(credits, "Missing credits");
        menus = new GameObject[6];
        menus[0] = mainMenu;
        menus[1] = help;
        menus[2] = howToPlay;
        menus[3] = dances;
        menus[4] = controls;
        menus[5] = credits;
        ShowMainMenu();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }

    public void ShowMainMenu()
    {
        ClearMenus();
        mainMenu.active = true;
    }

    public void ShowHelpMenu()
    {
        ClearMenus();
        help.active = true;
    }

    public void ShowHowToPlayMenu()
    {
        ClearMenus();
        howToPlay.active = true;
    }

    public void ShowDancesMenu()
    {
        ClearMenus();
        dances.active = true;
    }

    public void ShowControlsMenu()
    {
        ClearMenus();
        controls.active = true;
    }

    public void ShowCreditsMenu()
    {
        ClearMenus();
        credits.active = true;
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void ClearMenus()
    {
        for(int i = 0; i < menus.Length; ++i)
        {
            menus[i].active = false;
        }
    }
}
