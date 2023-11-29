using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshPathfinding : MonoBehaviour
{
    //[SerializeField] Transform target;
    public Vector3Variable targetPos;
    //public bool inChase;
    Rigidbody rb;
    public bool activate = false;


    public NavMeshPath movePath;
    float pathCalcSpacer = 0.1f;
    int currentCornerIndex = 0;
    public float reachLength = 0.1f;
    public float chaseForce;
    public float maxChaseForce;
    public NavMeshAgent navAgent;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        //movePath = new NavMeshPath();

        //StartCoroutine(PathRecalculation());

        navAgent = GetComponent<NavMeshAgent>();
        navAgent.updatePosition = false;
        navAgent.updateRotation = false;
    }


    /*
    public void MoveOnPath()
    {
        if (currentCornerIndex >= movePath.corners.Length)
        {
            rb.velocity = Vector3.zero;
            return;
        }

        Vector3 nextCorner = GetNextPathPos(movePath,0);

        if (Vector3.Distance(transform.position, nextCorner) < reachLength)
        {
            currentCornerIndex++;
        }
        else
        {
            Vector3 moveDir = (nextCorner - transform.position).normalized;
            rb.MovePosition(transform.position + moveDir * Time.deltaTime);
        }


    }

    */

    public Vector3 GetNextPathPos(NavMeshPath currentPath,int i)
    {
        Vector3 nextPos = currentPath.corners[0];
        if(currentPath.corners.Length >= 3)
        {
            nextPos = currentPath.corners[1+i];
        }
        else if (currentPath.corners.Length >= 2)
        {
            nextPos = currentPath.corners[1];
        }

        return nextPos;
    }
    IEnumerator Recalculation()
    {

            if (NavMesh.CalculatePath(transform.position, targetPos.value, NavMesh.AllAreas, movePath))
            {
                for (int i = 0; i < movePath.corners.Length - 1; i++)
                {

                    Debug.DrawLine(movePath.corners[i], movePath.corners[i + 1], Color.blue);
                }
                currentCornerIndex = 0;

                //yield return null;

            }
            else
            {
                rb.velocity = Vector3.zero;

            }
        yield return new WaitForSeconds(pathCalcSpacer);
        //StartCoroutine(PathRecalculation());
    }
    public bool CheckPath()
    {
        if (movePath.status == NavMeshPathStatus.PathInvalid)
        {
            return false;
        }
        else
        {
            return true;
        }

    }
    public Vector3 GetNextTargetPoint()
    {
        Debug.Log("Cal" + gameObject.name);
        navAgent.nextPosition = transform.position;
        navAgent.SetDestination(targetPos.value);
        return navAgent.velocity;
        /*
        if(Vector3.Distance(transform.position,GetNextPathPos(movePath,0))< 1f)
        {
            return GetNextPathPos(movePath, 1);
        }
        return GetNextPathPos(movePath,0);
        */
    }


    public void TogglePathfinding(bool b)
    {
        activate = b;
    }    
    
   /* private void OnDrawGizmos()
    {
        for (int i = 0; i < movePath.corners.Length; i++)
        {
            Gizmos.DrawCube(movePath.corners[i], Vector3.one/10);
        }
    }*/
    
}
