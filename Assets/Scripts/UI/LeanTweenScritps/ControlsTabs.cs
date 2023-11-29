using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsTabs : MonoBehaviour
{
    public float tweenTime;

    public void Start()
    {
        LeanTween.cancel(gameObject);
        LeanTween.moveY(gameObject, Screen.height, tweenTime).setEaseOutBack().setIgnoreTimeScale(true);
    }
    public void HideTab()
    {
        var hidLoc = Screen.height + Screen.height / 2;
        LeanTween.cancel(gameObject);
        LeanTween.moveY(gameObject, hidLoc, tweenTime).setEaseInBack().setIgnoreTimeScale(true);
    }
}
