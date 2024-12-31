using UnityEngine;
using TMPro;

public class RaccoltaGhiaccio : MonoBehaviour
{
    // Dichiarazione della variabile nell'editor Unity
    public float distanzaMassima;
    public TextMeshProUGUI interactText;
    public GameObject[] ArrayIce;
    private int pressCount = 0;
    public int ActualCheck;
    private int ice = 0;
    public Level_Status_glacial Level_Status_glacial;
    
    private void Start()
    {
        HideInteractMessage();
    }
    
    void Update()
    {
        // Trova la distanza tra il giocatore e l'oggetto

        if (ice < 3)
        {
            float distanza = Vector3.Distance(transform.position, ArrayIce[ice].transform.position);

            // Se il giocatore è abbastanza vicino, disattiva l'oggetto
            if (distanza <= distanzaMassima && ice < 3 && Level_Status_glacial.NumCheck == ActualCheck)
            {
                ShowInteractMessage();
                if (Input.GetKeyDown(KeyCode.F))
                {
                    pressCount++;
                }

                if (pressCount == 2)
                {
                    RaccogliIce();
                }
            }
            else
            {
                HideInteractMessage();
            }
        }
        else
        {
            HideInteractMessage();
        }
    }

    void RaccogliIce()
    {
        if (ArrayIce[ice] != null)
        {
            pressCount = 0;
            ArrayIce[ice].SetActive(false);
            ice++;
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