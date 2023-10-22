using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class swinging : MonoBehaviour
{

    [SerializeField] private KeyCode swingKey = KeyCode.Mouse0;
    [SerializeField] private LineRenderer lr;
    [SerializeField] private Transform gunTip;
    [SerializeField] private Transform cam;
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask grappleable;

    private float maxSwingDistance = 25f;
    private Vector3 swingPoint;
    private Vector3 currentGrapplePosition;
    private SpringJoint joint;


    private void Update()
    {
        if (Input.GetKeyDown(swingKey)) StartSwing();
        if (Input.GetKeyUp(swingKey)) StopSwing();
    }

    private void LateUpdate()
    {
        DrawRope();
    }
    private void StartSwing()
    {
       RaycastHit hit;
       if (Physics.Raycast(cam.position, cam.forward, out hit, maxSwingDistance, grappleable))
        {
              swingPoint = hit.point;
              joint = player.gameObject.AddComponent<SpringJoint>();
              joint.autoConfigureConnectedAnchor = false;
              joint.connectedAnchor = swingPoint;
    
              float distanceFromPoint = Vector3.Distance(player.position, swingPoint);
    
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
    {
        lr.enabled = false;
        Destroy(joint);
    }

    void DrawRope()
    {
        if (!joint) return;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, swingPoint, Time.deltaTime * 8f);

        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, swingPoint);
    }
}
