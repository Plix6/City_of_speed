using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerCamera : MonoBehaviour
{
    // Variables
    public float sensX;
    public float sensY;

    public bool lockCamera = true;

    public Transform orientation;
    public Transform camHolder;

    float xRotation;
    float yRotation;
    
    // Start is called before the first frame update
    private void Start()
    {
        // Lock and hide cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    // Update is called once per frame
    private void Update()
    {   
        // Collect Mouse Input
        float inputX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float inputY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += inputX;
        xRotation -= inputY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Rotate camera and orientation
        if (!lockCamera)
        {
            camHolder.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
            orientation.rotation = Quaternion.Euler(0f, yRotation, 0f);
        }

        // Unlock, show cursor
        if (Input.GetButtonDown("Submit"))
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
        }
    }
    
    public void DoFov(float endValue)
    {
        GetComponent<Camera>().DOFieldOfView(endValue, 0.25f);
    }

    public void DoTilt(float zTilt)
    {
        transform.DOLocalRotate(new Vector3(0,0, zTilt), 0.25f);
    }
}
