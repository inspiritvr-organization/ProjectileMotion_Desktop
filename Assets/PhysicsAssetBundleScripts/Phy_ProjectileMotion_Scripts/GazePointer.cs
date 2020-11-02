using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectileMotionDesktop
{
    public class GazePointer : MonoBehaviour
    {
        RaycastHit hit;
        public LayerMask layerMask;
        GameObject objectGazed;
        Ray ray;
        // Start is called before the first frame update
        void Start()
        {
            //layerMask = 1 << 12;

        }

        // Update is called once per frame
        void Update()
        {
            ray = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(ray, out hit, 1000f, layerMask))
            {
                objectGazed = hit.collider.transform.gameObject;
                print(hit.collider.transform.name);
                GazePointerEnter();
            }
            else if (objectGazed)
            {
                GazePointerExit();
                objectGazed = null;
            }
        }

        void GazePointerEnter()
        {
            Animator anim = objectGazed.GetComponent<Animator>();
            if (anim)
                anim.SetBool("MarkerActive", true);
            else if (objectGazed.tag == "Projectile" && objectGazed.GetComponent<PointReleaseMarker>() != null)   // Activates GazeMarker Animation for projectile's release point
            {
                objectGazed.GetComponent<PointReleaseMarker>().OnEnter();
            }
            if (objectGazed.tag == "Incline" && objectGazed.GetComponent<VectorResolution>() && objectGazed.GetComponent<VectorResolution>().vecResEnabled)       // Activates Vector Resolution for an arrow marker if it has the VectorResolution Script
            {
                objectGazed.GetComponent<VectorResolution>().vecResActive = true;
            }
        }

        void GazePointerExit()
        {
            Animator anim = objectGazed.GetComponent<Animator>();
            if (anim)
                anim.SetBool("MarkerActive", false);
            else if (objectGazed.tag == "Projectile" && objectGazed.GetComponent<PointReleaseMarker>() != null)     // Deactivates GazeMarker Animation for projectile's release point
            {
                objectGazed.GetComponent<PointReleaseMarker>().OnExit();
            }
            if (objectGazed.tag == "Incline" && objectGazed.GetComponent<VectorResolution>() && objectGazed.GetComponent<VectorResolution>().vecResEnabled)       // Deactivates Vector Resolution for an arrow marker if it has the VectorResolution Script
            {
                objectGazed.GetComponent<VectorResolution>().vecResActive = false;
            }

        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(ray);
        }

    }
}
