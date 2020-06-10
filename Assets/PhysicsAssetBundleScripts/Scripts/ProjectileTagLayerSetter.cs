using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTagLayerSetter : MonoBehaviour
{
    public List<GameObject> objectListRef = new List<GameObject>();
    public string tagName;
    public string layerName;
    public bool setChild = false;
    void Start()
    {
        if (tagName == String.Empty)
            tagName = "Untagged";
        if (layerName == String.Empty)
            layerName = "Default";
        foreach (GameObject x in objectListRef)
        {

            if (setChild)
            {
                SetLayerRecursively(x, tagName, layerName);
            }
            else
            {
                if (tagName != null)
                    x.tag = tagName;
                if (layerName != null)
                    x.layer = LayerMask.NameToLayer(layerName);
            }
        }
    }


    void SetLayerRecursively(GameObject x, string tagName, string layerName)
    {
        x.tag = tagName;
        x.layer = LayerMask.NameToLayer(layerName);

        foreach (Transform child  in x.transform)
        {
            SetLayerRecursively(child.gameObject, tagName, layerName);
        }
    }
}
