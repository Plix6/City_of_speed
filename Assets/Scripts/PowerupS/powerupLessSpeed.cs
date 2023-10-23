using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerupLessSpeed : MonoBehaviour
{
    // Effet visuel, multiplicateur, durée du powerup (les trois publiques)
    public GameObject UnSpeedEffect;
    public float multiplier = 1.5f;
    public float duration = 4;

    // Quand on touche le powerup
    void OnTriggerEnter(Collider other)
    {
        // Utilisation d'une coroutine pour l'utilisation d'une durée
        StartCoroutine(Slower(other));
    }

    IEnumerator Slower(Collider player)
    {
        //Effet visuel
        Instantiate(UnSpeedEffect, transform.position, transform.rotation);

        //Changement de vitesse (on récupère les valeurs dans le script du player)
        PlayerMovement playerMovement = player.transform.parent.GetComponent<PlayerMovement>();
        playerMovement.walkSpeed /= multiplier;
        playerMovement.sprintSpeed /= multiplier;

        // On attend X secondes avant de détruire (donc on rend invisible le powerup)
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(duration);

        // On remet tout normal
        playerMovement.walkSpeed *= multiplier;
        playerMovement.sprintSpeed *= multiplier;

        // Et on détruit!
        Destroy(gameObject);
    }
}
