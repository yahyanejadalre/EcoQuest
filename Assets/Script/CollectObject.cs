using UnityEngine;
using TMPro;
using Unity.Collections;

public class RaccoltaOggetto : MonoBehaviour
{
    // Dichiarazione della variabile nell'editor Unity
    public GameObject oggettoDaRaccogliere;
    public float distanzaMassima;
    public TextMeshProUGUI interactText;
    public Level_Status_glacial Level_Status_glacial;
    public int ActualCheck;


    private void Start()
    {
        HideInteractMessage();
    }
    void Update()
    {
        // Trova la distanza tra il giocatore e l'oggetto
        float distanza = Vector3.Distance(transform.position, oggettoDaRaccogliere.transform.position);
        
        // Se il giocatore è abbastanza vicino, disattiva l'oggetto
        if (distanza <= distanzaMassima)
        {
            RaccogliOggetto();
        }
        else
        {
            HideInteractMessage();
        }
    }

    void RaccogliOggetto()
    {
        if (oggettoDaRaccogliere != null && oggettoDaRaccogliere.activeSelf && Level_Status_glacial.NumCheck == ActualCheck)
        {
            ShowInteractMessage();
            if (Input.GetKeyDown(KeyCode.F))
            {
                Level_Status_glacial.NumCheck++;
                Level_Status_glacial.ArrayLight[Level_Status_glacial.NumCheck - 1].SetActive(true);
                HideInteractMessage();
                oggettoDaRaccogliere.SetActive(false);
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