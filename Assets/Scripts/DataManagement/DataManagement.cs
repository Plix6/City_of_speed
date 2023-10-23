using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManagement : MonoBehaviour // TODO - Attach to an empty object to activate timer/checkpoint management
{
    [SerializeField] private GameObject leaderboardObject;

    [SerializeField] private Timer timer = new Timer();
    [SerializeField] private string username = string.Empty;
    private Checkpoint checkpoint = new Checkpoint();
    [SerializeField] private Leaderboard leaderboard;

    private readonly int USERNAME_CHARACTER_LIMIT = 10;

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
        if (timer.IsTimerEnded() && username != string.Empty)
        {
            leaderboard.UpdateLeaderboard(this.timer, username);
            this.timer = new Timer();
            this.username = string.Empty;
        }
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

    // Gives the number of previous checkpoints reached
    public int GetPreviousCheckpointsNumber()
    {
        return checkpoint.GetPreviousCheckpointsNumber();
    }

    // Sets the username. Returns a bool to check if username has been set
    public bool SetUsername(string name)
    {
        if (name.Length > USERNAME_CHARACTER_LIMIT)
        {
            return false;
        }
        this.username = name;
        return true;
    }
}
