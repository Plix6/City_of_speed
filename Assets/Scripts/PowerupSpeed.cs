using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpeed : MonoBehaviour
{
    // Effet visuel
    public GameObject SpeedEffect;
    // Multiplicateur (publique)
    public float multiplier = 1.5f;
    // Durée du powerup (publique)
    public float duration = 4;

    // Quand le powerup est touché
    void OnTriggerEnter(Collider other)
    {
        // Utilisation d'une Coroutine pour pouvoir utiliser une durée 
        StartCoroutine(SpeedBoost(other));
        
    }

    IEnumerator SpeedBoost(Collider player)
    {
        //Effect visuel
        Instantiate(SpeedEffect, transform.position, transform.rotation);

        //Change la vitesse (on récupère la vitesse du player dans son script)
        PlayerMovement playerMovement = player.transform.parent.GetComponent<PlayerMovement>();
        playerMovement.walkSpeed *= multiplier;
        playerMovement.sprintSpeed *= multiplier;

        //On rend invisible le mesh et les collisions et on attend X secondes avant de détruire
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(duration);

        // On remet les valeurs normales
        playerMovement.walkSpeed /= multiplier;
        playerMovement.sprintSpeed /= multiplier;

        // Et on détruit le powerup!
        Destroy(gameObject);
    }
}
