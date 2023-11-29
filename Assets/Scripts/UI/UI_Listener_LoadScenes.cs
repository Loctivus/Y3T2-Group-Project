using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Listener_LoadScenes : MonoBehaviour
{
    public LoadEventChannelSO onPlayButtonPress;
    public GameSceneSO[] locationsToLoad;
    public void OnPlayButtonPress()
    {
        onPlayButtonPress.RaiseEvent(locationsToLoad);
    }

    public void OnPlayButtonPressDelay(float t)
    {
        StartCoroutine(DelayLoad(t));
    }
    IEnumerator DelayLoad(float t)
    {
        yield return new WaitForSeconds(t);
        onPlayButtonPress.RaiseEvent(locationsToLoad);
    }
}
