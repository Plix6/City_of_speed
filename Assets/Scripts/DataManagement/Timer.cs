using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Timer : MonoBehaviour
{
    [SerializeField] private bool timerOn = false;
    [SerializeField] private bool endTimer = false;
    [SerializeField] private float time = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timerOn)
        {
            time += Time.deltaTime;
        }
    }

    public void ToggleTimer()
    {
        if (!endTimer)
        {
            timerOn = !timerOn;
        }
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
        return (int) (time - (int) time) * 1000;
    }

    public void ResetTimer()
    {
        if (!endTimer)
        {
            time = 0;
        }
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
