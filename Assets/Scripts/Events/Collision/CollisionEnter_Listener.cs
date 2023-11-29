using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionEnter_Listener : MonoBehaviour
{

    public string targetTag;
    public UnityEvent OnEventRaised_CollisionEnter;
    public float interactDelay;
    [SerializeField] bool valid = true;

   
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == targetTag && valid)
        {
            valid = false;
            Respond(OnEventRaised_CollisionEnter);
        }
    }
    private void Respond(UnityEvent temp)
    {
        if (temp != null)
            temp.Invoke();
    }

    IEnumerator ResetDelay()
    {
        yield return new WaitForSeconds(interactDelay);
        valid = true;
    }
}
