using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float countdownTime = 5.0f; // Imposta il tempo di countdown desiderato in secondi
    public float currentTime = 0.0f;
    public bool timerActive = false;
    public bool death;
    public Level_Status_glacial Level_Status_glacial;

    void Start()
    { 
        death = false;
    }

    void Update()
    {
        if (timerActive)
        {
            currentTime += Time.deltaTime;

            // Aggiorna la UI o esegui azioni quando il tempo è scaduto
            if (currentTime >= countdownTime)
            {
                TimerEnded();
            }
        }
    }

    public void StartTimer()
    {
        // Avvia il timer
        currentTime = 0.0f;
        timerActive = true;
    }

    public void ZeroTimer()
    {
        // Riattiva il timer
        currentTime = 0.0f;
    }
    public void StopTimer()
    {
        // Stop il timer
        timerActive = false;
    }

    void TimerEnded()
    {
        // Azioni da eseguire quando il tempo è scaduto
        Debug.Log("Tempo scaduto!");
        death = true;
        ZeroTimer();
        // Puoi aggiungere altre azioni qui, come cambiare scena o attivare/disattivare oggetti
    }
}
