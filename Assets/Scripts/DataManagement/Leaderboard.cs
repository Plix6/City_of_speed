using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Leaderboard : MonoBehaviour, IDataPersistence // TODO - Attach to an empty object to activate leaderboard + link gameObject to data management script
{
    private List<Timer> leaderboard = new List<Timer>();
    private List<string> leaderboardNames = new List<string>();
    private readonly int LEADERBOARD_SIZE = 10;

    // Only gives the string times of the timers
    public List<string> GetLeaderboardTimes() 
    {
        List<string> entries = new List<string>();
        foreach (Timer timer in leaderboard)
        {
            entries.Add(timer.ToString());
        }
        return entries;
    }

    // Gives the usernames associated to the times
    public List<string> GetLeaderboardNames()
    {
        return leaderboardNames;
    }

    // Updates leaderboard with new time and username
    // Slots the time in its right position + deletes extra records if list is too long
    public void UpdateLeaderboard(Timer newTime, string newUsername)
    {
        foreach (Timer timer in leaderboard)
        {
            // If current timer is lower than compared timer, insert it
            if (timer.Compare(newTime) == -1) 
            {
                int index = leaderboard.IndexOf(timer);
                leaderboard.Insert(index, newTime);
                leaderboardNames.Insert(index, newUsername);
                break;
            }
            // If compared timer is last list item and not 10th record, add current timer at the end
            else if (leaderboard.IndexOf(timer) + 1 == leaderboard.Count)
            {
                leaderboard.Add(newTime);
                leaderboardNames.Add(newUsername);
                break;
            }
        }
        // If no leaderboard entries before, foreach will not run
        if (leaderboard.Count == 0) 
        {
            leaderboard.Add(newTime);
            leaderboardNames.Add(newUsername);
        }
        // If list exceeds 10 records, remove slowest timer
        if (leaderboard.Count > LEADERBOARD_SIZE)
        {
            leaderboard.RemoveAt(leaderboard.Count - 1);
            leaderboardNames.RemoveAt(leaderboard.Count - 1);
        }
    }

    // Converts data from timer list to game data
    private List<float> ConvertFromLeaderboardToGameData(List<Timer> curLeaderboard) 
    {
        List<float> values = new List<float>();

        foreach (Timer timer in curLeaderboard)
        {
            values.Add(timer.GetTime());
        }

        return values;
    }

    // Converts data from game data to timer list
    private List<Timer> ConvertFromGameDataToLeaderboard(GameData gameData)
    {
        List<Timer> temp = new List<Timer>();

        foreach (float value in gameData.timerLeaderboardValues)
        {
            Timer timer = new Timer();
            timer.Addtime(value);
            timer.StopTimer();
            temp.Add(timer);
        }

        return temp;
    }

    // Method inherited from data persistence interface to load data in this component
    public void LoadData(GameData gameData)
    {
        this.leaderboard = ConvertFromGameDataToLeaderboard(gameData);
        this.leaderboardNames = gameData.usernameLeaderboardValues;
    }

    // Method inherited from data persistence interface to save data from this component
    public void SaveData(GameData gameData)
    {
        gameData.timerLeaderboardValues = ConvertFromLeaderboardToGameData(this.leaderboard);
        gameData.usernameLeaderboardValues = this.leaderboardNames;
    }
}
