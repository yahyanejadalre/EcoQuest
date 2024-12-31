using DialogueEditor;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Conv_start : MonoBehaviour
{
    [SerializeField] public NPCConversation myConversation;
    private bool conversationStarted = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        // Gestisci l'avvio della conversazione se il giocatore è nel trigger
        if (other.CompareTag("Player") && !conversationStarted)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                ConversationManager.Instance.StartConversation(myConversation);
                conversationStarted = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
