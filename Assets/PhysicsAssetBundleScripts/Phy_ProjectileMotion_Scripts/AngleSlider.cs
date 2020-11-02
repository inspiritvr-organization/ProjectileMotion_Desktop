using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace ProjectileMotionDesktop
{
    public class AngleSlider : MonoBehaviour
    {
        Slider angleSlider;
        public TiltCannon tiltCannon;
        public TextMeshProUGUI angleSliderText;

        // Start is called before the first frame update
        void Start()
        {
            angleSlider = GetComponent<Slider>();
            angleSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        }

        // Update is called once per frame
        void ValueChangeCheck()
        {
            angleSlider.value = tiltCannon.SetAngle(angleSlider.value);
            angleSliderText.text = angleSlider.value.ToString() + "\x00B0";
        }


    }
}
