using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Teleport_Sust : MonoBehaviour
{
    public GameObject Scientist;
    public float distanzaMassima;
    public TextMeshProUGUI interactText;
    public WasteScore WasteScore;

    void Start()
    {
        HideInteractMessage();
    }

    // Update is called once per frame
    void Update()
    {
        float distanza = Vector3.Distance(transform.position, Scientist.transform.position);
        
        if (distanza <= distanzaMassima)
        {
            EndScene();
        }
        else
        {
            HideInteractMessage();
        }
    }

    void EndScene()
    {
        if (Scientist != null && Scientist.activeSelf && WasteScore.NumWasteObject == 12)
        {
            ShowInteractMessage();
            if (Input.GetKeyDown(KeyCode.F))
            {
                SceneManager.LoadSceneAsync("Final scene");
            }
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
