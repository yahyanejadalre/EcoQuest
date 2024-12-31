using UnityEngine;
using TMPro;
using DialogueEditor;

public class ConverstaionStarter : MonoBehaviour
{
    [SerializeField] private NPCConversation myConversation;
    public TextMeshProUGUI interactText;
    private bool conversationStarted = false;
    public Level_Status_glacial Level_Status_glacial;
    public int ActualCheck;

    private void Start()
    {
        HideInteractMessage();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !conversationStarted && Level_Status_glacial.NumCheck == ActualCheck)
        {
            ShowInteractMessage();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HideInteractMessage();
        }
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

    private void OnTriggerStay(Collider other)
    {
        // Gestisci l'avvio della conversazione se il giocatore è nel trigger
        if (other.CompareTag("Player") && !conversationStarted)
        {
            if (Input.GetKeyDown(KeyCode.F) && Level_Status_glacial.NumCheck == ActualCheck)
            {
                Level_Status_glacial.NumCheck++;
                if (Level_Status_glacial.NumCheck - 1 <= 6)
                {
                    Level_Status_glacial.ArrayLight[Level_Status_glacial.NumCheck - 1].SetActive(true);
                }
                HideInteractMessage();
                ConversationManager.Instance.StartConversation(myConversation);
                conversationStarted = true;
            }
        }
    }
}