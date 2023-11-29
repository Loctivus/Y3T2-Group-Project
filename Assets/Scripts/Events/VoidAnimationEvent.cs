using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidAnimationEvent : MonoBehaviour
{
    [SerializeField] VoidEventChannelSO thisEvent;
    public void TriggerEvent()
    {
        thisEvent.RaiseEvent();
        Cursor.lockState = CursorLockMode.Confined;
    }
}
