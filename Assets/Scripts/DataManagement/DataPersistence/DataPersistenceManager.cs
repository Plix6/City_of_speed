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
        if (instance != null)
        {
            Debug.LogError("More than one instance of the data persistence manager");
        }
        instance = this;
    }

    private void Start()
    {
        this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistencesObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

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

    public void SaveGame()
    {
        foreach (IDataPersistence dataPersistenceObject in dataPersistencesObjects)
        {
            dataPersistenceObject.SaveData(gameData);
        }

        fileDataHandler.Save(this.gameData);
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
