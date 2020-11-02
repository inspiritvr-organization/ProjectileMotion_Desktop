using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurfaceChange : MonoBehaviour
{
    public Material[] mat;
    public PhysicMaterial[] physMat;
    public int index = 1;
    public Text text;
    public GameObject plane;
    public Texture texture;
    public Animator anim;
    bool first = true;

    // Start is called before the first frame update
    void Start()
    {
        DisplayText(ref index);
    }

    // Update is called once per frame
    void Update()
    {
        //plane = transform.parent.gameObject;
    }

    void DisplayText(ref int index)
    {
        plane.GetComponent<Collider>().material = physMat[index];
        plane.GetComponent<MeshRenderer>().material = mat[index];
        if (index == 0)
            text.text = "RUBBER \n(\u03BC = 0.8)";
        else if (index == 1)
            text.text = "WOOD \n(\u03BC = 0.25)";
        else if (index == 2)
            text.text = "METAL \n(\u03BC = 0.2)";
        else if (index == 3)
            text.text = "ICE \n(\u03BC = 0.1)";
        else{
            text.text = "FRICTIONLESS \n(\u03BC = 0)";
            index = -1;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if(other.gameObject.tag == "GameController")// && OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))
        if(other.gameObject.layer == 11)// && OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))
        {
            if (first) {
                anim.SetTrigger("FadeIn");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //if (other.gameObject.tag == "GameController")// && OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))
        if (other.gameObject.layer == 11)// && OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))
        {

            print("otehrENter");
            index++;
            DisplayText(ref index);
            if (GetComponent<HighlightPlus.HighlightEffect>().highlighted)
                GetComponent<MeshRenderer>().enabled = false;

            if (first)
            {
                anim.SetTrigger("FadeOut");
                first = false;
            }
        }
    }
}
