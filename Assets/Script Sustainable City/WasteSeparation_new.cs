using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WasteSeparation_new : MonoBehaviour
{
    WasteObject oggettoRaccolto;
    public int tipoCassonetto;
    private GameObject wasteScoreObject;
    private WasteScore wasteScore;
    private GameObject canvasObject;
    private TextMeshProUGUI textComponent;

    void Awake()
    {
        wasteScoreObject = GameObject.Find("WasteScore");
        if (wasteScoreObject != null)
        {
            wasteScore = wasteScoreObject.GetComponent<WasteScore>();
        }

        canvasObject = GameObject.Find("Canvas");

        if (canvasObject != null)
        {
            // Cerca il componente TextMeshProUGUI all'interno di "Canvas" usando il nome "Text_2"
            textComponent = canvasObject.transform.Find("Text_4")?.GetComponent<TextMeshProUGUI>();

            if (textComponent != null)
            {
                textComponent.gameObject.SetActive(false);
            }
            else
            {
                Debug.LogError("Text_4 non trovato all'interno di Canvas.");
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
            if (Input.GetKeyDown(KeyCode.G))
            {
                // Verifica se RaccoltaRifiuto.oggettoRaccolto non è null prima di assegnare il suo valore a oggettoRaccolto
                if (RaccoltaRifiuto.oggettoRaccolto != null)
                {
                    oggettoRaccolto = RaccoltaRifiuto.oggettoRaccolto;

                    if (oggettoRaccolto.typeWaste == tipoCassonetto)
                    {
                        Destroy(oggettoRaccolto.gameObject);
                        Debug.Log("è stato buttato un rifiuto di tipo: " + oggettoRaccolto.typeWaste);
                        wasteScore.NumWasteObject++;
                        Debug.Log("NumWasteObject after increment: " + wasteScore.NumWasteObject);
                        if (textComponent != null)
                        {
                            textComponent.gameObject.SetActive(false);
                        }
                    }
                }
            }
        }
    }
}