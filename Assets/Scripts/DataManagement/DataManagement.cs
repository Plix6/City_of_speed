using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManagement : MonoBehaviour
{
    private string fileName = "game.data";
    private string dirPath;

    public Timer timer = new Timer();
    public Checkpoint checkpoint = new Checkpoint();
    public Leaderboard leaderboard;

    public bool dataTest = false;
    // Start is called before the first frame update
    void Start()
    {
        leaderboard = new Leaderboard(fileName, dirPath: Application.persistentDataPath);
        leaderboard.LoadData();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer.IsActive())
        {
            timer.Addtime(Time.deltaTime);
        }
        if (dataTest)
        {
            this.DataTesting();
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }

    private void DataTesting()
    {
        dataTest = false;

        timer.StopTimer();
        Debug.Log("Initial length : " + leaderboard.GetLeaderboard().Count);
        leaderboard.UpdateLeaderboard(timer);
        Debug.Log("Final length : " + leaderboard.GetLeaderboard().Count);

        foreach (string record in leaderboard.GetLeaderboard())
        {
            Debug.Log(record);
        }

        leaderboard.SaveData();
        Debug.Log("Length after save : " + leaderboard.GetLeaderboard().Count);
        leaderboard.LoadData();
        Debug.Log("Length after load : " + leaderboard.GetLeaderboard().Count);

        foreach (string record in leaderboard.GetLeaderboard())
        {
            Debug.Log(record);
        }

        timer = new Timer();
    }
}
