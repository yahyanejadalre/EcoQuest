using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Level_Status_glacial : MonoBehaviour
{
    public int NumCheck = 0;
    public Vector3 Checkpoint;
    private GameObject playerObject;
    public bool updated = false;
    public GameObject[] ArrayLight;
    public GameObject[] TriggersHouse;
    public GameObject[] TriggersScientist;
    public GameObject[] TriggersFinal;
    public GameObject[] Audios;
    public int NumIce = 1;
    public bool timer_enable;
    public bool easy_mode;
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
        TriggersHouse[cluster].SetActive(true);
        TriggersScientist[cluster].SetActive(true);
        TriggersFinal[cluster].SetActive(true);
        Audios[cluster].SetActive(true);
        if (age >= 35)
        {
            easy_mode = true;
            timer_enable = false;
        }
        else
        {
            easy_mode = false;
            timer_enable = true;
        }
        if (playerObject != null)
        {
            // Salva le coordinate iniziali del giocatore come Checkpoint
            Checkpoint = playerObject.transform.position;
        }
    }

    void Update()
    {
    //    improvement = Improvement.transform.position.y;
     //   worsening = Worsening.transform.position.y;
        SaveCheckpoint();
    }

    void SaveCheckpoint()
    {
        if (NumCheck >= 0 && updated == false)
        {
            Checkpoint = GameObject.FindGameObjectWithTag("Player").transform.position;
            updated = true;
        }
    }
}
