using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
using TMPro;

public class RaccoltaRifiuto : MonoBehaviour
{
    public static WasteObject oggettoRaccolto;
    public WasteObject oggettoDaRaccogliere;
    private bool inPossessoDiOggetto = false;
    [SerializeField] private NPCConversation myConversation;
    private GameObject canvasObject;
    private TextMeshProUGUI textComponent;

    private void Start()
    {
        canvasObject = GameObject.Find("Canvas");

        if (canvasObject != null)
        {
            // Cerca il componente TextMeshProUGUI all'interno di "Canvas" usando il nome "Text_2"
            textComponent = canvasObject.transform.Find("Text_2")?.GetComponent<TextMeshProUGUI>();

            if (textComponent != null)
            {
                textComponent.gameObject.SetActive(false);
            }
            else
            {
                Debug.LogError("Text_2 non trovato all'interno di Canvas.");
            }
        }
        else
        {
            Debug.LogError("Canvas non trovato.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Mostra solo Text_2 quando il giocatore si avvicina
            if (textComponent != null)
            {
                textComponent.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Nascondi solo Text_2 quando il giocatore si allontana
            if (textComponent != null)
            {
                textComponent.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
       if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                RaccogliOggetto(oggettoDaRaccogliere);
                ConversationManager.Instance.StartConversation(myConversation);
                if (textComponent != null)
                {
                    textComponent.gameObject.SetActive(false);
                }
                // Disattiva l'oggetto associato allo script
                gameObject.SetActive(false);
            }
        }
    }

    void RaccogliOggetto(WasteObject oggetto)
    {
        if (oggetto != null)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (!inPossessoDiOggetto)
                {
                    oggettoDaRaccogliere = oggettoRaccolto;
                    oggettoRaccolto = oggetto;
                    oggetto.gameObject.SetActive(false);
                    inPossessoDiOggetto = true;
                }
            }
        }
    }
}
