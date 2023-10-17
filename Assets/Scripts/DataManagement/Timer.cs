using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private bool timerOn = false;
    private float time = 0;

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

    public void toggleTimer()
    {
        timerOn = !timerOn;
    }

    public int getTimerMinutes()
    {
        return (int) time / 60;
    }

    public int getTimerSeconds()
    {
        return (int) time % 60;
    }

    public int getTimerMilliseconds()
    {
        return (int) (time - (int) time) * 1000;
    }

    public void resetTimer()
    {
        time = 0;
    }
}
