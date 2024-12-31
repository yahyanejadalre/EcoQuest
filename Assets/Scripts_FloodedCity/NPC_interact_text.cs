using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPC_interact_text : MonoBehaviour
{
    public float interactionDistance = 2f; // Distanza di interazione
    public KeyCode interactKey = KeyCode.F; // Tasto per interagire
    private GameObject PlayerArmature; // Riferimento al GameObject del giocatore
    private GameObject levelStatusObject; // Riferimento al GameObject Level_status
    private levelStatus levelStatus; // Riferimento allo script LevelStatus
    public bool interact = false;

    private void Start()
    {
        // Assicurati che il giocatore e il Level_status siano presenti nella scena
        PlayerArmature = GameObject.FindGameObjectWithTag("Player");
        levelStatusObject = GameObject.Find("Level_status");
        if (levelStatusObject != null)
        {
            levelStatus = levelStatusObject.GetComponent<levelStatus>();
        }
        //playerInteraction = GameObject.Find("Text_3").GetComponent<TextMeshProUGUI>();
        //playerInteraction.enabled = false;
    }

    private void Update()
    {
        if (PlayerArmature != null && (Vector3.Distance(PlayerArmature.transform.position, transform.position) <= interactionDistance))
        {
            interact = true;

            if (Input.GetKeyDown(interactKey))
            {
                //Text_manager_script.NPCInteractionActive();
                if (gameObject.name != "Trigger_Scientist")
                {
                    levelStatus.collect_enable = true;
                }
                gameObject.SetActive(false);
                interact = false;
            }
        }
        else
        {
            interact = false;

        }
    }
}
