using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    // Game properties that need to be saved
    public List<float> timerLeaderboardValues;
    public List<string> usernameLeaderboardValues;

    // Initialization of new data for a new game
    public GameData() 
    {
        this.timerLeaderboardValues = new List<float>();
        this.usernameLeaderboardValues = new List<string>();
    }
}
