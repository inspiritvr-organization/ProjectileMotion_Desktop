using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour
{
    public float sensitivity = 2f; //sensitivity of player movement

    public CharacterController controller;//reference to character controller

    private float initialHeight; //initial height of the player 

    private void Start()
    {
        initialHeight = transform.position.y;

    }

    private void Update()
    {   //horizontal and vertical input of the player
        float finput = Input.GetAxis("Vertical");
        float sinput = Input.GetAxis("Horizontal");

        //final move vector
        Vector3 move = finput * transform.forward + transform.right * sinput;

        //player moved using character controller
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            controller.Move(move * sensitivity * 2 * Time.deltaTime);
        else
            controller.Move(move * sensitivity * Time.deltaTime);

        //to maintain the height of the player incase he hits any coliider
        maintainHeight(transform.position.y);
    }

    //to maintain the initial height of the player.
    void maintainHeight(float height)
    {
        //resets the transform to original height if the player's height changes
        if (height != initialHeight)
        {
            transform.position = new Vector3(transform.position.x, initialHeight, transform.position.z);
        }
    }
}
