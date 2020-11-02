using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailManager : MonoBehaviour
{

    public GameObject trail;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.isKinematic)
        {
            trail.GetComponent<TrailRenderer>().Clear();
            trail.SetActive(false);
        }
        else trail.SetActive(false);
    }
}
