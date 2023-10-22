using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerupLessSpeed : MonoBehaviour
{
    public GameObject JumpEffect;
    public float multiplier = 1.5f;
    public float duration = 4;

    void OnTriggerEnter(Collider other)
    {
        StartCoroutine(JumpHigher(other));

    }

    IEnumerator JumpHigher(Collider player)
    {
        //Effect when the powerup is touched
        Instantiate(JumpEffect, transform.position, transform.rotation);

        //Change speed
        PlayerMovement playerMovement = player.transform.parent.GetComponent<PlayerMovement>();
        playerMovement.walkSpeed /= multiplier;
        playerMovement.sprintSpeed /= multiplier;

        //Wait X seconds
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(duration);

        //Revert back to normal
        playerMovement.walkSpeed *= multiplier;
        playerMovement.sprintSpeed *= multiplier;

        //Destroy after taken
        Destroy(gameObject);
    }
}
