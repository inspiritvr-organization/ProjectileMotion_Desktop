using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullyAngle : MonoBehaviour
{
    Vector3 orig_angle;
    Vector3 orig_pulley_angle;

    // Start is called before the first frame update
    void Start()
    {
        //print(transform.eulerAngles);
        orig_angle = transform.parent.eulerAngles;
        orig_pulley_angle = transform.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        print(transform.eulerAngles);
        print(transform.parent.eulerAngles + "parent");
        transform.localEulerAngles = -(transform.parent.eulerAngles - orig_angle) + orig_pulley_angle;
    }
}
