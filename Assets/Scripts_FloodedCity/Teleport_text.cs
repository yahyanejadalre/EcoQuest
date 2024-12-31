using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Teleport_text : MonoBehaviour
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
    }

    private void Update()
    {
        if (PlayerArmature != null && (Vector3.Distance(PlayerArmature.transform.position, transform.position) <= interactionDistance) && levelStatus.NumBatteriesCollected > 2)
        {
            interact = true;

            if (Input.GetKeyDown(interactKey))
            {
                gameObject.SetActive(false);
                SceneManager.LoadSceneAsync("Glacial_level");
            }
        }
        else
        {
            interact = false;

        }
    }
}
