using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExitDoor : Interactable
{
    [SerializeField] UnityEvent OnEventRaised;
    public override void Interacted()
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke();
    }
}
