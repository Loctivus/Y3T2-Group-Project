using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameTime : MonoBehaviour
{
    public FloatVariable currentMinutes;
    public FloatVariable currentSeconds;
    public FloatVariable currentMilli;
    float milliseconds, seconds, minutes;
    float time;
    private void OnEnable()
    {
        milliseconds = 0;
        seconds = 0;
        minutes = 0;
        time = 0;
    }
    void Update()
    {
        /*
        currentTime.RuntimeValue += Time.deltaTime;

        string minutes = ((int)(Time.timeSinceLevelLoad / 60f)).ToString("00");
        string seconds = ((int)(Time.timeSinceLevelLoad % 60f)).ToString("00");
        string milli = ((int)(Time.timeSinceLevelLoad * 6f)).ToString("00");

        timerText.text = minutes + ":" + seconds + ":" +  milli;
        */

        time += Time.deltaTime;

        minutes = (int)(time / 60f) % 60;
        seconds = (int)(time % 60f);
        milliseconds = (int)(time * 1000f) % 1000;

        currentMilli.RuntimeValue = milliseconds;
        currentSeconds.RuntimeValue = seconds;
        currentMinutes.RuntimeValue = minutes;
        
    }
}
