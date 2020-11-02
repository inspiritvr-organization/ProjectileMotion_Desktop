using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    public Button loademptyscene;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            loademptyscene.onClick.Invoke();
    }
}
