using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] VoidEventChannelSO pKeyPressEventChannel;
    [SerializeField] VoidEventChannelSO eKeyPressEventChannel;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            pKeyPressEventChannel.RaiseEvent();
        if (Input.GetKeyDown(KeyCode.E))
            eKeyPressEventChannel.RaiseEvent();
    }
}
