using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ProjectileMotionDesktop
{
    public class GazeMarkerPosn : MonoBehaviour
    {
        Animator anim;
        float dist;
        // Start is called before the first frame update
        void Start()
        {
            //anim = gameObject.GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            transform.LookAt(Camera.main.transform.position);
            transform.Rotate(Vector3.up, 180);
            //dist = Vector3.Distance(this.transform.position, Camera.main.transform.position);

            //RectTransform rt = gameObject.GetComponent(typeof(RectTransform)) as RectTransform;
            //rt.sizeDelta = new Vector2(rt.sizeDelta.x*dist, rt.sizeDelta.y * dist);
            //rt.localScale *= dist;
            //transform.gameObject.GetComponent<RectTransform>().rect = new Rect(0, 0, rect1.width * dist, rect1.height * dist);
            //gameObject.GetComponent<CanvasScaler>().scaleFactor *= dist;
            //this.transform.localScale *= dist;
        }

        //private void OnMouseEnter()
        //{
        //    anim.SetBool("MarkerActive", true);
        //}

        //private void OnMouseExit()
        //{
        //    anim.SetBool("MarkerActive", false);
        //}

        //public void OnPointerExit(PointerEventData eventData)
        //{
        //    //anim.SetBool("MarkerActive", true);
        //}

        //public void OnPointerEnter(PointerEventData eventData)
        //{
        //    //anim.SetBool("MarkerActive", false);
        //}
    }
}
