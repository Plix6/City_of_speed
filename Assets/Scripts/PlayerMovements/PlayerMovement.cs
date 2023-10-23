using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //---------------------
    //      VARIABLES
    //---------------------

    // Movement variables
    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public float wallRunSpeed;

    public float groundDrag;
    Vector3 moveDirection;
    private Vector3 velocityToSet;

    // Jumping variables
    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    private bool canJump = true;
    private bool canDoubleJump = false;

    // Crouching variables
    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;

    // Slope Handling variables
    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;

    // Keybind variables
    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;
    public KeyCode backToCheckpoint = KeyCode.R;

    private float horizontalInput;
    private float verticalInput;

    // Ground Check variables
    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation; // Player orientation

    Rigidbody rb; // Player rigidbody

    public MovementState state;
    public enum MovementState
    {
        Walking,
        Sprinting,
        WallRunning,
        Crouching,
        Air
    }

    public bool WallRunning;

    // Game Controller
    public GameController gameController;

    //--------------------------
    //      START FUNCTION
    //--------------------------
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        startYScale = transform.localScale.y;
    }

    //--------------------------
    //      UPDATE FUNCTION
    //--------------------------
    void Update()
    {
        if (gameController.GetGameIsActive())
        {
            // Ground Check
            grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

            MyInput();
            SpeedControl();
            StateHandler();

            if (grounded)
                rb.drag = groundDrag;
            else
                rb.drag = 0f;

            if(isOutOfMap())
            {
                StartCoroutine(Respawn());
            }
        }
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }

    //--------------------
    //      FUNCTIONS
    //--------------------

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // back to checkpoint
        if (Input.GetKeyDown(backToCheckpoint))
        {
            StartCoroutine(Respawn());
        }

        // Jump
        if (Input.GetKey(jumpKey) && canJump && grounded)
        {
            canJump = false;
            canDoubleJump = true;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
            
        }
        // Double Jump
        else if (Input.GetKeyDown(jumpKey) && canDoubleJump && !WallRunning)
        {
            canDoubleJump = false;
            Jump();
        }

        // start crouch
        if (Input.GetKeyDown(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }

        // stop crouch
        if (Input.GetKeyUp(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }
    }

    private bool isOutOfMap()
    {
        // Check if the player is under y = 26
        if (transform.position.y < 26)
        {
            return true;
        }
        return false;
    }

    IEnumerator Respawn()
    {
        rb.isKinematic = true;
        yield return new WaitForSeconds(0.01f);
        transform.position = gameController.GetCheckpoint();
        yield return new WaitForSeconds(0.01f);
        rb.isKinematic = false;
        rb.useGravity = true;
    }

    // State Machine for movement Function
    private void StateHandler()
    {
        // Wallrunning
        if (WallRunning)
        {
            state = MovementState.WallRunning;
            moveSpeed = wallRunSpeed;
        }
        // Crouching
        if (Input.GetKey(crouchKey))
        {
            state = MovementState.Crouching;
            moveSpeed = crouchSpeed;
        }
        // Sprinting
        else if(grounded && Input.GetKey(sprintKey))
        {
            state = MovementState.Sprinting;
            moveSpeed = sprintSpeed;
        }
        // Walking
        else if (grounded)
        {
            state = MovementState.Walking;
            moveSpeed = walkSpeed;
        }
        // Air
        else
        {
            state = MovementState.Air;
        }
    }

    // Movement Function
    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on slope
        if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDirection(moveDirection) * moveSpeed * 20f, ForceMode.Force);

            if(rb.velocity.y > 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }

        // on ground
        else if(grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // in air
        else if(!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);

        // turn gravity off when on slope
        rb.useGravity = !OnSlope();

    }

    // Speed Control Function
    private void SpeedControl()
    {
        // Limit speed on slope
        if (OnSlope() && !exitingSlope)
        {
            if(rb.velocity.magnitude > moveSpeed)
            {
                rb.velocity = rb.velocity.normalized * moveSpeed;
            }
        }

        else
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }

        
    }

    // Jump Function
    private void Jump()
    {
        exitingSlope = true;
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // add jump force
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    // Reset Jump Function
    private void ResetJump()
    {
        canJump = true;

        exitingSlope = false;
    }

    // Slope Handling Functions
    public bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    // Get slope move direction function
    public Vector3 GetSlopeMoveDirection(Vector3 direction)
    {
        return Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized;
    }

    // Calculations for the Grapple Jump
    public void JumpToPosition(Vector3 targetPosition, float trajectoryHeight)
    {

        velocityToSet = CalculateJumpVelocity(transform.position, targetPosition, trajectoryHeight);
        Invoke(nameof(SetVelocity), 0.1f);
    }
    // Calculate speed during Grapple Jump
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

    // Set speed during the Grapple Jump
    private void SetVelocity()
    {
        rb.velocity = velocityToSet;
    }
}

