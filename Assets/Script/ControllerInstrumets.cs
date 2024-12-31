using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInstrumets : MonoBehaviour
{

    public GameObject controller;
    
    // Update is called once per frame
    void Update()
    {
        if (controller.activeSelf == false)
        {
            gameObject.SetActive(false);
        }
    }
}
