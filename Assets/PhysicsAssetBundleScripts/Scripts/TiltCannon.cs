using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TiltCannon : MonoBehaviour
{
    public bool isCannon;                                   // iscannon true for cannon, false for screen
    Vector3 hand;
    Vector3 offset;
    public GameObject cannon;                               // remains stable
    public GameObject pivot;                                // point around which the rotation happens
    public GameObject cannonBody;                           // part that moves
    public Transform handlepoint;                           // point which keeps track of rotation along the pivot, different from the knob/lever
    public float angle;
    public bool enableTilt;
    public GameObject screen;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 pivottoknob = -handlepoint.position + pivot.transform.position;
        angle = Vector3.Angle(-cannon.transform.forward, pivottoknob.normalized);
        print(angle);
        if (isCannon)
            screen.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = "Angle " + angle.ToString("f") + "\x00B0";                             //Displays default angle value on the screen
        //transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "30" + "\x00B0";
    }

    private void Update()
    {
            
    }

    private void RotateBody(float angle)
    {
        cannonBody.transform.rotation = Quaternion.AngleAxis(angle, cannon.transform.right);
    }

    public float SetAngle(float angle)
    {
        if (angle < 22.5)
        {
            RotateBody(15f);
            screen.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = "Angle = 15" + "\x00B0";
            return 15f;
        }
        else if (angle > 22.5f && angle < 37.5f)
        {
            RotateBody(30f);
            screen.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = "Angle = 30" + "\x00B0";
            return 30f;
        }
        else if (angle > 37.5f && angle < 52.5f)
        {
            RotateBody(45f);
            screen.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = "Angle = 45" + "\x00B0";
            return 45f;
        }
        else if (angle > 52.5f)
        {
            RotateBody(60f);
            screen.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = "Angle = 60" + "\x00B0";
            return 60f;
        }
        else return 15f;
    }

}
