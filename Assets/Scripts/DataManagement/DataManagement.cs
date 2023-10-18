using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManagement : MonoBehaviour
{
    private string fileName = "game.data";
    private string dirPath = Application.persistentDataPath;

    public Timer timer = new Timer();
    public Checkpoint checkpoint = new Checkpoint();
    public Leaderboard leaderboard;
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

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }
}
