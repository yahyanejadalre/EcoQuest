using TMPro;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class ComputerManager : MonoBehaviour
{
    public GameObject pannelloConTesto;
    public GameObject OggettoComputer;
    public Level_Status_glacial Level_Status_glacial;
    public int distanzaMassima;
    public TextMeshProUGUI interactText;

    void Start()
    {
        pannelloConTesto.SetActive(false);
        HideInteractMessage();
    }

    void Update()
    {
        // Qui inserisci la logica per verificare quando avvicini l'oggetto desiderato
        float distanza = Vector3.Distance(transform.position, OggettoComputer.transform.position);
        
        if (distanza <= distanzaMassima && Level_Status_glacial.NumCheck == 7)
        {
            ShowInteractMessage();
            if(Input.GetKeyDown(KeyCode.F))
            {
                Level_Status_glacial.NumCheck++;
                HideInteractMessage();
                pannelloConTesto.SetActive(true);
            }
            
        }
        else if (distanza > distanzaMassima)
        {
            pannelloConTesto.SetActive(false);
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
