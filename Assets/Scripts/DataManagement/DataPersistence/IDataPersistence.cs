using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Interface to detect objects that save and load data
// Also ensures that they implement methods to load and save
public interface IDataPersistence
{
    void LoadData(GameData gameData);
    void SaveData(GameData gameData);
}
