using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphInteractionCenterScreen : MonoBehaviour
{
    public StreamingGraphProj streamGraph;

    public void Clicked(ChartAndGraph.GraphChartBase.GraphEventArgs arg)
    {
        print("clicked");
        streamGraph.PlaceTrailPointer((float)arg.Value.x);                      // passes time of the selected point as an argument to StreamingGraphProj(PLaceTrailPOinter)
    }
}
