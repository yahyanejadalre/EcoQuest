using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport_TEST : MonoBehaviour
{
    public float interactionDistance = 2f; // Distanza di interazione
    public KeyCode interactKey = KeyCode.F; // Tasto per interagire
    private GameObject PlayerArmature; // Riferimento al GameObject del giocatore
    public bool interact = false;

    private void Start()
    {
        // Assicurati che il giocatore e il Level_status siano presenti nella scena
        PlayerArmature = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (PlayerArmature != null && (Vector3.Distance(PlayerArmature.transform.position, transform.position) <= interactionDistance))
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
