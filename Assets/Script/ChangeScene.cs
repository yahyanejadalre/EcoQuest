using UnityEngine;
using DialogueEditor;
using UnityEngine.SceneManagement;


public class ChangeScene : MonoBehaviour
{
    private void OnEnable()
    {
        ConversationManager.OnConversationStarted += ConversationStart;
        ConversationManager.OnConversationEnded += ConversationEnd;
    }

    private void OnDisable()
    {
        ConversationManager.OnConversationStarted -= ConversationStart;
        ConversationManager.OnConversationEnded -= ConversationEnd;
    }

    private void ConversationStart()
    {
        Debug.Log("A conversation has begun.");
    }

    private void ConversationEnd()
    {
        Debug.Log("A conversation has ended.");
        SceneManager.LoadSceneAsync("Flooded_new");
    }
}