using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChartAndGraph;

public class PopulateGraphIncline : MonoBehaviour
{
    GraphChartFeed graph;
    float inclineForce;
    float objForce;
    float inclineFrictionForce;
    float mass;
    float[] friction;
    public bool weight_objectmoveup;
    public bool weight_objectmovedown;


    // Start is called before the first frame update
    void Start()
    {
        graph = GetComponent<GraphChartFeed>();
        mass = 2;
        friction = new float[5] { 0.8f, 0.5f, 0.3f, 0.1f, 0f };
        Populate();
    }

    void Populate()
    {
        for (int i = 0; i < 5; i++) 
        {
            float[] angle = new float[61];
            float[] force = new float[61];
            for (int j = 0; j <= 60; j++)
            {
                angle[j] = j;
                inclineForce = mass * -Physics.gravity.y * Mathf.Sin(angle[j] * Mathf.Deg2Rad);
                objForce = mass * Physics.gravity.y;
                inclineFrictionForce = mass * -Physics.gravity.y * friction[i] * Mathf.Cos(angle[j] * Mathf.Deg2Rad);
                if(weight_objectmoveup)
                    force[j] = (inclineForce + inclineFrictionForce);
                if(weight_objectmovedown)
                    force[j] = (inclineForce - inclineFrictionForce);
            }
            graph.dataset[i] = new Tuple<float[], float[]> (angle, force);
        }
        graph.PopulateGraph();
    }

    // Update is called once per frame
    void Update()
    { }
}
