using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Timer
{
    // TODO : change data to link with other class which can be serialized
    [SerializeField] private bool timerOn = false;
    [SerializeField] private bool endTimer = false;
    [SerializeField] private float time = 0;


    public void ToggleTimer()
    {
        if (!endTimer)
        {
            timerOn = !timerOn;
        }
    }

    public bool IsActive()
    {
        return this.timerOn && !this.endTimer;
    }

    private int GetTimerMinutes()
    {
        return (int) time / 60;
    }

    private int GetTimerSeconds()
    {
        return (int) time % 60;
    }

    private int GetTimerMilliseconds()
    {
        return (int) ((time - (int) time) * 1000f);
    }

    public void ResetTimer()
    {
        if (!endTimer)
        {
            time = 0;
        }
    }

    public void Addtime(float delta)
    {
        time += delta;
    }

    public void StopTimer()
    {
        endTimer = true;
    }

    public float GetTime()
    {
        return this.time;
    }

    public override string ToString()
    {
        return string.Format("{0}:{1}:{2}", 
            this.GetTimerMinutes(), 
            this.GetTimerSeconds(), 
            this.GetTimerMilliseconds());
    }

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
