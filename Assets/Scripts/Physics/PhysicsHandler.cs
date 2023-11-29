using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PhysicsHandler : MonoBehaviour
{
    [SerializeField] bool activate;
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed;
    float tiredCoeff = 1;
    [SerializeField] float maxSpeed;
    [SerializeField] float drag;
    [SerializeField] float turnSpeed;
    [SerializeField] float angularDrag;
    [SerializeField] float chaseRange;
    [SerializeField] float knockback;
    //temp
    [SerializeField] Vector3Variable playerPosition;
    Vector3 target;
    [SerializeField] NavMeshPathfinding AIBrain;
    Vector3 currentContactPoint;
    bool noDrag = false;
    [SerializeField] bool canMove = false;
    bool canMoveAll = true;
    [SerializeField] float dashDistance;
    [SerializeField] float dashSpeed;
    [SerializeField] Animator anim;
    [SerializeField] Attacks entitiesATK;

    private void Start()
    {
        currentContactPoint = transform.forward;
    }
    private void FixedUpdate()
    {
        if (activate)
        {
            if (canMoveAll)
            {
                if (canMove)
                {
                    //Debug.Log("Can Move");
                    Move();
                }

                Turn();
            }
            if (!noDrag)
            {
                Drag();
                //AngularDrag();
            }
        }


    }

    private void Update()
    {
        if (activate && canMove)
        {
            anim.SetFloat("Speed", rb.velocity.magnitude);
            //if (AIBrain.CheckPath())
            target = AIBrain.GetNextTargetPoint();
        }

    }

    public void Activate()
    {
        activate = true;
        canMove = true;
    }

    void Move()
    {
        if (Vector3.Distance(playerPosition.value, transform.position) > chaseRange)
        {
            if (rb.velocity.magnitude < maxSpeed)
            {
               // Debug.Log("Im Zooming");
                rb.AddForce((target + AvoidanceOffset(target, 0.05f)).normalized * (speed/tiredCoeff), ForceMode.Force);
                //rb.AddForce(transform.forward * speed, ForceMode.Force);
            }

        }
        else
        {
            entitiesATK.RequestAttack();
            RepelFrom();
        }

    }


    Vector3 AvoidanceOffset(Vector3 target, float speedFac)
    {
        float separateSpeed = speed * speedFac;
        float separateRadius = 2f;
        Vector3 sum = Vector3.zero;
        float count = 0f;

        var hits = Physics.OverlapSphere(transform.position, separateRadius);
        foreach (var hit in hits)
        {
            if (hit.tag == "Enemy" && hit.transform != transform)
            {
                Vector3 difference = transform.position - hit.transform.position;

                Vector3 scaledDifference = (difference.normalized / (Mathf.Abs(difference.magnitude) != 0 ? Mathf.Abs(difference.magnitude) : 1));
                //Debug.DrawRay(transform.position, difference.normalized *10, Color.magenta, 0);
                //Debug.DrawRay(transform.position, scaledDifference, Color.magenta, 0);
                sum += scaledDifference;
                count++;
            }
        }
        //Debug.Log(sum);
        Debug.DrawRay(transform.position, sum, Color.red, 0);
        if (count > 0)
        {
            sum /= count;
            sum = sum.normalized * separateSpeed;

            return sum;
        }
        return Vector3.zero;
    }




    void Turn()
    {

        // Find Axis that will take us from current rotation to Identity
        Vector3 currentRotation = rb.rotation.eulerAngles;
        Vector3 deltaAngle = currentRotation - Quaternion.LookRotation(target).eulerAngles;
        Vector3 deltaAngleWrapped = new Vector3(Mathf.DeltaAngle(deltaAngle.x, 0.0f), Mathf.DeltaAngle(deltaAngle.y, 0.0f), Mathf.DeltaAngle(deltaAngle.z, 0.0f));
        Vector3 velocityProjected = Vector3.Project(rb.angularVelocity, deltaAngleWrapped);
        Vector3 torque = deltaAngleWrapped - velocityProjected;
        torque = torque * angularDrag;
        rb.AddTorque(torque);

        /*
        // Calculate the target direction without the up or down axis
        //Vector3 targetDirection = (target) - transform.position;
        Vector3 targetDirection = target + AvoidanceOffset(target, 0.015f);
        //targetDirection = target - transform.position;
        targetDirection.y = 0.0f;

        // Rotate the Rigidbody towards the target direction using torque
        Vector3 cross = Vector3.Cross(transform.forward, targetDirection);
        rb.AddTorque(cross * turnSpeed, ForceMode.Force);
        */
    }

    void Drag()
    {
        rb.AddForce(-rb.velocity * rb.velocity.magnitude * drag, ForceMode.Force);
    }

    void AngularDrag()
    {
        rb.AddTorque(-rb.angularVelocity * angularDrag, ForceMode.Force);
    }

    void RepelFrom()
    {
        // if (rb.velocity.magnitude < maxSpeed/2)
        //    rb.AddForce(-transform.forward * speed/2, ForceMode.Force);
    }

    public void KnockBack()
    {
        rb.AddForce(((transform.position - playerPosition.value).normalized + Vector3.up) * knockback, ForceMode.VelocityChange);
    }

    public void ToggleMovement(bool b)
    {
        canMove = b;
    }

    public void ToggleAllMovement(bool b)
    {
        canMoveAll = b;
    }

    public void LinearAttack()
    {
        StopAllCoroutines();
        StartCoroutine(Dash());
    }

    public void Ragdoll()
    {
        rb.constraints = RigidbodyConstraints.None;
        canMoveAll = false;
    }

    public void RevertRagdoll()
    {
        rb.constraints = RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        canMoveAll = true;
    }
    public void ToggleRotation(bool b)
    {
        if (!b)
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        else
        {
            rb.constraints = RigidbodyConstraints.None;
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }

    }
    public void SlowMovement()
    {
        StopAllCoroutines();
        StartCoroutine(Slow());
    }
    public void KillMomentum()
    {
        StopAllCoroutines();
        rb.velocity = Vector3.zero;
    }
    IEnumerator Dash()
    {
        float t = dashDistance;
        while (t > 0)
        {
            t -= Time.deltaTime;
            if (rb.velocity.magnitude < maxSpeed * 2)
                rb.AddForce(transform.forward * dashSpeed, ForceMode.Force);
            yield return null;
        }
    }

    IEnumerator Slow()
    {
        rb.velocity = Vector3.zero;
        tiredCoeff = 2;
        yield return new WaitForSeconds(1f);
        tiredCoeff = 1;
    }

    public IEnumerator ActivateAfter()
    {
        yield return new WaitForSeconds(1f);
        activate = true;
    }
}
