using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChangePerson : MonoBehaviour
{   
    private string character;
    public GameObject middle_age_man;
    public GameObject middle_age_woman;
    public GameObject young_age_man;
    public GameObject oooo;
    public GameObject young_age_woman;

    // Start is called before the first frame update
    void Start()
    {
        character = MainMenu.character;

        young_age_man.SetActive(false);

        if (character == "young man")
        {
            young_age_man.SetActive(true);
        }
        else if (character == "middle aged man")
        {
            //young_age_man.SetActive(false);
            middle_age_man.SetActive(true);
        }
        else if (character == "middle aged woman")
        {
            //young_age_man.SetActive(false);
            middle_age_woman.SetActive(true);
        }
        else
        {
            oooo.SetActive(true);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (character == "young woman")
        {
            young_age_man.SetActive(false);
            //young_age_woman.SetActive(true);
        }
        else if (character == "middle aged man")
        {
            young_age_man.SetActive(false);
            //middle_age_man.SetActive(true);
        }
        else if (character == "middle aged woman")
        {
            young_age_man.SetActive(false);
            //middle_age_woman.SetActive(true);
        }
    }
}
