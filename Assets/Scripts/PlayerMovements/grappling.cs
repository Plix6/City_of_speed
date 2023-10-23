using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappling : MonoBehaviour
{

    private PlayerMovement pm;

    [Header("Keybinds")]
    [SerializeField] private KeyCode grappleKey = KeyCode.Mouse1;

    [Header("Visuals")]
    [SerializeField] private Transform cam;
    [SerializeField] private Transform gunTip;
    [SerializeField] private LayerMask grappleable;
    [SerializeField] private LineRenderer lr;

    [Header("GrappleCalculations")]
    [SerializeField] private float maxDistance;
    [SerializeField] private float grappleDelay;
    [SerializeField] private float overshootYAxis;

    private Vector3 GrapplePoint;

    [SerializeField] private float grappleCooldown;
    private float grappleCooldownTimer;

    [Header("State")]
    private bool grappling;

    private void Start()
    {
        pm = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(grappleKey)) //Check if player is trying to grapple
        {
            StartGrapple();
        }

        if (grappleCooldownTimer > 0) //If the grapple is on cooldown, reduce the cooldown timer
        {
            grappleCooldownTimer -= Time.deltaTime;
        }
    }

    private void LateUpdate()
    {
        if (grappling)
        {
            lr.SetPosition(0, gunTip.position); //Set the position of the line renderer
        }
    }

    private void StartGrapple()
    {
        if (grappleCooldownTimer > 0)
        {
            return;
        }

        grappling = true;

        RaycastHit hit; // Throw a raycast to check if the player is looking at a grappleable object
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxDistance, grappleable))
        {
            GrapplePoint = hit.point;
            Invoke(nameof(ExecuteGrapple), grappleDelay);
        }
        else
        {
            GrapplePoint = cam.position + (cam.forward * maxDistance);
            Invoke(nameof(StopGrapple), grappleDelay);
        }

        lr.enabled = true;
        lr.SetPosition(1, GrapplePoint);
    }

    private void ExecuteGrapple()
    {
        //Pull to the grapple point
        Vector3 lowestPoint = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);

        float grapplePointRelativeYPos = GrapplePoint.y - lowestPoint.y;
        float highestPointOnArc = grapplePointRelativeYPos + overshootYAxis;

        if (grapplePointRelativeYPos < 0) {
            highestPointOnArc = overshootYAxis;
        }

        pm.JumpToPosition(GrapplePoint, highestPointOnArc);

        Invoke(nameof(StopGrapple), 1f);
    }

    public void StopGrapple()
    {
        //Stop the grapple and add a cooldown
        grappling = false;

        grappleCooldownTimer = grappleCooldown;

        lr.enabled = false;
    }

    

    
}
