using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonInteraction : MonoBehaviour
{   
    public GameObject cannonCanvas;
    public GameObject[] menus;
    int layerMask;
    MouseLook mouselook;
    public bool cannonUIenable = false;
    public GameObject cannonScreen;
    public GameObject pointerScreen;

    // Start is called before the first frame update
    void Start()
    {
        layerMask = LayerMask.GetMask("Cannon");
        mouselook = transform.parent.GetComponent<MouseLook>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject menu in menus)
        {
            if (menu.activeSelf == true)
                return;
        }
        if(!cannonUIenable)
            Raycast();
        if (cannonUIenable)
        {
            if (Input.anyKeyDown && Input.GetMouseButtonDown(0) == false)
            {
                CannonEnable(false);
            }
        }
    }

    void Raycast()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));  // ray to the middle of the screen
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 3f, layerMask))               // raycast till 3m from the camera
        {
            if(Input.GetMouseButtonDown(0))                              
            {
                cannonScreen.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                cannonScreen.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
                cannonScreen.transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
                CannonEnable(true);
            }
        }
    }

    public void CannonEnable(bool dec)
    {
        mouselook.freeze_view = dec;                   //Freezes camera rotation
        Cursor.visible = dec;
        cannonCanvas.SetActive(dec);                   //Activates camera canvas
        cannonUIenable = dec;                          // bool when cannonUI enable is on
        pointerScreen.SetActive(!dec);
    }

}
