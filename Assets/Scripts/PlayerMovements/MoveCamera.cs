using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    //---------------------
    //      VARIABLES
    //---------------------

    public Transform cameraPosition; // Camera position

    //--------------------------
    //      UPDATE FUNCTION
    //--------------------------
    void Update()
    {
        transform.position = cameraPosition.position;    
    }
}
