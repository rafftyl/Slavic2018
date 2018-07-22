using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchablePanelMenu : MonoBehaviour
{
    [SerializeField]
    GameObject[] menus;
    int menuIndex = 0;

    void Start()
    {
        SetMenu(0);
    }

    public void SetMenu(int index)
    {
        menus[menuIndex].SetActive(false);
        menuIndex = index;
        menus[menuIndex].SetActive(true);
    }
}
