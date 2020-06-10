using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerSlider : MonoBehaviour
{
    Slider powerSlider;
    public TextMeshProUGUI powerScreenText;
    public TextMeshProUGUI powerSliderText;

    // Start is called before the first frame update
    void Start()
    {
        powerSlider = GetComponent<Slider>();
        powerSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        
    }

    void ValueChangeCheck()
    {
        powerSlider.value = Mathf.CeilToInt(powerSlider.value);
        powerScreenText.text = "Velocity = " + powerSlider.value.ToString() + " m/s";
        powerSliderText.text = powerSlider.value.ToString() + " m/s";
    }
}
