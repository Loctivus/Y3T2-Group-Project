using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyCorpse : Interactable
{
    [SerializeField] VoidEventChannelSO WeaponEvent;
    [SerializeField] UnityEvent HitCorpseEvent;
    [SerializeField] bool hasWeapon;
    [SerializeField] EntityRagdoll rag;
    public override void Interacted()
    {
        if (hasWeapon)
        {
            hasWeapon = false;
            WeaponEvent.RaiseEvent();
        }
        if(HitCorpseEvent != null)
            HitCorpseEvent.Invoke();
    }


    private void OnTriggerEnter(Collider collision)
    {
        if(collision.transform.gameObject.tag == "Enemy")
        {
            rag.KickRagdoll(new Vector3(collision.transform.position.x, transform.position.y -0.5f, collision.transform.position.z));
        }
    }
}
