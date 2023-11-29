using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeting : MonoBehaviour
{
    [SerializeField] bool activate;
    [SerializeField] float aggroDistance;
    public Vector3Variable targetPos;
    [SerializeField] GameObjectEventChannelSO alertedChannel;

    // If not active check if target is inrange if so trigger alert event
    private void FixedUpdate()
    {
        if (!activate)
        {
            if (inRange(aggroDistance))
            {
                activate = true;
                alertedChannel.RaiseEvent(gameObject);
            }
        }

    }
    public bool inRange(float range)
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, targetPos.value - transform.position, out hit, range, layerMask))
        {
            if (hit.collider.tag == "Player")
            {
                Debug.DrawRay(transform.position, targetPos.value - transform.position * hit.distance, Color.yellow);
                //Debug.Log("Did Hit");
                return true;
            }

        }
        //Debug.DrawRay(transform.position, targetPos.value - transform.position * 1000, Color.white);
        //Debug.Log("Did not Hit");
        return false;
    }
}
