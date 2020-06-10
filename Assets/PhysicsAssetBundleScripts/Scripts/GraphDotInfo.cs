using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GraphDotInfo : MonoBehaviour
{
    //Stores the values of the point;
    public float pointX;
    public float pointY;
    public StreamingGraphProj streamGraph;
    Vector3 offset = new Vector3(-0.1f, 0.05f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hovered()
    {
        streamGraph.pointText.enabled = true;
        streamGraph.pointText.transform.position = this.transform.position + offset;
        streamGraph.pointText.text = pointX.ToString("f") + ", " + pointY.ToString("f");
    }

    public void Unhovered()
    {
        streamGraph.pointText.enabled = false;
        //streamGraph.pointText.text = pointX.ToString("f") + ", " + pointY.ToString("f");
    }

    public void Clicked()
    {
        print("clicked");
        streamGraph.PlaceTrailPointer(pointX);                      // passes time of the selected point as an argument to StreamingGraphProj(PLaceTrailPOinter)
    }

}
