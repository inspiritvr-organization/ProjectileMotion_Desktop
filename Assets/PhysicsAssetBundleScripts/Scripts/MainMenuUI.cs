using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public MouseLook mouseLook;
    public CannonInteraction cannonUI;
    public GameObject MainPanel;
    public GameObject ControlsPanel;
    public GameObject PointerCenterScreen;
    public bool enable = true;
    public bool active = false;

    private void Start()
    {
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) &&  cannonUI.cannonUIenable == false)
        {
            if (enable)
            {
                active = !active;
                MenuVisibility();
            }
        }

    }

    public void Controls()
    {
        ControlsPanel.SetActive(true);
        enable = false;
        MainPanel.SetActive(false);
    }

    public void ExitToDesktop()
    {
        Application.Quit();
    }

    public void Cancel()
    {
        active = false;
        MenuVisibility();
    }

    public void MenuVisibility()
    {
        if (active)
        {
            MouseLook.freeze_view = true;
            PointerCenterScreen.SetActive(false);
        }
        else
        {
            MouseLook.freeze_view = false;
            PointerCenterScreen.SetActive(true);
        }
        MainPanel.SetActive(active);
    }

}
