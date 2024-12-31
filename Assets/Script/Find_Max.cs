using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Find_Max : MonoBehaviour
{
    public Transform increment_A;
    public Transform increment_B;
    public Transform increment_C;

    void TrovaCuboPiuAlto()
    {
        Transform[] cubi = new Transform[] { increment_A, increment_B, increment_C };

        float maxY = float.MinValue;
        int indexCuboPiuAlto = -1;

        for (int i = 0; i < 3; i++)
        {
            float y = cubi[i].position.y;
            if (y > maxY)
            {
                maxY = y;
                indexCuboPiuAlto = i;
                CLUSTERING_ALL_SCENES.cluster = i;
            }
        }

        if (indexCuboPiuAlto != -1)
        {
            Debug.Log("Il cubo più in alto è il cubo " + cubi[indexCuboPiuAlto].name);
          
            
        }
        else
        {
            Debug.Log("Nessun cubo trovato");
        }
    }

    void Update()
    {
        TrovaCuboPiuAlto();
    }
}


