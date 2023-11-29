using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEnter_Listener : MonoBehaviour
{
    public string targetTag;
    public UnityEvent OnEventRaised_TriggerEnter;
    public float interactDelay;
    [SerializeField] bool valid;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == targetTag && valid)
        {
            valid = false;
            Respond(OnEventRaised_TriggerEnter);
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
