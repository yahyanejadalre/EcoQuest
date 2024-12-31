using StarterAssets;
using UnityEngine;
using TMPro;
using Cinemachine;

public class GetOutCar : MonoBehaviour
{
    public GameObject oggettoDaRaccogliere;
    public GameObject carSecond;
    public float distanzaMassima;
    public TextMeshProUGUI interactText;
    public TextMeshProUGUI interactText2;
    private Transform figlio2car;
    private Transform figlio2people;
    public Timer Timer;
    public int ActualCheck;
    public Level_Status_glacial Level_Status_glacial;
    public ThirdPersonController ThirdPersonController;
    public CinemachineVirtualCamera CinemachineVirtualCamera;
    
    private void Start()
    {
        // Assicura che il testo sia inizialmente nascosto all'avvio
        HideInteractMessage();
        carSecond.SetActive(false);
        Transform figlio1 = transform.Find("Geometry");
        if (figlio1 != null)
        {
            figlio2car = figlio1.Find("SM_Veh_Pickup_01");
            figlio2people = figlio1.Find("Character_BusinessMan_Shirt");
        }
    }
    
    void Update()
    {
        // Trova la distanza tra il giocatore e l'oggetto
        float distanza = Vector3.Distance(transform.position, oggettoDaRaccogliere.transform.position);
        
        // Se il giocatore è abbastanza vicino, disattiva l'oggetto
        if (distanza <= distanzaMassima && figlio2car.gameObject.activeSelf && Level_Status_glacial.NumCheck == ActualCheck)
        {
            ShowInteractMessage();
            GetOut();
        }
        else
        {
            HideInteractMessage();
        }
    }
    
    
    void GetOut()
    {
        if (oggettoDaRaccogliere != null)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (Level_Status_glacial.timer_enable && Timer.timerActive)
                {
                    Timer.StopTimer();
                    HideInteractMessage2();
                }
                Level_Status_glacial.NumCheck++;
                Level_Status_glacial.ArrayLight[Level_Status_glacial.NumCheck - 1].SetActive(true);
                HideInteractMessage();
                oggettoDaRaccogliere.SetActive(false);
                DisactiveCar();
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
    private void HideInteractMessage2()
    {
        if (interactText2 != null)
        {
            interactText2.gameObject.SetActive(false);
        }
    }
    public void DisactiveCar()
    {
        if (figlio2car != null)
        {
            figlio2car.gameObject.SetActive(false);
            figlio2people.gameObject.SetActive(true);
            carSecond.SetActive(true);
            ThirdPersonController.MoveSpeed = 2.0f;
            ThirdPersonController.SprintSpeed = 5.335f;
            ThirdPersonController.RotationSmoothTime = 0.12f;
            ThirdPersonController.JumpHeight = 1.2f;
            CinemachineVirtualCamera.m_Lens.FieldOfView = 40.0f;
            ThirdPersonController.FootstepAudioVolume = 0.5f;
        }
    }
}