using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaunchCannonUI : MonoBehaviour
{
    public Slider powerSlider;
    Button launch;
    public Cannon cannon;
    public CannonInteraction cannonInteraction;

    // Start is called before the first frame update
    void Start()
    {
        launch = GetComponent<Button>();
        launch.onClick.AddListener(delegate { Launch(); });
        cannonInteraction = Camera.main.gameObject.GetComponent<CannonInteraction>();
    }

    void Launch()
    {
        cannon.ShootProjectile(powerSlider.value);
        cannonInteraction.CannonEnable(false);
    }

}
