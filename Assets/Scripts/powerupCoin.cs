using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerupCoin : MonoBehaviour
{
    public GameObject CoinEffect;
    public int value = 1;
    
    void OnTriggerEnter(Collider other)
    {
        TakeCoin();
    }

    void TakeCoin()
    {
        //Effect when the coin is touched
        Instantiate(CoinEffect, transform.position, transform.rotation);
        

        //Destroy after taken
        Destroy(gameObject);
        CoinCounter.instance.IncreaseCoins(value);
    }
}
