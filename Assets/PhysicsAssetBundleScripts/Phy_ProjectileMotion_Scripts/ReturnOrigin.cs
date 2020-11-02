using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectileMotionDesktop
{
    public class ReturnOrigin : MonoBehaviour
    {
        [HideInInspector]
        public bool moveOrig = false;
        Vector3 orig_posn;
        Quaternion orig_orientation;
        // Start is called before the first frame update
        void Start()
        {
            orig_posn = transform.position;
            orig_orientation = transform.rotation;
        }

        // Update is called once per frame
        void Update()
        {
            if (moveOrig)
            {
                transform.position = orig_posn;
                transform.rotation = orig_orientation;
            }

            if (moveOrig && Vector3.Distance(transform.position, orig_posn) < 0.01f)
            {
                GetComponent<Collider>().enabled = true;
                moveOrig = false;
                //if(GetComponent<InclineMarkers>())
                //    GetComponent<InclineMarkers>().enabled = true;
                GetComponent<SoundEffectManager>().PlayStrikeSound();
            }
        }


    }
}
