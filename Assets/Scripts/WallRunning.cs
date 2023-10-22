using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WallRunning : MonoBehaviour
{
    [Header("WallRunning")]
    public LayerMask whatIsWall;
    public LayerMask whatIsGround;
    public float wallRunForce;
    public float maxWallRunTime;
    public float wallRunTimer;
    public bool wallrunning;

    [Header("Input")]
    public float horizontalInput;
    public float verticalInput;

    [Header("Detection")]
    public float wallCheckDistance;
    public float minJumpHeight;
    private RaycastHit leftWallhit;
    private RaycastHit rightWallhit;
    private bool wallLeft;
    private bool wallRight;

    [Header("References")]
    public Transform orientation;
    private PlayerMovement pm;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        pm = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        CheckForWall();
        StateMachine();
    }

    private void FixedUpdate()
    {
        if(wallrunning)
        {
            WallRunningMovement();
        }
    }
    // Check if the player is near a wall
    private void CheckForWall()
    {
        wallRight = Physics.Raycast(transform.position, orientation.right, out rightWallhit, wallCheckDistance, whatIsWall);
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallhit, wallCheckDistance, whatIsWall);
    }

    // Check if the player is above the ground 
    private bool AboveGround()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight, whatIsGround);
    }

    private void StateMachine()
    {
        // Getting Inputs
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // State 1 - Wallrunning
        if ((wallLeft || wallRight) && verticalInput > 0 && AboveGround())
        {
            if (!wallrunning)
            {
                Debug.Log("Start Wallrun");
                StartWallRun();
            }
        }

        // State 2 - Not Wallrunning
        else
        {
            StopWallRun();
        }
    }

    private void StartWallRun()
    {
        wallrunning = true;
    }


    private void WallRunningMovement()
    {
        rb.useGravity = false;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        Vector3 wallNormal = wallRight ? rightWallhit.normal : leftWallhit.normal;

        Vector3 wallForward = Vector3.Cross(wallNormal, transform.up);

        if ((orientation.forward - wallForward).magnitude > (orientation.forward - -wallForward).magnitude)
            wallForward = -wallForward;

    }

    private void StopWallRun()
    {
        wallrunning = false;
        rb.useGravity = true;
    }
}
