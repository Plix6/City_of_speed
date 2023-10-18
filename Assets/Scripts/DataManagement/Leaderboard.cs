using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Leaderboard
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

    public void LoadData()
    {
        // Get full path of data storage location
        string fullPath = Path.Combine(dirPath, fileName);
        if (File.Exists(fullPath))
        {
            try
            {   
                // Create variable to store file content
                string dataLoad = string.Empty;
                Debug.Log("Init load data string");
                // Create stream to read data from file
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        // Read data from file
                        dataLoad = reader.ReadToEnd();
                        Debug.Log("Loaded file content : " + dataLoad);
                    }
                }
                // Deserialize the JSON data into the original format
                leaderboard = JsonUtility.FromJson<List<Timer>>(dataLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Following error occured when trying to load leaderboard : " + e);
            }
        } 
        else
        {
            // If no data is present or an error occurs, we store an empty list
            Debug.LogError("No file stored the data");
            leaderboard = new List<Timer>();
        }
    }

    public void SaveData()
    {
        // Get full path of data storage location
        string fullPath = Path.Combine(dirPath, fileName);
        try
        {
            // Create directory of data storage location if not exists
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            // Convert data to be stored to JSON
            string dataStored = JsonUtility.ToJson(leaderboard);
            Debug.Log("Data to JSON : " + dataStored);
            // Create stream to write data into file
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    // Store data into file
                    writer.Write(dataStored);
                    Debug.Log("Data stored");
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Following error occured when trying to save leaderboard : " + e);
        }
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
            if (timer.Compare(newTime) == -1)
            {
                leaderboard.Insert(leaderboard.IndexOf(timer), newTime);
                if (leaderboard.Count > leaderboardSize)
                {
                    leaderboard.RemoveAt(leaderboard.Count);
                }
                break;
            }
        }
        if (leaderboard.Count == 0) // If no leaderboard entries before, foreach will not run
        {
            leaderboard.Add(newTime);
        }
    }
}
