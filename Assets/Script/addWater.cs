using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addWater : MonoBehaviour
{
    public Transform targetObject; 
    public GameObject water;

    private void Update()
    {
        // Ottieni la direzione dal cubo all'oggetto esterno
        Vector3 directionToTarget = targetObject.position - transform.position;

        // Calcola l'angolo tra la direzione del cubo e la direzione verso l'oggetto esterno
        float angle = Vector3.Angle(transform.forward, directionToTarget);

        // Se l'angolo è minore di una certa soglia (es. 45 gradi), muovi il cubo verso l'alto
        if (angle < 1f)
        {
            water.SetActive(true);

        }
    }
}
