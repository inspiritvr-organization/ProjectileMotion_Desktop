using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfettiTrail : MonoBehaviour
{
    /* Applies confetti trail to the parent based on its velocity*/

    GameObject obj;

    // Start is called before the first frame update
    void Start()
    {
        obj = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (obj.GetComponent<Rigidbody>().velocity.magnitude > 5f)
            Launched();
        else
            AtRest();
        
    }

    void AtRest()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    void Launched()
    {
        Vector3 velocity = obj.GetComponent<Rigidbody>().velocity;
        float angle = Vector3.Angle(velocity, new Vector3(velocity.x, 0, velocity.z));
        transform.rotation = Quaternion.AngleAxis(-angle, obj.transform.right);                                                     // such that the trail is following the object ion the right angle
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
