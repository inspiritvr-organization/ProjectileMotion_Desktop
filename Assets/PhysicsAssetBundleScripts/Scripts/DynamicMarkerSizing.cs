using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicMarkerSizing : MonoBehaviour
{
    Vector3 orig_scale;

    void Start()
    {
        orig_scale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = (0.5f) * orig_scale * Vector3.Distance(transform.position, Camera.main.transform.position);
    }
}
