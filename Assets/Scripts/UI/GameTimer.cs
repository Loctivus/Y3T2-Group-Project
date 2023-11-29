using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public TMP_Text timerText;
    public FloatVariable currentMinutes;
    public FloatVariable currentSeconds;
    public FloatVariable currentMilli;
    private void FixedUpdate()
    {
        timerText.text = currentMinutes.RuntimeValue.ToString("00") + ":" + currentSeconds.RuntimeValue.ToString("00") + ":" + currentMilli.RuntimeValue.ToString("00");
    }
}
