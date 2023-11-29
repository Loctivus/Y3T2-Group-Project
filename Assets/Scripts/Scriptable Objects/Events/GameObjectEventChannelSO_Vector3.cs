
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/GameObject Event Channel Vector")]
public class GameObjectEventChannelSO_Vector3 : ScriptableObject
{
    public UnityAction<GameObject,Vector3> OnEventRaised;

    public void RaiseEvent(GameObject obj, Vector3 v3)
    {
        if (OnEventRaised != null)
        {
            Debug.Log("Void Event Invoked");
            OnEventRaised.Invoke(obj,v3);
        }
    }
}
