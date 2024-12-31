using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowTimer : MonoBehaviour
{
    public Level_Status_glacial Level_Status_glacial;
    public Timer Timer;
    public TextMeshProUGUI interactText;
    public int sec;
    
    // Start is called before the first frame update
    void Start()
    {
        HideInteractMessage();
    }

    // Update is called once per frame
    void Update()
    {
        if (Timer.currentTime >= sec && Timer.currentTime < sec + 1 && Timer.timerActive)
        {
            ShowInteractMessage();
        }
        else
        {
            HideInteractMessage();
        }
    }
    private void ShowInteractMessage()
    {
        if (interactText != null)
        {
            interactText.gameObject.SetActive(true);
        }
    }

    private void HideInteractMessage()
    {
        if (interactText != null)
        {
            interactText.gameObject.SetActive(false);
        }
    }
}
