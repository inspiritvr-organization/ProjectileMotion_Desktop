
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class MouseLook : MonoBehaviour
{
    // horizontal rotation speed
    public float horizontalSpeed = 1f;
    // vertical rotation speed
    public float verticalSpeed = 1f;

    private float xRotation = 0.0f;
    private float yRotation = -130f;
    private Camera cam;
    public static bool freeze_view;

    //Variables for Mouse Slider
    public Slider mouseSlider;
    private int MouseSensitivity;
    public Text MouseSensitivityText;
    //Correction Value to change the scale from player prefs to your previous scale
    public float CorrectionValue = 3f;

    private void Awake()
    {
        //Read the value from player prefs..If not present then default value is 50

        MouseSensitivity = PlayerPrefs.GetInt("MouseSensitivity", 50);
        mouseSlider.value = MouseSensitivity;
        ChangeMouseSensitivity();
    }
    void Start()
    {
        freeze_view = false;
    }

    void Update()
    {
        //If view is not Frozen for Camera Rotation
        if (!freeze_view)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            float mouseX = Input.GetAxis("Mouse X") * horizontalSpeed * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * verticalSpeed * Time.deltaTime;

            yRotation += mouseX;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90, 90);

            transform.eulerAngles = new Vector3(xRotation, yRotation, 0.0f);
        }
        else //View Frozen for Interaction with Menu
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    //Called for every time we change the slider
    public void ChangeMouseSensitivity()
    {
        MouseSensitivity = (int)mouseSlider.value;
        horizontalSpeed = MouseSensitivity * CorrectionValue;
        verticalSpeed = MouseSensitivity * CorrectionValue;
        MouseSensitivityText.text = MouseSensitivity + "";
    }

    private void OnApplicationQuit()
    {
        //Save Value in Player Prefs when we quit
        PlayerPrefs.SetInt("MouseSensitivity", MouseSensitivity);
    }
}