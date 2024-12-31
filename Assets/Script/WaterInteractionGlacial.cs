using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class WaterInteractionGlacial : MonoBehaviour
{
    public Level_Status_glacial Level_Status_glacial; // Riferimento allo script LevelStatus
    private Animator animator; // Riferimento all'Animator del personaggio
    private CinemachineVirtualCamera playerFollowCamera;
    public Timer Timer;
    public float yDifference1;
    void Start()
    {
        Level_Status_glacial = GameObject.Find("Level_status").GetComponent<Level_Status_glacial>(); // Assicurati che il GameObject "Level_status" abbia lo script LevelStatus
        animator = GetComponent<Animator>(); // Ottieni il riferimento all'Animator del personaggio
        playerFollowCamera = GameObject.Find("PlayerFollowCamera").GetComponent<CinemachineVirtualCamera>(); // Trova la CinemachineVirtual Camera
    }

    void Update()
    {
        GameObject water2 = GameObject.Find("Water2"); // Trova l'oggetto "Water2" nella scena
        GameObject water1 = GameObject.Find("Water1");
        GameObject divider = GameObject.Find("Divider");

        if (water2 != null)
        {
             float yDifference2 = transform.position.y - water2.transform.position.y;
            Debug.Log("Y Difference: " + yDifference2);

            if (yDifference2 < -0.1f && transform.position.z < divider.transform.position.z)
            {
                Debug.Log("Teleport Condition Met");
                Timer.ZeroTimer();
                // Disattiva temporaneamente l'animazione del personaggio
                animator.enabled = false;
                DisableAllScripts();

                // Teletrasporta il player al checkpoint
                transform.position = Level_Status_glacial.Checkpoint;

                TeleportCamera();

                // Riattiva l'animazione dopo il teletrasporto (eventualmente dopo un breve ritardo)
                StartCoroutine(EnableAnimatorAfterDelay());
                StartCoroutine(EnableAllScriptsAfterDelay(0.1f));
            }
        }
        if (water1 != null)
        {
            yDifference1 = transform.position.y - water1.transform.position.y;
            Debug.Log("Y Difference: " + yDifference1);

            if (yDifference1 < -0.01f && transform.position.z > divider.transform.position.z)
            {
                Debug.Log("Teleport Condition Met");
                Timer.ZeroTimer();
                // Disattiva temporaneamente l'animazione del personaggio
                animator.enabled = false;
                DisableAllScripts();

                // Teletrasporta il player al checkpoint
                transform.position = Level_Status_glacial.Checkpoint;

                TeleportCamera();

                // Riattiva l'animazione dopo il teletrasporto (eventualmente dopo un breve ritardo)
                StartCoroutine(EnableAnimatorAfterDelay());
                StartCoroutine(EnableAllScriptsAfterDelay(0.1f));
            }
        }

        if (Timer.death)
        {
            Debug.Log("Teleport Condition Met");

            // Disattiva temporaneamente l'animazione del personaggio
            animator.enabled = false;
            DisableAllScripts();

            // Teletrasporta il player al checkpoint
            transform.position = Level_Status_glacial.Checkpoint;

            TeleportCamera();

            // Riattiva l'animazione dopo il teletrasporto (eventualmente dopo un breve ritardo)
            StartCoroutine(EnableAnimatorAfterDelay());
            StartCoroutine(EnableAllScriptsAfterDelay(0.1f));
            Timer.death = false;
        }
    }

    void TeleportCamera()
    {
        if (playerFollowCamera != null)
        {
            playerFollowCamera.transform.position = Level_Status_glacial.Checkpoint;
            // Aggiorna qualsiasi altra impostazione della telecamera che potrebbe essere necessaria
        }
    }

    void DisableAllScripts()
    {
        MonoBehaviour[] scripts = FindObjectsOfType<MonoBehaviour>();

        foreach (var script in scripts)
        {
            if (script != this) // Evita di disattivare lo script corrente
            {
                script.enabled = false;
            }
        }
    }

    // Opzionale: Riattiva l'animazione dopo un breve ritardo
    IEnumerator EnableAnimatorAfterDelay()
     {
         yield return new WaitForSeconds(0.1f); // Tempo di ritardo
         animator.enabled = true;
     }

    IEnumerator EnableAllScriptsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        MonoBehaviour[] scripts = FindObjectsOfType<MonoBehaviour>();

        foreach (var script in scripts)
        {
            if (script != this) // Evita di riattivare lo script corrente
            {
                script.enabled = true;
            }
        }
    }
}
