using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerExit_Listener : MonoBehaviour
{
    public string targetTag;
    public UnityEvent OnEventRaised_TriggerExit;

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == targetTag)
        {
            Respond(OnEventRaised_TriggerExit);
        }
    }

    private void Respond(UnityEvent temp)
    {
        if (temp != null)
            temp.Invoke();
    }


}
