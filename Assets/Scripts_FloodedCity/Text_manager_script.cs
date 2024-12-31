using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Text_manager_script : MonoBehaviour
{
    private TextMeshProUGUI textInteraction;
    private TextMeshProUGUI playerInteraction;
    private TextMeshProUGUI teleportInteraction;
    private GameObject Trigger_Scientist;
    private GameObject Trigger_NPC_1;
    private GameObject Trigger_NPC_2;
    private GameObject Trigger_NPC_3;
    private GameObject Trigger_teleport;
    private NPC_interact_text interact1;
    private NPC_interact_text interact2;
    private NPC_interact_text interact3;
    private NPC_interact_text interact_s;
    private Teleport_text interact_t;
    private GameObject Battery_1;
    private GameObject Battery_2;
    private GameObject Battery_3;
    private BatteryCollect interactB1;
    private BatteryCollect interactB2;
    private BatteryCollect interactB3;

    private GameObject levelStatusObject; // Riferimento al GameObject Level_status
    private levelStatus levelStatus; // Riferimento allo script LevelStatus

    // Start is called before the first frame update
    void Start()
    {
        GameObject canvasObject = GameObject.Find("Canvas");
        textInteraction = GameObject.Find("Text_2").GetComponent<TextMeshProUGUI>();
        playerInteraction = GameObject.Find("Text_3").GetComponent<TextMeshProUGUI>();
        teleportInteraction = GameObject.Find("Text_4").GetComponent<TextMeshProUGUI>();
        textInteraction.enabled = false;
        playerInteraction.enabled = false;
        Trigger_NPC_1 = GameObject.Find("Trigger_NPC_1");
        Trigger_NPC_2 = GameObject.Find("Trigger_NPC_2");
        Trigger_NPC_3 = GameObject.Find("Trigger_NPC_3");
        Trigger_Scientist = GameObject.Find("Trigger_Scientist");
        Trigger_teleport = GameObject.Find("Trigger_teleport");
        interact1 = Trigger_NPC_1.GetComponent<NPC_interact_text>();
        interact2 = Trigger_NPC_2.GetComponent<NPC_interact_text>();
        interact3 = Trigger_NPC_3.GetComponent<NPC_interact_text>();
        interact_s = Trigger_Scientist.GetComponent<NPC_interact_text>();
        interact_t = Trigger_teleport.GetComponent<Teleport_text>();
        Battery_1 = GameObject.Find("Battery_1");
        Battery_2 = GameObject.Find("Battery_2");
        Battery_3 = GameObject.Find("Battery_3");
        interactB1 = Battery_1.GetComponent<BatteryCollect>();
        interactB2 = Battery_2.GetComponent<BatteryCollect>();
        interactB3 = Battery_3.GetComponent<BatteryCollect>();

        levelStatusObject = GameObject.Find("Level_status");
        levelStatus = levelStatusObject.GetComponent<levelStatus>();
    }


    // Update is called once per frame
    void Update()
    {
        if(interact1.interact == true || interact2.interact == true || interact_s.interact == true || interact3.interact == true)
        {
            NPCInteractionActive();
        }
        else
        {
            NPCInteractionOff();
        }

        if (interactB1.interact == true || interactB2.interact == true || interactB3.interact == true)
        {
            InteractionTextActive();
        }
        else
        {
            InteractionTextOff();
        }

        if (interact_t.interact == true && levelStatus.NumBatteriesCollected > 2)
        {
            teleportInteraction.enabled = true;
        }
        else
        {
            teleportInteraction.enabled = false;
        }
    }

    public void NPCInteractionActive()
    {
        playerInteraction.enabled = true;
    }
    public void NPCInteractionOff()
    {
        playerInteraction.enabled = false;
    }
    public void InteractionTextActive()
    {
        textInteraction.enabled = true;
    }
    public void InteractionTextOff()
    {
        textInteraction.enabled = false;
    }
}
