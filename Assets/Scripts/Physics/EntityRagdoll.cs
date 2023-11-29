using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class EntityRagdoll : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] List<Collider> ragColliders = new List<Collider>();
    [SerializeField] List<Rigidbody> ragRBs = new List<Rigidbody>();
    [SerializeField] Vector3Variable playerPosition;
    [SerializeField] float knockback;
    Animator anim;
    [SerializeField] List<Vector3> vec = new List<Vector3>();
    [SerializeField] List<Quaternion> rot = new List<Quaternion>();
    [SerializeField] GameObject offsetObject;
    [SerializeField] UnityEvent getUpAction;
    Coroutine resetCR;
    Vector3 offset;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        GetRagdollComps(transform, "Ragdoll RB", ragColliders);
        SavePose();
        
         
        EnableRagdoll(false);
    }

    void GetRagdollComps(Transform parent, string tag, List<Collider> cols)
    {
        /*
        //ragColliders = gameObject.GetComponentsInChildren<Collider>(true);
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);

            if (child.gameObject.CompareTag("Ragdoll RB"))
            {
                Collider col = child.GetComponent<Collider>();
                if (col != null)
                {
                    ragColliders.Add(col);
                    col.isTrigger = true;
                    var rb = child.gameObject.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.useGravity = false;
                        ragRBs.Add(rb);
                    }
                }
            }
        }
        */

        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);

            // Check if the child has the specified tag
            if (child.gameObject.CompareTag(tag))
            {
                // If it does, add its collider to the list
                Collider col = child.GetComponent<Collider>();
                if (col != null)
                {
                    ragColliders.Add(col);
                    col.isTrigger = true;
                    Rigidbody rb = child.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.useGravity = false;
                        ragRBs.Add(rb);
                    }

                }
            }

            // Call the function recursively on the child object
            GetRagdollComps(child, tag, cols);
        }
    }

    public void KnockBackRagdoll(float forceMultipler)
    {
        foreach (Rigidbody rb in ragRBs)
        {
            rb.AddForce(((rb.gameObject.transform.position - playerPosition.value).normalized + Vector3.up) * knockback * forceMultipler, ForceMode.VelocityChange);
        }
    }
    public void KickRagdoll(Vector3 position)
    {
        foreach (Rigidbody rb in ragRBs)
        {
            rb.AddForce(((rb.gameObject.transform.position - position).normalized + Vector3.up) * knockback/3, ForceMode.VelocityChange);
        }
    }

    public void SetRagdollMass()
    {
        foreach  (Rigidbody rb in ragRBs)
        {
            rb.mass = 0.05f;
        }
    }

    public void SetRagdollKnockback(float newKB)
    {
        knockback = newKB;
    }

    public void EnableRagdoll(bool enableRag)
    {
        anim.enabled = !enableRag;
        foreach (Collider col in ragColliders)
        {
            col.enabled = enableRag;
            col.isTrigger = !enableRag;
        }

        foreach (Rigidbody rb in ragRBs)
        {
            rb.useGravity = enableRag;
            rb.isKinematic = !enableRag;
        }
    }


    public void SavePose()
    {
        for (int i = 0; i < ragColliders.Count; i++)
        {
            Transform child = ragColliders[i].transform;
            vec.Add(child.localPosition);
            rot.Add(child.localRotation);
        }
    }

    public void StartResetPose()
    { 
        if (resetCR != null)
            StopCoroutine(resetCR);
        resetCR = StartCoroutine(ResetPoseOffCollision());
    }

    public void ResetPose()
    {
        Pose();
        //StartCoroutine(PoseLerp());
    }


    void Pose()
    {
        offset = offsetObject.transform.position;
        transform.position = offset;
        for (int i = 0; i < ragColliders.Count; i++)
        {
            //Debug.Log("saved child rot: " + children[i].rotation);
            ragColliders[i].transform.localPosition = vec[i];
            ragColliders[i].transform.localRotation = rot[i];
        }
    }

    IEnumerator PoseLerp()
    {
        float t = 0;
        int completetion = 0;
        List<bool> completed = new List<bool>();
        List<Quaternion> originalHitRotation = new List<Quaternion>();
        List<Vector3> originalHitVector= new List<Vector3>();
        
        for (int i = 0; i < ragColliders.Count; i++)
        {
            completed.Add(false);
            originalHitRotation.Add(ragColliders[i].transform.localRotation);
            originalHitVector.Add(ragColliders[i].transform.localPosition - transform.InverseTransformPoint(new Vector3(offsetObject.transform.position.x,1, offsetObject.transform.position.z)));
        }


        while (true)
        {
            t += Time.deltaTime * speed;
 
            if(completetion >= ragColliders.Count)
                    break;
            for (int i = 0; i < ragColliders.Count; i++)
            {
               
                if (!completed[i])
                {
                    if (Vector3.Distance(ragColliders[i].transform.localPosition, vec[i]) <= 0.1f)
                    {
                        completetion += 1;
                        completed[i] = true;
                    }
                        
                    //Debug.Log("saved child rot: " + children[i].rotation);
                    ragColliders[i].transform.localPosition = Vector3.Lerp(originalHitVector[i], vec[i], t);
                    ragColliders[i].transform.localRotation = Quaternion.Lerp(originalHitRotation[i], rot[i], t);
                }
                
            }
            yield return null;
        }
        Pose();
    }

    IEnumerator ResetPoseOffCollision()
    {
        Debug.Log("yess Get Up");
        float t = 0;
        while (true)
        {
            t += Time.deltaTime;
            if(t >= 5)
            {
                break;
            }
            if(Vector3.Distance(new Vector3(0,offsetObject.transform.position.y,0),new Vector3(0,0,0))<= 2.5f)
            {
                break;
            }
            yield return null;
        }
        yield return new WaitForSeconds(0.25f);
        if(getUpAction != null)
        {
            getUpAction.Invoke();
        }
    }
}
