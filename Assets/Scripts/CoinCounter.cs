using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCounter : MonoBehaviour
{
    // Instanciation du compteur pour pouvoir l'augmenter en continu
    public static CoinCounter instance;
    // Texte sur l'UI
    public TMP_Text coinText;
    // Nombre actuel de pièces 
    public int currentCoins = 0;

    // Instanciation du compteur
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // Affichage du compteur au démarrage
        coinText.text = currentCoins.ToString();
    }

    // Fonction utilisée à chaque fois qu'une pièce est obtenue (augmente le compteur de la valeur)
    public void IncreaseCoins(int value)
    {
       
        currentCoins += value;
        coinText.text = currentCoins.ToString();
    }
}
