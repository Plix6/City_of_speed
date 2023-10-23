using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Timer
{
    [SerializeField] private bool timerOn = false;
    [SerializeField] private bool endTimer = false;
    [SerializeField] private float time = 0;

    // Toggles timer on and off
    public void ToggleTimer()
    {
        if (!endTimer)
        {
            timerOn = !timerOn;
        }
    }

    // Checks if timer is active
    public bool IsActive()
    {
        return this.timerOn && !this.endTimer;
    }

    // Get timer minutes from float
    private int GetTimerMinutes()
    {
        return (int) time / 60;
    }

    // Get timer seconds from float
    private int GetTimerSeconds()
    {
        return (int) time % 60;
    }

    // Get timer milliseconds from float
    private int GetTimerMilliseconds()
    {
        return (int) ((time - (int) time) * 1000f);
    }

    // Increments the timer by time delta (Time.deltatime ideally)
    public void Addtime(float delta)
    {
        time += delta;
    }

    // Ends the timer. Can't be activated again
    public void StopTimer()
    {
        endTimer = true;
    }

    // Checks if timer is ended
    public bool IsTimerEnded()
    {
        return endTimer;
    }

    // Get float value of timer. Used to compare with other timers or store the data
    public float GetTime()
    {
        return this.time;
    }

    // Gives timer in a more readable format
    public override string ToString()
    {
        return string.Format("{0:D2}:{1:D2}:{2:D3}", 
            this.GetTimerMinutes(), 
            this.GetTimerSeconds(), 
            this.GetTimerMilliseconds());
    }

    // Compares two Timer objects with their float timer value
    public int Compare(Timer other) // 1 = other higher / -1 = other lower
    {
        if(this.GetTime() == other.GetTime())
        {
            return 0;
        }
        else if (this.GetTime() > other.GetTime())
        {
            return -1;
        }
        else
        {
            return 1;
        }
    }
}
