using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractPointer : MonoBehaviour
{
    [SerializeField]
    GameObject currentTarget;
    void RayCast()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 3.75f))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            currentTarget = hit.collider.gameObject;
            Debug.Log(hit.collider.gameObject.name);
            //Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 10f, Color.white);
            currentTarget = null;
            //Debug.Log("Did not Hit");
        }
    }

    public void RequestPickUP()
    {
        RayCast();
        if (currentTarget != null)
        {
            
            Interactable thisInteractable = currentTarget.GetComponent<Interactable>();
            Debug.Log(thisInteractable);
            if (thisInteractable != null)
            {
                
                thisInteractable.Interacted();
                currentTarget = null;

            }
            thisInteractable = null;
        }
        else
        {
            Debug.Log("No valid interact target");
        }
    }

  
}