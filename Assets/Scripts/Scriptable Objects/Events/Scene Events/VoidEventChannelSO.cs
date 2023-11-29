using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Void Event Channel")]
public class VoidEventChannelSO : ScriptableObject
{
    public UnityAction OnEventRaised;

    public void RaiseEvent()
    {
        if(OnEventRaised != null)
        {
            Debug.Log("Void Event Invoked");
            OnEventRaised.Invoke();
        }
    }

}
