using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearEasyMode : MonoBehaviour
{
    public GameObject OggettoScompare;
    public float distanzaMassima;
    public Level_Status_glacial Level_Status_glacial;
    
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
            if (distanza <= distanzaMassima && Level_Status_glacial.easy_mode == false)
            {
                OggettoScompare.SetActive(false);
            }
        }
    }
}
