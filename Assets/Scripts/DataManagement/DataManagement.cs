using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManagement : MonoBehaviour // TODO - Attach to an empty object to activate timer/checkpoint management
{
    [SerializeField] private GameObject leaderboardObject;

    private Timer timer = new Timer();
    private Checkpoint checkpoint = new Checkpoint();
    private Leaderboard leaderboard;

    // Start is called before the first frame update
    void Start()
    {
        this.leaderboard = leaderboardObject.GetComponent<Leaderboard>();
    }

    // Update is called once per frame
    void Update()
    {
        // Increments timer with time if active
        if (timer.IsActive())
        {
            timer.Addtime(Time.deltaTime);
        }
        // Updates leaderboard and resets timer if timer is ended
        if (timer.IsTimerEnded())
        {
            leaderboard.UpdateLeaderboard(this.timer);
            this.timer = new Timer();
        }
    }

    // Pauses the game by stopping time
    public void Pause()
    {
        Time.timeScale = 0;
    }

    // Un-pauses the game by resuming time
    public void Resume()
    {
        Time.timeScale = 1;
    }

    // Toggles the timer inside the data management object
    public void ToggleTimer()
    {
        timer.ToggleTimer();
    }

    // Stops the timer inside the data management object
    public void StopTimer()
    {
        timer.StopTimer();
    }

    // Check if timer inside the data management object is active
    public bool IsTimerActive()
    {
        return timer.IsActive();
    }

    // Get string of timer inside the data management object
    public string GetTimer()
    {
        return timer.ToString();
    }

    // Sets a position to the checkpoint inside the data management object
    public void SetCheckpoint(Vector3 position)
    {
        checkpoint.SetCheckpoint(position);
    }

    // Gets current checkpoint inside the data management object
    public Vector3 GetCheckpoint()
    {
        return checkpoint.GetCheckpoint();
    }

    // Checks if a checkpoint is set inside the data management object
    public bool IsCheckPointSet()
    {
        return checkpoint.IsCheckpointSet();
    }

    // Resets the checkpoint inside the data management object
    public void ResetCheckpoint()
    {
        checkpoint.ResetCheckpoint();
    }
}
