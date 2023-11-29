using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelArea : MonoBehaviour
{
    [SerializeField] VoidEventChannelSO fadeOutEventChannel;
    UI_Listener_LoadScenes loadOutroListener;
    LoadEventChannelSO loadEventSO;

    private void Awake()
    {
        loadOutroListener = GetComponent<UI_Listener_LoadScenes>();
        loadEventSO = loadOutroListener.onPlayButtonPress;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //loadEventSO.RaiseEvent(loadOutroListener.locationsToLoad);
            StartCoroutine(FadeToLoad());
        }
    }

    IEnumerator FadeToLoad()
    {
        fadeOutEventChannel.RaiseEvent();
        yield return new WaitForSeconds(2f);
        loadEventSO.RaiseEvent(loadOutroListener.locationsToLoad);
    }
}
