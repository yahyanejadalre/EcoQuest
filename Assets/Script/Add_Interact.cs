using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DialogueEditor;


public class Add_Interact : MonoBehaviour
{   
    public GameObject porta1;
    public GameObject porta2;
    public GameObject porta3;
    public GameObject porta4;
    public GameObject porta5;
    public GameObject porta6;
    public GameObject porta7;
    public GameObject personaggio;
    
    public float distanzaMassima;
    public TextMeshProUGUI interactText;
    private int flag = 0;
    // Start is called before the first frame update
    void Start()
    {
        HideInteractMessage();    
    }

    // Update is called once per frame
    void Update()
    {
        float distanza1 = Vector3.Distance(transform.position, porta1.transform.position);
        float distanza2 = Vector3.Distance(transform.position, porta2.transform.position); 
        float distanza3 = Vector3.Distance(transform.position, porta3.transform.position); 
        float distanza4 = Vector3.Distance(transform.position, porta4.transform.position); 
        float distanza5 = Vector3.Distance(transform.position, porta5.transform.position); 
        float distanza6 = Vector3.Distance(transform.position, porta6.transform.position); 
        float distanza7 = Vector3.Distance(transform.position, porta7.transform.position);
        float distanza8 = Vector3.Distance(transform.position, personaggio.transform.position);

        if (distanza1 <= distanzaMassima || distanza2 <= distanzaMassima || distanza3 <= distanzaMassima ||
            distanza4 <= distanzaMassima || distanza5 <= distanzaMassima || distanza6 <= distanzaMassima ||
            distanza7 <= distanzaMassima)
        {
            ShowInteractMessage();
        }
        else if(distanza8 <= distanzaMassima && flag == 0)
        {
            ShowInteractMessage();
        }
        else
        {
            HideInteractMessage();
        }
    }
    
    private void OnEnable()
    {
        ConversationManager.OnConversationStarted += ConversationStart;

    }
    
    private void OnDisable()
    {
        ConversationManager.OnConversationStarted -= ConversationStart;

    }
    
    private void ConversationStart()
    {
        Debug.Log("A conversation has begun.");
        flag = 1;
    }
    
    private void ShowInteractMessage()
    {
        if (interactText != null)
        {
            interactText.gameObject.SetActive(true);
        }
    }

    private void HideInteractMessage()
    {
        if (interactText != null)
        {
            interactText.gameObject.SetActive(false);
        }
    }

}
