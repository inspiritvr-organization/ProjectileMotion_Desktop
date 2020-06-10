using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefineSize : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.parent != null && transform.parent.tag == "Weight")
        {
            print("scale" + transform.parent.localScale.x);
            float sizeFactor = 1/transform.parent.localScale.x * 10f;
            transform.localScale *= sizeFactor;
        }
    }
}
