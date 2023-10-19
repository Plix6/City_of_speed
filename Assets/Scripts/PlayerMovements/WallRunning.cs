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
    public float wallJumpSideForce;
    public float wallJumpUpForce;
    public float maxWallRunTime;
    public float maxLastWallCooldown;
    private float wallRunTimer;
    private float lastWallCooldown;

    [Header("Input")]
    public KeyCode jumpKey = KeyCode.Space;
    public float horizontalInput;
    public float verticalInput;

    [Header("Detection")]
    public float wallCheckDistance;
    public float minJumpHeight;
    private RaycastHit leftWallhit;
    private RaycastHit rightWallhit;
    private bool wallLeft;
    private bool wallRight;
    private Object lastWall;

    [Header("Exiting")]
    private bool exitingWall;
    public float exitWallTime;
    private float exitWallTimer;

    [Header("References")]
    public Transform orientation;
    public PlayerCamera camera;
    private PlayerMovement pm;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        pm = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForWall();
        StateMachine();
    }

    private void FixedUpdate()
    {
        if(pm.WallRunning)
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

    // State machine for wallrunning
    private void StateMachine()
    {
        // Getting Inputs
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // State 1 - Wallrunning
        if ((wallLeft || wallRight) && verticalInput > 0 && AboveGround() && !exitingWall)
        {
            if (!pm.WallRunning)
            {
                StartWallRun();
            }

            // Wallrun Timer
            if (wallRunTimer > 0)
            {
                wallRunTimer -= Time.deltaTime;
            }
            if(wallRunTimer <= 0 && pm.WallRunning)
            {
                exitWallTimer = exitWallTime;
                exitingWall = true;
            }

            // Walljump
            if (Input.GetKeyDown(jumpKey) && pm.WallRunning)
            {
                WallJump();
            }
        }

        // State 2 - Exiting
        else if (exitingWall)
        {
            if(pm.WallRunning)
            {
                StopWallRun();
            }
            if(exitWallTimer > 0)
            {
                exitWallTimer -= Time.deltaTime;
            }
            if(exitWallTimer <= 0)
            {
                exitingWall = false;
            }
        }

        // State 3 - Not Wallrunning
        else
        {
            if (pm.WallRunning)
            {
                StopWallRun();
            }
            else
            {
                if (lastWallCooldown > 0)
                {
                    lastWallCooldown -= Time.deltaTime;
                }
                if(lastWallCooldown <= 0)
                {
                    lastWall = null;
                }
            }
        }
    }

    // Start wallrunning function
    private void StartWallRun()
    {
        // Check if the wall is the same as the last wall (to prevent jumping back and forth)
        if (lastWall == (wallRight ? rightWallhit.collider : leftWallhit.collider))
        {
            return;
        }
        else
        {
            // Get the wall id
            lastWall = wallRight ? rightWallhit.collider : leftWallhit.collider;
            pm.WallRunning = true;

            wallRunTimer = maxWallRunTime;

            // Apply camera effects
            camera.DoFov(90f);
            if (wallLeft) camera.DoTilt(-5f);
            if (wallRight) camera.DoTilt(5f);
        }
    }

    // Wallrunning movement function
    private void WallRunningMovement()
    {
        rb.useGravity = false;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        Vector3 wallNormal = wallRight ? rightWallhit.normal : leftWallhit.normal;

        Vector3 wallForward = Vector3.Cross(wallNormal, transform.up);

        if ((orientation.forward - wallForward).magnitude > (orientation.forward - -wallForward).magnitude)
            wallForward = -wallForward;

        // forward movement
        rb.AddForce(wallForward * wallRunForce, ForceMode.Force);

        // push to wall force
        if(!(wallLeft && horizontalInput > 0) || !(wallRight && horizontalInput < 0))
            rb.AddForce(-wallNormal * 100, ForceMode.Force);
    }

    // Stop wallrunning function
    private void StopWallRun()
    {
        pm.WallRunning = false;
        rb.useGravity = true;

        lastWallCooldown = maxLastWallCooldown;

        // Reset camera effects
        camera.DoFov(80f);
        camera.DoTilt(0f);
    }

    // Walljump function
    private void WallJump()
    {
        // enter exiting wall state
        exitingWall = true;
        exitWallTimer = exitWallTime;

        Vector3 wallNormal = wallRight ? rightWallhit.normal : leftWallhit.normal;

        Vector3 forceToApply = transform.up * wallJumpUpForce + wallNormal * wallJumpSideForce;

        // reset y velocity and add force
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(forceToApply, ForceMode.Impulse);
    }
}
