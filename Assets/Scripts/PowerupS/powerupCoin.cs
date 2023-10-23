using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerupCoin : MonoBehaviour
{
    // Objet qui va stocker un effet visuel
    public GameObject CoinEffect;
    // Valeur de notre pièce
    public int value = 1;
    
    void OnTriggerEnter(Collider other)
    {
        TakeCoin();
    }

    void TakeCoin()
    {
        // Effet visuel quand la pièce est touchée
        Instantiate(CoinEffect, transform.position, transform.rotation);

        // Suppression de la pièce et augmentation du compteur
        Destroy(gameObject);
        CoinCounter.instance.IncreaseCoins(value);
    }
}
