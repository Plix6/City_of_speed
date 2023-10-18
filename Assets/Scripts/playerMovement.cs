using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform orientation;
    public bool freezeMovement;
    public bool activeGrapple;
    private bool enableMovement;

    private float horizontalMovement;
    private float verticalMovement;

    private Vector3 moveDirection;
    private Vector3 velocityToSet;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void FixedUpdate()
    {
        if (freezeMovement)
        {
            rb.velocity = Vector3.zero;
        }
        MovePlayer();

        
    }

    private void MovePlayer()
    {
        if (activeGrapple) {
            return;
        }
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");
        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;
        rb.AddForce(moveDirection.normalized * moveSpeed, ForceMode.Force);
    }

    public void JumpToPosition(Vector3 targetPosition, float trajectoryHeight)
    {
        activeGrapple = true;
        velocityToSet = CalculateJumpVelocity(transform.position, targetPosition, trajectoryHeight);
        Invoke(nameof(SetVelocity), 0.1f);
    }

    private void SetVelocity() { 
        enableMovement = true;
        rb.velocity = velocityToSet;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (enableMovement)
        {
            activeGrapple = false;
            enableMovement = false;

            GetComponent<Grappling>().StopGrapple();
        }
    }
    public Vector3 CalculateJumpVelocity(Vector3 startPoint, Vector3 endPoint, float trajectoryHeight)
    {
        float gravity = Physics.gravity.y;
        float displacementY = endPoint.y - startPoint.y;
        Vector3 displacementXZ = new Vector3(endPoint.x - startPoint.x, 0f, endPoint.z - startPoint.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * trajectoryHeight);
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * trajectoryHeight / gravity)
            + Mathf.Sqrt(2 * (displacementY - trajectoryHeight) / gravity));

        return velocityXZ + velocityY;
    }

}
