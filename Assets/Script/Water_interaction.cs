using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class NewBehaviourScWript : MonoBehaviour
{
    public levelStatus levelStatus; // Riferimento allo script LevelStatus
    private Animator animator; // Riferimento all'Animator del personaggio
    private CinemachineVirtualCamera playerFollowCamera;
    private Text_manager_script Text_manager_script;

    void Start()
    {
        levelStatus = GameObject.Find("Level_status").GetComponent<levelStatus>(); // Assicurati che il GameObject "Level_status" abbia lo script LevelStatus
        Text_manager_script = GameObject.Find("Level_status").GetComponent<Text_manager_script>();
        animator = GetComponent<Animator>(); // Ottieni il riferimento all'Animator del personaggio
        playerFollowCamera = GameObject.Find("PlayerFollowCamera").GetComponent<CinemachineVirtualCamera>(); // Trova la CinemachineVirtual Camera
    }

    void Update()
    {
        GameObject ocean = GameObject.Find("Ocean"); // Trova l'oggetto "Ocean" nella scena

        if (ocean != null)
        {
            float yDifference = transform.position.y - ocean.transform.position.y;

            Debug.Log("Y Difference: " + yDifference);

            if (yDifference < -0.1f)
            {
                Debug.Log("Teleport Condition Met");

                // Disattiva temporaneamente l'animazione del personaggio
                animator.enabled = false;
                DisableAllScripts();

                // Teletrasporta il player al checkpoint
                transform.position = levelStatus.Checkpoint;

                TeleportCamera();

                Text_manager_script.NPCInteractionOff();
                Text_manager_script.InteractionTextOff();

                // Riattiva l'animazione dopo il teletrasporto (eventualmente dopo un breve ritardo)
                StartCoroutine(EnableAnimatorAfterDelay());
                StartCoroutine(EnableAllScriptsAfterDelay(0.1f));
            }
        }
    }

    void TeleportCamera()
    {
        if (playerFollowCamera != null)
        {
            playerFollowCamera.transform.position = levelStatus.Checkpoint;
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
