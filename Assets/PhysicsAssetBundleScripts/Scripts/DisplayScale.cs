using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayScale : MonoBehaviour
{
    TextMeshProUGUI[] text;
    
    // Start is called before the first frame update
    void Start()
    {
        text = transform.Find("Canvas").GetComponentsInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        float mass = 10f * Mathf.Pow(transform.lossyScale.y / 10, 2);
        GetComponent<Rigidbody>().mass = mass;
       foreach(TextMeshProUGUI t in text)
            t.text = mass.ToString("f");
    }
}
