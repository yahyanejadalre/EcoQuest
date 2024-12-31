using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WasteScore : MonoBehaviour
{
    public int NumWasteObject = 0;
    private GameObject playerObject;
    public GameObject[] Audios;
    public int cluster;
    public static float improvement = 0;
    public static float worsening = 0;
    private string character;
    public int age;
    public GameObject middle_age_men;
    public GameObject middle_age_women;
    public GameObject young_men;
    public GameObject young_women;
    private GameObject Improvement;
    private GameObject Worsening;
    public GameObject[] TriggersCitizen;
    public GameObject[] TriggersScientist;

    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player"); // Trova il GameObject del giocatore
        character = MainMenu.character;
        age = MainMenu.age;
        cluster = CLUSTERING_ALL_SCENES.cluster;
        Improvement = GameObject.Find("Improvement");
        Worsening = GameObject.Find("Worsening");
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
        
        TriggersCitizen[cluster].SetActive(true);
        TriggersScientist[cluster].SetActive(true);
        Audios[cluster].SetActive(true);
    }

    void Update()
    {
        UpdateCanvasText();
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
                switch (NumWasteObject - 6)
                {
                    case 1:
                        textComponent.text = "Collected waste: 1/6";
                        break;
                    case 2:
                        textComponent.text = "Collected waste: 2/6";
                        break;
                    case 3:
                        textComponent.text = "Collected waste: 3/6";
                        break;
                    case 4:
                        textComponent.text = "Collected waste: 4/6";
                        break;
                    case 5:
                        textComponent.text = "Collected waste: 5/6";
                        break;
                    case 6:
                        textComponent.text = "Collected waste: 6/6";
                        break;
                    default:
                        textComponent.text = "Collected waste: 0/6";
                        break;
                }
            }
        }
    }
}

