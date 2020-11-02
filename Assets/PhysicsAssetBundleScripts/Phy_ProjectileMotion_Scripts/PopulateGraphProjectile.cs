using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChartAndGraph;

namespace ProjectileMotionDesktop
{
    public class PopulateGraphProjectile : MonoBehaviour
    {
        public Launch launchdata;
        public StreamingGraphProj graph_pos;
        public StreamingGraphProj graph_vel;
        public float timegap = 0.02f;
        protected float velocity;




        // Start is called before the first frame update
        void Start()
        {

        }

        public void Populate()
        {
            print("graph called");
            velocity = launchdata.velo;
            float totaltime = -2 * launchdata.velo * Mathf.Sin(launchdata.angle * Mathf.Deg2Rad) / Physics.gravity.y;                // Total time taken for the projectile to land again
            print("totaltime" + totaltime + "velo " + launchdata.velo + "angle" + launchdata.angle + Mathf.Sin(launchdata.angle));
            int iterations = (int)(totaltime / timegap);                                                            // no. of iterations depending on the 
            print("iterations" + iterations);
            float max_vert_pos = 0;
            float max_vert_vel = 0;
            for (int i = 0; i < 1; i++)
            {
                float[] position = new float[iterations + 1];
                float[] velocity = new float[iterations + 1];
                float velox = launchdata.velo * Mathf.Cos(launchdata.angle * Mathf.Deg2Rad);
                float veloy = launchdata.velo * Mathf.Sin(launchdata.angle * Mathf.Deg2Rad);
                print("veloy" + veloy);
                float time;
                for (int j = 0; j < iterations; j++)
                {
                    time = timegap * j;
                    velocity[j] = veloy + Physics.gravity.y * time;
                    position[j] = veloy * time + 0.5f * Physics.gravity.y * time * time;
                    if (Mathf.Abs(velocity[j]) > max_vert_vel)
                        max_vert_vel = Mathf.Abs(velocity[j]);
                    if (position[j] > max_vert_pos)
                        max_vert_pos = position[j];

                }
                velocity[iterations] = veloy + Physics.gravity.y * totaltime;                                             // Last time value
                position[iterations] = veloy * totaltime + 0.5f * Physics.gravity.y * totaltime * totaltime;              // Last time value
                graph_pos.dataset[i] = new Tuple<float[], float[]>(position, position);
                graph_vel.dataset[i] = new Tuple<float[], float[]>(position, velocity);
            }
            graph_pos.initial_velocity = launchdata.velo;
            graph_vel.initial_velocity = launchdata.velo;
            graph_pos.AdjustVerticalView(max_vert_pos);
            graph_vel.AdjustAbsoluteVerticalView(max_vert_vel);
            graph_pos.Populate(iterations, totaltime);
            graph_vel.Populate(iterations, totaltime);
        }


        // Update is called once per frame
        void Update()
        {

        }
    }
}
