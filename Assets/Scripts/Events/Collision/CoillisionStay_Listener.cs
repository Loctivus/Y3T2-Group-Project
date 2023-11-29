using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class CoillisionStay_Listener : MonoBehaviour
{
    // Simplify Respond, Refactor Directly Into Collision Event
    public string targetTag;
    public UnityEvent OnEventRaised_CollisionStay;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == targetTag)
        {
            Respond(OnEventRaised_CollisionStay);
        }
    }

    private void Respond(UnityEvent temp)
    {
        if (temp != null)
            temp.Invoke();
    }
}
