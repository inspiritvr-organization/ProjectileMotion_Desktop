using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLighting : MonoBehaviour
{
    public Material skybox;
    public Light sunlight;
    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.skybox = skybox;
        RenderSettings.sun = sunlight;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
