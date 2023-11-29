using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/GameObject Event Channel")]
public class GameObjectEventChannelSO : ScriptableObject
{
    public UnityAction<GameObject> OnEventRaised;

    public void RaiseEvent(GameObject obj)
    {
        if (OnEventRaised != null)
        {
            //Debug.Log("Void Event Invoked");
            OnEventRaised.Invoke(obj);
        }
    }
}
