using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManagement : MonoBehaviour
{
    private string fileName = "game.data";
    private string dirPath = Application.persistentDataPath;

    private Timer timer = new Timer();
    private Checkpoint checkpoint = new Checkpoint();
    private Leaderboard leaderboard;
    // Start is called before the first frame update
    void Start()
    {
        leaderboard = new Leaderboard(fileName, dirPath);
        leaderboard.LoadData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
