using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Security.Cryptography;
using Unity.VisualScripting;
using DialogueEditor;

public class levelStatus : MonoBehaviour
{
    public int NumBatteriesCollected = 0;
    public Vector3 Checkpoint;
    private GameObject playerObject;
    private GameObject Easy_mode;
    private GameObject Improvement;
    private GameObject Worsening;
    public GameObject[] AudioArray;

    public bool updated = false;
    public bool collect_enable = false;

    public bool easy_mode_enabled;
    public int cluster = 3;
    public static float improvement = 0;
    public static float worsening = 0;
    private string character;
    private int age = 0;
    public GameObject middle_age_men;
    public GameObject middle_age_women;
    public GameObject young_men;
    public GameObject young_women;


    void Start()
    {
        character = MainMenu.character;
        age = MainMenu.age;

        playerObject = GameObject.FindGameObjectWithTag("Player"); // Trova il GameObject del giocatore
        Easy_mode = GameObject.Find("Easy_mode");
        Improvement = GameObject.Find("Improvement");
        Worsening = GameObject.Find("Worsening");
        cluster = CLUSTERING_ALL_SCENES.cluster;

        AudioArray[cluster].SetActive(true);

        //character = "young woman";

        if (character == "young man")
        {
            young_men.SetActive(true);
        }
        if (character == "young woman")
        {
            young_men.SetActive(false);
            young_women.SetActive(true);
        }
        if (character == "middle aged man")
        {
            young_men.SetActive(false);
            middle_age_men.SetActive(true);
        }
        if (character == "middle aged woman")
        {
            young_men.SetActive(false);
            middle_age_women.SetActive(true);
        }
        
        Checkpoint = playerObject.transform.position;

        // Makes the game easier if the age is above a threshold
        if(age > 35)
        {
            easy_mode_enabled = true;
        }
        else
        {
            easy_mode_enabled = false;
        }

        if(easy_mode_enabled == true)
        {
            Easy_mode.SetActive(true);
        }
        else
        {
            Easy_mode.SetActive(false);
        }
    }

    void Update()
    {
        UpdateCanvasText();
        SaveCheckpoint();

        improvement = Improvement.transform.position.y;
        worsening = Worsening.transform.position.y;

        if (improvement > 0)
        {
            Debug.Log("Improvement:" + improvement);
        }

        if (worsening > 0)
        {
            Debug.Log("Worsening:" + worsening);
        }

    }

    void UpdateCanvasText()
    {
        // Codice per aggiornare il testo nel Canvas
        // ...

        // Trova il GameObject del Canvas
        GameObject canvasObject = GameObject.Find("Canvas"); // Sostituisci "NomeDelTuoCanvas" con il nome effettivo del tuo GameObject Canvas

        if (canvasObject != null)
        {
            TextMeshProUGUI textComponent = canvasObject.GetComponentInChildren<TextMeshProUGUI>();

            if (textComponent != null)
            {
                switch (NumBatteriesCollected)
                {
                    case 1:
                        textComponent.text = "Collected batteries: 1/3";
                        break;
                    case 2:
                        textComponent.text = "Collected batteries: 2/3";
                        break;
                    case 3:
                        textComponent.text = "Collected batteries: 3/3";
                        break;
                    default:
                        textComponent.text = "Collected batteries: 0/3";
                        break;
                }
            }
        }
    }

    void SaveCheckpoint()
    {
        if (NumBatteriesCollected > 0 && updated == false)
        {
            Checkpoint = GameObject.FindGameObjectWithTag("Player").transform.position;
            updated = true;
        }
    }
}
