using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInputs : MonoBehaviour
{
    //public PlayerEventChannelSO pecSO;
    public VoidEventChannelSO interactKeySO;
    public VoidEventChannelSO leftMouseButton;


    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            //interactKeySO.RaiseEvent();
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            leftMouseButton.RaiseEvent();
        }
    }

}

