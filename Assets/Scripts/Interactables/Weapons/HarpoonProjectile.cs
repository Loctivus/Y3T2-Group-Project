using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonProjectile : Weapon
{
    [SerializeField] Rigidbody rb;
    [SerializeField] SphereCollider col;
    public float launchForce;
    [SerializeField] bool inAir;
    Vector3 targetPos;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        Debug.Log("Spawn position is: " + transform.position);
        RaycastHit rayHit;
        
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        Debug.DrawRay(ray.origin, ray.direction * 20f, Color.cyan, 5f);
        if (Physics.Raycast(ray, out rayHit))
        {
            targetPos = rayHit.point;
            transform.LookAt(targetPos);
        }
        else
        {
            targetPos = ray.GetPoint(20f);
            transform.LookAt(targetPos);
        }

        rb.AddForce(transform.forward * launchForce, ForceMode.Impulse);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (inAir)
        {
            inAir = false;
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezeAll;

            if (collision.gameObject.tag == "Enemy")
            {
                punchActionsHit.Invoke();
                playerDamage.RuntimeValue = weaponDamage;
                hitObjectSO.RaiseEvent(collision.gameObject);
            }
        }

        col.enabled = false;
        MeshRenderer mr = GetComponentInChildren<MeshRenderer>();
        mr.enabled = false;

        Invoke("DestroyProjectile", 1f);
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
