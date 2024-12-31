using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Teletrasport : MonoBehaviour
{
    // Dichiarazione della variabile nell'editor Unity
    public GameObject Scientist;
    public float distanzaMassima;
    public Level_Status_glacial Level_Status_glacial;
    public int ActualCheck;

    
    void Update()
    {
        // Trova la distanza tra il giocatore e l'oggetto
        float distanza = Vector3.Distance(transform.position, Scientist.transform.position);
        
        // Se il giocatore è abbastanza vicino, disattiva l'oggetto
        if (distanza <= distanzaMassima)
        {
            EndScene();
        }
    }

    void EndScene()
    {
        if (Scientist != null && Scientist.activeSelf && Level_Status_glacial.NumCheck == ActualCheck)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                SceneManager.LoadSceneAsync("SustainableCityNew");
            }
        }
    }
}
