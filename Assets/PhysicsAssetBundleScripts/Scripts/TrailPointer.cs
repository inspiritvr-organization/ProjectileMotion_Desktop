using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailPointer : MonoBehaviour
{
    Vector3 orig_posn;
    // Start is called before the first frame update
    void Start()
    {
        orig_posn = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaceTrailPointer(float velocity, float time)
    {
        print("placetrailpointer");
        print(velocity);
        Vector3 init_velo = -transform.forward * velocity;                  // Direction of initial velocity
        transform.position = orig_posn + init_velo * time + (0.5f) * Physics.gravity * time * time; // position in projectile motion after time seconds
    }

}
