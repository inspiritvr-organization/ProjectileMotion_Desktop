using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectileMotionDesktop
{
    public class PointReleaseMarker : MonoBehaviour
    {
        Animator Angle_anim;
        Animator VecRes_anim;
        bool init = true;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

            float dist = Vector3.Distance(transform.position, Camera.main.transform.position);

        }

        public bool CheckProjectileInfo()
        {
            if (this.transform.childCount > 0)
            {
                init = false;
                Angle_anim = transform.GetChild(0).Find("Angle").GetComponent<Animator>();
                VecRes_anim = transform.GetChild(0).Find("Proj_VecRes").GetComponent<Animator>();
                return true;
            }
            return false;
        }

        public void OnEnter()
        {
            if (CheckProjectileInfo())
            {
                Angle_anim.SetBool("MarkerActive", true);
                VecRes_anim.SetBool("MarkerActive", true);
            }
        }

        public void OnExit()
        {
            if (CheckProjectileInfo())
            {
                Angle_anim.SetBool("MarkerActive", false);
                VecRes_anim.SetBool("MarkerActive", false);
            }
        }

    }
}
