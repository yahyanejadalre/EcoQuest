using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoorController : MonoBehaviour
{
    public float distanzaRilevamento; // Distanza a cui si attiverà l'apertura
    public TextMeshProUGUI interactText;
    //public string tagPersonaggioPrincipale = "Player";
    private bool isDoorOpen = false;
        
    private void Start()
    {
        // Assicura che il testo sia inizialmente nascosto all'avvio
        HideInteractMessage();
    }

    void Update()
    {
        if (IsPlayerNear())
        {
            //ShowInteractMessage();
            if (Input.GetKeyDown(KeyCode.F))
            {
                // Inverte lo stato della porta (aperta o chiusa)
                isDoorOpen = !isDoorOpen;

                // Ruota la porta in base allo stato
                float angoloRotazione = isDoorOpen ? 90f : -90f;
                transform.Rotate(Vector3.up * angoloRotazione);
            
            }
        }
        else
        {
                
        }

    }

    bool IsPlayerNear()
    {
        // Trova il personaggio nella scena (assumendo che ci sia un solo personaggio)
        GameObject personaggio = GameObject.FindGameObjectWithTag("Player");

        // Se non ci sono personaggi, restituisci false
        if (personaggio == null)
        {
            return false;
        }

        // Calcola la distanza tra il personaggio e la porta
        float distanza = Vector3.Distance(personaggio.transform.position, transform.position);

        // Restituisci true se il personaggio è abbastanza vicino
        return distanza < distanzaRilevamento;
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
