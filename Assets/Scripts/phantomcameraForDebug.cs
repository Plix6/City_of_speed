using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private Transform playerTransform;
    [SerializeField] private float speed = 15f;
    [SerializeField] private float rotationX = 0.00f;
    [SerializeField] private float rotationY = 0.00f;

    // Start is called before the first frame update
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        rotationY += Input.GetAxis("Mouse X") * speed;
        rotationX -= Input.GetAxis("Mouse Y") * speed;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        playerTransform.position += (playerTransform.right * Input.GetAxis("Horizontal")
        + playerTransform.forward * Input.GetAxis("Vertical")) * speed * Time.deltaTime;
        transform.eulerAngles = new Vector3(rotationX, rotationY, 0f);

    }
}
