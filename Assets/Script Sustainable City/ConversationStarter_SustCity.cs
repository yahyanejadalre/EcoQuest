using UnityEngine;
using TMPro;
using DialogueEditor;

public class ConverstaionStarter_SustCity : MonoBehaviour
{
    [SerializeField] private NPCConversation myConversation;
    public TextMeshProUGUI interactText;
    private bool conversationStarted = false;
    private GameObject canvasObject;
    private TextMeshProUGUI textComponent;

    private void Start()
    {
        // Assicura che il testo sia inizialmente nascosto all'avvio
        HideInteractMessage();

        canvasObject = GameObject.Find("Canvas");

        if (canvasObject != null)
        {
            // Cerca il componente TextMeshProUGUI all'interno di "Canvas" usando il nome "Text_2"
            textComponent = canvasObject.transform.Find("Text_3")?.GetComponent<TextMeshProUGUI>();

            if (textComponent != null)
            {
                textComponent.gameObject.SetActive(false);
            }
            else
            {
                Debug.LogError("Text_3 non trovato all'interno di Canvas.");
            }
        }
        else
        {
            Debug.LogError("Canvas non trovato.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !conversationStarted)
        {
            // Quando il giocatore entra, mostra il testo "Press F" sulla Canvas
            ShowInteractMessage();

            if (textComponent != null)
            {
                textComponent.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Quando il giocatore esce, nascondi il testo dalla Canvas
            HideInteractMessage();

            if (textComponent != null)
            {
                textComponent.gameObject.SetActive(false);
            }
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
            if (Input.GetKeyDown(KeyCode.F))
            {
                HideInteractMessage();
                ConversationManager.Instance.StartConversation(myConversation);
                conversationStarted = true;

                if (textComponent != null)
                {
                    textComponent.gameObject.SetActive(false);
                }
            }
        }
    }
}
