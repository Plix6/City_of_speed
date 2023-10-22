using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCounter : MonoBehaviour
{
    public static CoinCounter instance;
    public TMP_Text coinText;
    public int currentCoins = 0;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        coinText.text = currentCoins.ToString();
    }

    public void IncreaseCoins(int value)
    {
        currentCoins += value;
        coinText.text = currentCoins.ToString();
    }
}
