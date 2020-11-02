#define Graph_And_Chart_PRO
using UnityEngine;
using System;
using System.Collections.Generic;
using ChartAndGraph;
using TMPro;

namespace ProjectileMotionDesktop {
    public class StreamingGraphProj : MonoBehaviour
    {

        public Tuple<float[], float[]>[] dataset = new Tuple<float[], float[]>[1];
        bool populateGraph;
        public Transform GraphDotLocator;
        public GameObject GraphDotPrefab;
        public GraphChart Graph;
        public TextMeshProUGUI pointText;
        public TrailPointer trail;
        public int TotalPoints = 5;
        float lastTime = 0f;
        float lastX = 0f;
        int index = 0;
        bool start = false;
        int iterations;
        DateTime orig_time;
        float totaltime;
        float timegap = 0.02f;
        List<GameObject> dots = new List<GameObject>();
        public float initial_velocity;

        void Start()
        {
            if (Graph == null) // the ChartGraph info is obtained via the inspector
                return;
            //float x = 3f * TotalPoints;
            Graph.DataSource.StartBatch(); // calling StartBatch allows changing the graph data without redrawing the graph for every change
            Graph.DataSource.ClearCategory("Player 1"); // clear the "Player 1" category. this category is defined using the GraphChart inspector
                                                        //Graph.DataSource.ClearCategory("Player 2"); // clear the "Player 2" category. this category is defined using the GraphChart inspector

            //for (int i = 0; i < TotalPoints; i++)  //add random points to the graph
            //{
            //    Graph.DataSource.AddPointToCategory("Player 1", System.DateTime.Now - System.TimeSpan.FromSeconds(x), Random.value * 20f + 10f); // each time we call AddPointToCategory 
            //    Graph.DataSource.AddPointToCategory("Player 2", System.DateTime.Now  - System.TimeSpan.FromSeconds(x), Random.value * 10f); // each time we call AddPointToCategory 
            //    x -= Random.value * 3f;
            //    lastX = x;
            //}
            timegap = transform.parent.parent.GetComponent<PopulateGraphProjectile>().timegap;
            Graph.DataSource.EndBatch(); // finally we call EndBatch , this will cause the GraphChart to redraw itself
        }

        void Update()
        {
            if (populateGraph)
            {
                PopulateGraph();
            }
            else
            {
                start = false;
                index = 0;

            }
        }

        public void Populate(int iterations, float totaltime)
        {
            this.iterations = iterations;
            populateGraph = true;
            this.totaltime = totaltime;
        }

        public void PopulateGraph()
        {
            if (!start)
            {
                orig_time = System.DateTime.Now;
                Graph.DataSource.ClearCategory("Player 1");
                start = true;
                foreach (GameObject d in dots)
                {
                    Destroy(d);
                }
                dots.Clear();
            }
            float time = Time.time;

            Graph.DataSource.HorizontalViewSize = Mathf.Ceil(totaltime);

            if (lastTime + timegap < time && index <= iterations)
            {
                lastTime = time;
                //lastX += Random.value * 3f;
                //System.DateTime t = ChartDateUtility.ValueToDate(lastX);
                //public void AddPointToCategoryRealtime(string category, double x, double y, double slideTime = 0f, double pointSize = -1f)
                float pointx;
                float pointy;
                if (index == iterations)
                {
                    pointx = totaltime;
                    pointy = dataset[0].Item2[index];
                    Graph.DataSource.AddPointToCategoryRealtime("Player 1", pointx, pointy, 0f);
                    populateGraph = false;
                }
                else
                {
                    pointx = index * timegap;
                    pointy = dataset[0].Item2[index];
                    Graph.DataSource.AddPointToCategoryRealtime("Player 1", pointx, pointy, 0f); // each time we call AddPointToCategory 
                }


                index++;

                // ***** Spawning Dots for VR, not required for desktop
                //DoubleVector3 last;

                //if (Graph.DataSource.GetLastPoint("Player 1", out last))
                //{
                //    Vector3 pos;
                //    if (Graph.PointToWorldSpace(out pos, last.x, last.y, "Player 1"))
                //    {
                //        if (Graph != null)
                //        {
                //            GraphDotLocator.position = pos;
                //            GameObject gdot = Instantiate(GraphDotPrefab, GraphDotLocator);
                //            gdot.transform.SetParent(transform);
                //            gdot.GetComponent<GraphDotInfo>().pointX = pointx;
                //            gdot.GetComponent<GraphDotInfo>().pointY = pointy;
                //            gdot.GetComponent<GraphDotInfo>().streamGraph = this;
                //            dots.Add(gdot);
                //        }
                //    }
                //}
                //******  Spawning Dots for VR, not required for desktop


                //Graph.DataSource.AddPointToCategoryRealtime("Player 2", System.DateTime.Now, UnityEngine.Random.value * 10f, 1f); // each time we call AddPointToCategory
            }
            //Graph.DataSource.AddPointToCategoryRealtime("Player 1", totaltime, dataset[0].Item2[index], 0f);

        }

        public void PlaceTrailPointer(float time)
        {
            print("placetrailstreaming graph");
            trail.GetComponent<MeshRenderer>().enabled = true;
            trail.PlaceTrailPointer(initial_velocity, time);                       //Passes initial velocity and time as arguments to trailpointer PLaceTrailPOinter
        }

        public void AdjustVerticalView(float size)
        {
            Graph.DataSource.VerticalViewSize = size;
        }

        public void AdjustAbsoluteVerticalView(float size)
        {
            Graph.DataSource.VerticalViewOrigin = -size;
            Graph.DataSource.VerticalViewSize = 2 * size;
        }

    }
}


