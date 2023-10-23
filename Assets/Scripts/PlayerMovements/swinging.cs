using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swinging : MonoBehaviour
{
    [Header("Keybinds")]
    [SerializeField] private KeyCode swingKey = KeyCode.Mouse0;

    [Header("Visuals")]
    [SerializeField] private LineRenderer lr;
    [SerializeField] private Transform gunTip;
    [SerializeField] private Transform cam;
    [SerializeField] private Transform player;

    [Header("Targets")]
    [SerializeField] private LayerMask grappleable;

    [Header("SwingCalculations")]
    [SerializeField] private float maxSwingDistance = 50f;
    private Vector3 swingPoint;
    private Vector3 currentGrapplePosition;
    private SpringJoint joint;


    private void Update()
    {
        // Check if the player is trying to swing
        if (Input.GetKeyDown(swingKey)) StartSwing();
        if (Input.GetKeyUp(swingKey)) StopSwing();
    }

    private void LateUpdate()
    {
        DrawRope();
    }
    private void StartSwing()
    {
        // Throw a raycast to check if the player is looking at a grappleable object
       RaycastHit hit;
       if (Physics.Raycast(cam.position, cam.forward, out hit, maxSwingDistance, grappleable))
        {
              // Create a joint for the player to swing around
              swingPoint = hit.point;
              joint = player.gameObject.AddComponent<SpringJoint>();
              joint.autoConfigureConnectedAnchor = false;
              joint.connectedAnchor = swingPoint;
    
              float distanceFromPoint = Vector3.Distance(player.position, swingPoint);
    
              //Various parameters for the joint
              joint.maxDistance = distanceFromPoint * 0.8f;
              joint.minDistance = distanceFromPoint * 0.25f;
              joint.spring = 4.5f;
              joint.damper = 7f;
              joint.massScale = 4.5f;

            lr.enabled = true;
            currentGrapplePosition = gunTip.position;
    
         }
    }

    private void StopSwing()
    {   // Destroy the joint and the line renderer when not swinging
        lr.enabled = false;
        Destroy(joint);
    }

    void DrawRope()
    {

        //Draw rope if grapple is active;
        if (!joint) return;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, swingPoint, Time.deltaTime * 8f);

        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, swingPoint);
    }
}
