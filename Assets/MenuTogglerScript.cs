using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTogglerScript : MonoBehaviour
{
    public GameObject[] allMenus; // je rajoute dans Unity tous les menus à gérer

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenus();
        }
    }

    void ToggleMenus()
    {
        // Fermer tous les menus actifs
        foreach (GameObject menu in allMenus)
        {
            if (menu.activeSelf)
            {
                menu.SetActive(false);
            }
        }

        // Ouvrir le premier menu si aucun menu n'est ouvert
        if (NoMenusOpen())
        {
            if (allMenus.Length > 0)
            {
                allMenus[0].SetActive(true);
            }
        }
    }

    bool NoMenusOpen()
    {
        foreach (GameObject menu in allMenus)
        {
            if (menu.activeSelf)
            {
                return false;
            }
        }
        return true;
    }
}
