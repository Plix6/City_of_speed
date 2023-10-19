using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    public List<float> timerLeaderboardValues;

    public GameData() 
    {
        this.timerLeaderboardValues = new List<float>();
    }
}
