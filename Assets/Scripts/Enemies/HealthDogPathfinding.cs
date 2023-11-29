using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealthDogPathfinding : MonoBehaviour
{
    [SerializeField] NavMeshAgent nmAgent;
    public Vector3Variable playerPosition;
    public float runDistance;
    [SerializeField] bool active;
    [SerializeField] Animator anim;

    private void Awake()
    {
        
    }

    
    void Update()
    {
        if (Vector3.Distance(transform.position, playerPosition.value) < runDistance && active)
        {
            anim.SetFloat("Speed", 1f);
            Vector3 directionToPlayer = transform.position - playerPosition.value;
            Vector3 destination = transform.position + directionToPlayer.normalized * runDistance;
            NavMeshPath path = new NavMeshPath();
            NavMeshHit hit;
            NavMesh.SamplePosition(destination, out hit, 10f, NavMesh.AllAreas);
            NavMesh.CalculatePath(transform.position, hit.position, NavMesh.AllAreas, path);
            if (path.status != NavMeshPathStatus.PathInvalid)
            {
                nmAgent.SetDestination(hit.position);
            }
            else
            {
                nmAgent.SetDestination(transform.position);
            }
        }
        else
        {
            nmAgent.SetDestination(transform.position);
            anim.SetFloat("Speed", 0.05f);
        }
       
    }


    public void ToggleActive()
    {
        if (active)
        {
            active = false;
        }
        else
        {
            active = true;
        }
    }
}
