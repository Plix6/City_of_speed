using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    private float rotationX = 0.0f;
    private float rotationY = 0.0f;
    private float moveX = 0.0f;
    private float minValue = -90f;
    private float maxValue = 90f;
    private float mouseSpeed = 100f;
    [SerializeField] public Transform playerBody;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        // Move the camera around with the mouse
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSpeed * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSpeed * Time.deltaTime;

        rotationY += mouseX;
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, minValue, maxValue);

        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
        playerBody.rotation = Quaternion.Euler(0, rotationY, 0);
    }
}
