using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasteObject : MonoBehaviour
{
    public int typeWaste; //1: Plastic, 2: Glass, 3: Paper, 4: Organic
    
    // Start is called before the first frame update
    void Start()
    {
        if (typeWaste > 4 | typeWaste < 1)
        {
            Debug.LogError("Initialization error");
            return;
        }
    }
}
