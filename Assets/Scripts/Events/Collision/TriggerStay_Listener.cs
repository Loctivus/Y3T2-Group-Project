using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerStay_Listener : MonoBehaviour
{
    public string targetTag;
    public UnityEvent OnEventRaised_TriggerStay;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == targetTag)
        {
            Respond(OnEventRaised_TriggerStay);
        }
    }

    private void Respond(UnityEvent temp)
    {
        if (temp != null)
            temp.Invoke();
    }

}
