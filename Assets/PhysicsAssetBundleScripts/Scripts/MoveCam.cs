using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCam : MonoBehaviour
{
    public float height = 1.2f;
    private float speed = 2.0f;

    void Update()
    {

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += -transform.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W))
        {
            //Vector3 forw = new Vector3(transform.forward.x, height, transform.forward.z);
            transform.position += transform.forward * speed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, height, transform.position.z);
        }
        if (Input.GetKey(KeyCode.S))
        {
            //Vector3 forw = new Vector3(transform.forward.x, height, transform.forward.z);
            transform.position -= transform.forward * speed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, height, transform.position.z);
        }
    }
}
