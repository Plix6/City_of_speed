using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerupLessSpeed : MonoBehaviour
{
    // Effet visuel, multiplicateur, dur�e du powerup (les trois publiques)
    public GameObject UnSpeedEffect;
    public float multiplier = 1.5f;
    public float duration = 4;

    // Quand on touche le powerup
    void OnTriggerEnter(Collider other)
    {
        // Utilisation d'une coroutine pour l'utilisation d'une dur�e
        StartCoroutine(Slower(other));
    }

    IEnumerator Slower(Collider player)
    {
        //Effet visuel
        Instantiate(UnSpeedEffect, transform.position, transform.rotation);

        //Changement de vitesse (on r�cup�re les valeurs dans le script du player)
        PlayerMovement playerMovement = player.transform.parent.GetComponent<PlayerMovement>();
        playerMovement.walkSpeed /= multiplier;
        playerMovement.sprintSpeed /= multiplier;

        // On attend X secondes avant de d�truire (donc on rend invisible le powerup)
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(duration);

        // On remet tout normal
        playerMovement.walkSpeed *= multiplier;
        playerMovement.sprintSpeed *= multiplier;

        // Et on d�truit!
        Destroy(gameObject);
    }
}
