using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectileMotionDesktop
{
    public class Cannon : MonoBehaviour
    {
        public Rigidbody cannonBallRB;
        public float power;
        AudioSource audio;
        public AudioClip launchClip;

        // Start is called before the first frame update
        void Start()
        {
            audio = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
            //if (Input.GetKeyDown(KeyCode.Backspace) && cannonBallRB.gameObject.GetComponent<Launch>().launched == false)
            //{
            //    ShootProjectile(power);

            //}
        }

        public void ShootProjectile(float power)
        {
            cannonBallRB.isKinematic = false;
            //cannonBallRB.AddForce(-transform.GetChild(0).transform.forward * power, ForceMode.Impulse);
            //cannonBallRB.velocity = (-transform.GetChild(0).transform.forward * power);
            cannonBallRB.transform.SetParent(null);
            cannonBallRB.gameObject.GetComponent<Launch>().LaunchProjectile(-transform.GetChild(0).transform.forward * power);
            audio.clip = launchClip;
            audio.Play();
        }

    }
}
