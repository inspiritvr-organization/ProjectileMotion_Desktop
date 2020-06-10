using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleRotate : MonoBehaviour
{

    public Vector3 rotateDirection;
    [Space(20)]
    public float rotateSpeed;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(rotateImage());
    }

    IEnumerator rotateImage()
    {
        while (true)
        {
            this.transform.Rotate(rotateDirection, rotateSpeed * Time.deltaTime, Space.Self);
            yield return null;
        }
    }

}
