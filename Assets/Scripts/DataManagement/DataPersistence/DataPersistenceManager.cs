using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour // TODO - link to empty object to enable saving/loading
{
    [SerializeField] private string fileName = "game.data";
    private GameData gameData;
    private List<IDataPersistence> dataPersistencesObjects;

    private FileDataHandler fileDataHandler;

    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        // Check that only one instance of the object is present, to avoid data conflicts
        if (instance != null)
        {
            Debug.LogError("More than one instance of the data persistence manager");
        }
        instance = this;
    }

    // Set properties and load game data
    private void Start()
    {
        this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistencesObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    // Save game on exit
    private void OnApplicationQuit()
    {
        SaveGame();
    }

    // Create new set of game data
    public void NewGame()
    {
        this.gameData = new GameData();
    }

    // Load data from disk file and transfer data to objects which need it
    public void LoadGame()
    {
        this.gameData = fileDataHandler.Load();
        if (this.gameData == null)
        {
            Debug.Log("No game data found in file");
            NewGame();
        }

        foreach (IDataPersistence dataPersistenceObject in dataPersistencesObjects)
        {
            dataPersistenceObject.LoadData(gameData);
        }
    }

    // Retrieves data to be saved from game objects and save data to disk file
    public void SaveGame()
    {
        foreach (IDataPersistence dataPersistenceObject in dataPersistencesObjects)
        {
            dataPersistenceObject.SaveData(gameData);
        }

        fileDataHandler.Save(this.gameData);
    }

    // Use Linq to get all game objects which implement the IDataPersistence interface
    // and therefore load and save data to file on disk
    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
