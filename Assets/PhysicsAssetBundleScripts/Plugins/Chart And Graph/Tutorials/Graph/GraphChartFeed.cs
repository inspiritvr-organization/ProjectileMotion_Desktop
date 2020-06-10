#define Graph_And_Chart_PRO
using UnityEngine;
using ChartAndGraph;
using System;
using System.Collections;

///   Original GraphChartFeed///

//public class GraphChartFeed : MonoBehaviour
//{
//	void Start ()
//    {
//        GraphChartBase graph = GetComponent<GraphChartBase>();
//        if (graph != null)
//        {
//            graph.HorizontalValueToStringMap[0.0] = "Zero"; // example of how to set custom axis strings
//            graph.DataSource.StartBatch();
//            graph.DataSource.ClearCategory("Player 1");
//            graph.DataSource.ClearAndMakeBezierCurve("Player 2");

//            for (int i = 0; i <5; i++)
//            {
//                graph.DataSource.AddPointToCategory("Player 1",i,UnityEngine.Random.value*10f + 20f);
//                if (i == 0)
//                    graph.DataSource.SetCurveInitialPoint("Player 2",i, UnityEngine.Random.value * 10f + 10f);
//                else
//                    graph.DataSource.AddLinearCurveToCategory("Player 2", new DoubleVector2(i , UnityEngine.Random.value * 10f + 10f));
//            }
//            graph.DataSource.MakeCurveCategorySmooth("Player 2");
//            graph.DataSource.EndBatch();
//        }
//       // StartCoroutine(ClearAll());
//    }

//    IEnumerator ClearAll()
//    {
//        yield return new WaitForSeconds(5f);
//        GraphChartBase graph = GetComponent<GraphChartBase>();

//        graph.DataSource.Clear();
//    }
//}

/// //// ///

public class GraphChartFeed : MonoBehaviour
{
    public Tuple<float[], float[]>[] dataset = new Tuple<float[], float[]>[5];

    void Start()
    {
        // StartCoroutine(ClearAll());
    }

    public void PopulateGraph()
    {

        GraphChartBase graph = GetComponent<GraphChartBase>();
        if (graph != null)
        {
            graph.HorizontalValueToStringMap[0.0] = "Zero"; // examplse of how to set custom axis strings
            graph.DataSource.StartBatch();

            graph.DataSource.ClearCategory("Player 1");
            graph.DataSource.ClearCategory("player 2");
            //graph.DataSource.ClearAndMakeBezierCurve("Player 2");

            for (int i = 0; i < dataset.Length; i++)
            {
                for (int j = 0; j < dataset[i].Item1.Length; j++)
                {
                    graph.DataSource.AddPointToCategory("Player " + (i + 1).ToString(), dataset[i].Item1[j], dataset[i].Item2[j]);
                }
            }
            //graph.DataSource.MakeCurveCategorySmooth("Player 2");
            graph.DataSource.EndBatch();
        }
    }

    IEnumerator ClearAll()
    {
        yield return new WaitForSeconds(5f);
        GraphChartBase graph = GetComponent<GraphChartBase>();

        graph.DataSource.Clear();
    }
}
