using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManagement : MonoBehaviour // TODO - Attach to an empty object to activate timer/checkpoint management
{
    [SerializeField] private Timer timer = new Timer();
    [SerializeField] private Checkpoint checkpoint = new Checkpoint();
    [SerializeField] private GameObject leaderboardObject;
    private Leaderboard leaderboard;

    // Start is called before the first frame update
    void Start()
    {
        this.leaderboard = leaderboardObject.GetComponent<Leaderboard>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer.IsActive())
        {
            timer.Addtime(Time.deltaTime);
        }
        if (timer.IsTimerEnded())
        {
            leaderboard.UpdateLeaderboard(this.timer);
            this.timer = new Timer();
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
}
