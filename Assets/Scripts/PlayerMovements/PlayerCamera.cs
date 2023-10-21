using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerCamera : MonoBehaviour
{
    //---------------------
    //      VARIABLES
    //---------------------

    // Mouse Sensitivity
    public float sensX; 
    public float sensY;

    public bool lockCamera = true; // Lock camera to player

    public Transform orientation; // Player orientation
    public Transform camHolder; // Camera holder

    // Camera rotation
    float xRotation; 
    float yRotation;

    // Game Controller
    public GameController gameController;

    //--------------------------
    //      START FUNCTION
    //--------------------------
    private void Start()
    {
        // Lock and hide cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    //--------------------------
    //      UPDATE FUNCTION
    //--------------------------
    private void Update()
    {
        if (gameController.GetGameIsActive())
        {
            // Collect Mouse Input
            float inputX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
            float inputY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

            yRotation += inputX;
            xRotation -= inputY;

            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            // Rotate camera and orientation          
            camHolder.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
            orientation.rotation = Quaternion.Euler(0f, yRotation, 0f);
            
        }

        // Unlock, show cursor
        /*if (Input.GetButtonDown("Submit"))
        {
            if (!lockCamera)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                lockCamera = true;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                lockCamera = false;
            }
        }*/
    }

    //--------------------
    //      FUNCTIONS
    //--------------------

    // Camera Shake Function
    public void DoFov(float endValue)
    {
        GetComponent<Camera>().DOFieldOfView(endValue, 0.25f);
    }

    // Camera Tilt Function
    public void DoTilt(float zTilt)
    {
        transform.DOLocalRotate(new Vector3(0,0, zTilt), 0.25f);
    }
}
