using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileDataHandler
{
    private string dirPath;
    private string fileName;

    public FileDataHandler(string dirPath, string fileName)
    {
        this.dirPath = dirPath;
        this.fileName = fileName;
    }

    // Retrieve data from file on disk and convert it from JSON to GameData object
    public GameData Load()
    {
        string fullPath = Path.Combine(dirPath, fileName);
        GameData dataLoaded = null;
        if (File.Exists(fullPath))
        {
            try
            {
                string dataFromFile = string.Empty;

                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataFromFile = reader.ReadToEnd();
                    }
                }

                dataLoaded = JsonUtility.FromJson<GameData>(dataFromFile);
            }
            catch (Exception e)
            {
                Debug.LogError("Following error happened when loading game data : " + e);
            }
        }
        return dataLoaded;
    }

    // Convert data from GameData object to JSON and save it to file on disk
    public void Save(GameData gameData)
    {
        string fullPath = Path.Combine(dirPath, fileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            string dataStored = JsonUtility.ToJson(gameData);

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataStored);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Following error happened when saving game data : " + e);
        }
    }
}
