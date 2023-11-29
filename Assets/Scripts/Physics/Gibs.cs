using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gibs : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float knockBack;

    private void OnEnable()
    {
        Destroy(gameObject, 15);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.gameObject.tag == "Enemy")
        {
            rb.AddForce(transform.position - new Vector3(collision.transform.position.x, transform.position.y - 0.5f, collision.transform.position.z) * knockBack,ForceMode.VelocityChange);
        }
    }
}
