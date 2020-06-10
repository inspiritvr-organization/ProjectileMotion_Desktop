using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject anchor;
    public GameObject spring;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spring.transform.position = (this.transform.position + anchor.transform.position)/2;
        float distance = Vector3.Distance(this.transform.position, anchor.transform.position);
        spring.transform.localScale = new Vector3(spring.transform.localScale.x, distance, spring.transform.localScale.z);
        
    }
}
