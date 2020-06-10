using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsMenuUI : MonoBehaviour
{
    public GameObject MainMenu;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            BacktoMainMenu();
        }
    }

    public void BacktoMainMenu()
    {
        MainMenu.GetComponent<MainMenuUI>().MainPanel.SetActive(true);
        MainMenu.GetComponent<MainMenuUI>().enable = true;
        gameObject.SetActive(false);
    }
}
