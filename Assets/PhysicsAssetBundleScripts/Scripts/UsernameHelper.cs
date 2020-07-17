using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsernameHelper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetString("username", "user2");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
