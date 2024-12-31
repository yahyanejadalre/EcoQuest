using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_up : MonoBehaviour
{
    public Transform targetObject; // Il riferimento all'oggetto esterno
    public float distanceToMove = 10f; // Distanza verticale da percorrere quando guarda l'oggetto esterno

    private void Update()
    {
        // Ottieni la direzione dal cubo all'oggetto esterno
        Vector3 directionToTarget = targetObject.position - transform.position;

        // Calcola l'angolo tra la direzione del cubo e la direzione verso l'oggetto esterno
        float angle = Vector3.Angle(transform.forward, directionToTarget);

        // Se l'angolo è minore di una certa soglia (es. 45 gradi), muovi il cubo verso l'alto
        if (angle < 1f)
        {
            // Modifica la posizione verticale del cubo
            transform.position += Vector3.up * distanceToMove;
        }
    }
}
