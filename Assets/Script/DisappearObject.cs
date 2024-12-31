using System;
using UnityEngine;
using UnityEngine.UIElements;

public class ScompareOggetto : MonoBehaviour
{
    // Dichiarazione della variabile nell'editor Unity
    public GameObject OggettoScompare;
    public float distanzaMassima;
    public Level_Status_glacial Level_Status_glacial;
    
    
    int currentIndex;
    private void Start()
    {
        Level_Status_glacial = GameObject.Find("Level_status").GetComponent<Level_Status_glacial>();
    }

    void Update()
    {
        if (OggettoScompare != null)
        {
            // Trova la distanza tra il giocatore e l'oggetto
            float distanza = Vector3.Distance(transform.position, OggettoScompare.transform.position);
            
            // Se il giocatore è abbastanza vicino, disattiva l'oggetto
            if (distanza <= distanzaMassima)
            {
                Level_Status_glacial.updated = false;
                OggettoScompare.SetActive(false);
            }
        }
    }
}