using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionExit_Listener : MonoBehaviour
{
    public string targetTag;
    public UnityEvent OnEventRaised_CollisionExit;
    
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == targetTag)
        {
            Respond(OnEventRaised_CollisionExit);
        }
    }

    private void Respond(UnityEvent temp)
    {
        if (temp != null)
            temp.Invoke();
    }
}
