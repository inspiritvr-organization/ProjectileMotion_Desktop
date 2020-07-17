using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class GrabObject : MonoBehaviour
{
    
    GameObject objectinHand;
    Transform grabobj_parent;
    float power = 5f;
    public TextMeshProUGUI velocityText;
    public TextMeshProUGUI angleText;
    public GameObject HoverText;                            // Shows object name when hovered
    LayerMask layerMask;
    public AudioClip grab_sound;
    AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        layerMask = LayerMask.GetMask("Projectile");
        objectinHand = null;
        audio = GetComponent<AudioSource>();
    }

   

    // Update is called once per frame
    void Update()
    {


        //if(!objectinHand && Input.GetMouseButtonUp(0))
            Raycast();

        if (objectinHand)
        {
            //float angle = Vector3.Angle(Camera.main.transform.forward, new Vector3(Camera.main.transform.forward.x, 0f, Camera.main.transform.forward.z));
            float angle = Vector3.SignedAngle(Camera.main.transform.forward, new Vector3(Camera.main.transform.forward.x, 0f, Camera.main.transform.forward.z), Camera.main.transform.right);
            angleText.text = angle.ToString("f") + "\u00B0";
            if(Input.GetMouseButtonUp(0))
            {
                objectinHand.transform.parent = grabobj_parent;
                objectinHand.GetComponent<Rigidbody>().isKinematic = false;
                StartCoroutine(objectinHand.GetComponent<Launch>().ObjectReset());
                objectinHand = null;
                velocityText.gameObject.SetActive(false);
                angleText.gameObject.SetActive(false);
            }
            if (Input.GetKey(KeyCode.Space))
            {
                if (power > 15f)
                    power = 5f; 
                power += Time.deltaTime*2f;
            }
                velocityText.text = power.ToString("f") + " m/s";
            if (Input.GetKeyUp(KeyCode.Space))
            {
                Throw();
            }
        }
    }

    void Raycast()
    {
        if (!objectinHand)
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 3f, layerMask))
            {
                HoverText.SetActive(true);
                HoverText.GetComponentInChildren<TextMeshProUGUI>().text = hit.transform.name;
                //if(Vector3.Distance(transform.position, hit.point) < 2f)
                if (Input.GetMouseButtonUp(0))
                {
                    Grab(hit.transform);
                }
            }
            else HoverText.SetActive(false);
        }
    }

    void Grab(Transform obj)
    {
        power = 5f;
        grabobj_parent = obj.parent;
        obj.SetParent(transform);
        obj.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        obj.position = transform.position + Camera.main.transform.forward * 1.2f;           // keep object in front of you when grabbed
        velocityText.gameObject.SetActive(true);
        angleText.gameObject.SetActive(true);
        audio.clip = grab_sound;
        audio.Play();
        StartCoroutine(AssignObjinHand(obj));
    }

    void Throw()
    {
        objectinHand.transform.parent = grabobj_parent;
        objectinHand.GetComponent<Rigidbody>().isKinematic = false;
        Vector3 throwVelocity = Camera.main.transform.forward * power;
        objectinHand.GetComponent<Rigidbody>().velocity = throwVelocity;
        //objectinHand.transform.Find("Trail").gameObject.GetComponent<TrailRenderer>().enabled = true;
        objectinHand.GetComponent<Launch>().LaunchProjectile(throwVelocity);
        //power = 0f;
        objectinHand = null;
        velocityText.gameObject.SetActive(false);                           
        angleText.gameObject.SetActive(false);
    }

    IEnumerator AssignObjinHand(Transform obj)
    {
        yield return new WaitForSeconds(0.1f);
        objectinHand = obj.gameObject;
    }

}
