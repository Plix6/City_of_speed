using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Leaderboard : MonoBehaviour, IDataPersistence // TODO - Attach to an empty object to activate leaderboard + link gameObject to data management script
{
    [SerializeField] private List<Timer> leaderboard = new List<Timer>();
    [SerializeField] private int leaderboardSize = 10;
    private string fileName;
    private string dirPath;
    public Leaderboard(string fileName, string dirPath)
    {
        this.fileName = fileName;
        this.dirPath = dirPath;
    }

    public List<string> GetLeaderboard() // Only gives the string times of the timers
    {
        List<string> entries = new List<string>();
        foreach (Timer timer in leaderboard)
        {
            entries.Add(timer.ToString());
        }
        return entries;
    }

    public void UpdateLeaderboard(Timer newTime)
    {
        foreach (Timer timer in leaderboard)
        {
            if (timer.Compare(newTime) == -1) // If current timer is lower than compared timer, insert it
            {
                leaderboard.Insert(leaderboard.IndexOf(timer), newTime);
                break;
            }
            else if (leaderboard.IndexOf(timer) + 1 == leaderboard.Count)
            {
                leaderboard.Add(newTime);
                break;
            }
        }
        if (leaderboard.Count == 0) // If no leaderboard entries before, foreach will not run
        {
            leaderboard.Add(newTime);
        }
        if (leaderboard.Count > leaderboardSize)
        {
            leaderboard.RemoveAt(leaderboard.Count - 1);
        }
    }

    private List<float> ConvertFromLeaderboardToGameData(List<Timer> curLeaderboard) 
    {
        List<float> values = new List<float>();

        foreach (Timer timer in curLeaderboard)
        {
            values.Add(timer.GetTime());
        }

        return values;
    }

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

    public void LoadData(GameData gameData)
    {
        this.leaderboard = ConvertFromGameDataToLeaderboard(gameData);
    }

    public void SaveData(GameData gameData)
    {
        gameData.timerLeaderboardValues = ConvertFromLeaderboardToGameData(this.leaderboard);
    }
}
