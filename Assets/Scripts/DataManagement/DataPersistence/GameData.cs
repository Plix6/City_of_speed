using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    // Game properties that need to be saved
    public List<float> timerLeaderboardValues;

    // Initialization of new data for a new game
    public GameData() 
    {
        this.timerLeaderboardValues = new List<float>();
    }
}
