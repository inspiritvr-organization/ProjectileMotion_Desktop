using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SensitivitySlider : MonoBehaviour
{
    Slider slider;
    public Text sliderText;

    private void Start()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(delegate { ValueChanged(); });
    }

    void ValueChanged()
    {
        //MouseLook.Mouse = slider.value;
        sliderText.text = ((int)slider.value).ToString();
    }
}
