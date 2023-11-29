using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    public float weaponDamage;
    public FloatVariable playerDamage;
    [SerializeField] float durabilityMax;
    float durability;
    [SerializeField] float attackRange;
    [SerializeField] float comboCount;
    [SerializeField] UnityEvent punchActionsMiss;
    public UnityEvent punchActionsHit;
    [SerializeField] AudioClip[] regularClips;
    [SerializeField] AudioClip[] succesfulClips;
    public VoidEventChannelSO successfulHit;
    public  GameObjectEventChannelSO hitObjectSO;
    [SerializeField] PlayerMoveRB movement;

    [Header("Durability SOs for UI Reference")]
    public VoidEventChannelSO uIClearMaskEvent;
    public FloatVariable uIMaxDurability;
    public FloatVariable uICurrentDurability;

    [HideInInspector] public AudioSource audioSource;
    [HideInInspector] public Animator anim;
    bool attacking;
    bool hitTarget;
    [HideInInspector] public GameObject hitObject;
    Coroutine punching;

    [SerializeField] BrokeEvents brokeActions;
    [SerializeField] PickUpEvents pickUpActions;
    [SerializeField] DropEvents dropActions;    

    private void OnEnable()
    {
        PickUp();
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Attack();
        if (Input.GetMouseButtonDown(1))
            SpecialAttack();
    }

    public void Attack()
    {
        if (!attacking)
        {
            attacking = true;
            playerDamage.RuntimeValue = weaponDamage;
            StartCoroutine(AttackingCooldown());
            
            if (HitRay())
            {
                successfulHit.RaiseEvent();
                if (punching != null)
                    StopCoroutine(punching);
                punching = StartCoroutine(DelayPunch());
            }
            else
            {
                anim.SetTrigger("Attack");
            }
        } 
    }

    public virtual void SpecialAttack()
    {
        Debug.Log("special attack executed");
    }

    public void LoseDurability()
    {
        if (durabilityMax < 0)
            return;
        durability -= 1;
        uICurrentDurability.RuntimeValue = durability;
        if(durability <= 0)
        {
            WeaponBroke();
        }
    }

    public void WeaponBroke()
    {
        if(brokeActions.OnEventRaised_Broke != null)
        {
            if (uIClearMaskEvent != null)
            {
                uIClearMaskEvent.RaiseEvent();
            }
            brokeActions.OnEventRaised_Broke.Invoke();
            
        }
    }

    public void DropWeapon()
    {
        if (dropActions.OnEventRaised_Drop != null)
        {
            dropActions.OnEventRaised_Drop.Invoke();

        }
    }

    public void PickUp()
    {
        if (pickUpActions.OnEventRaised_PickUp != null)
        {
            pickUpActions.OnEventRaised_PickUp.Invoke();

        }
        uIMaxDurability.RuntimeValue = durabilityMax;
        uICurrentDurability.RuntimeValue = durabilityMax;
        durability = durabilityMax;
        playerDamage.RuntimeValue = weaponDamage;
        attacking = false;
    }


    IEnumerator DelayPunch()
    {
        yield return new WaitForSeconds(0.1f);
        anim.SetTrigger("Attack");
    }
    bool HitRay()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 12;
        RaycastHit hit;
        hitTarget = false;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position-transform.forward, transform.forward, out hit, attackRange,layerMask))
        {
            Debug.DrawLine(transform.position, transform.forward * attackRange);
            if (hit.collider.tag == "Enemy")
            {
                hitObject = hit.collider.gameObject;
                movement.hitTargetVector3 = hit.collider.gameObject.transform.position;
                hitTarget = true;
                return true;
            }

        }
        hitTarget = false;
        movement.hitTargetVector3 = transform.position + transform.forward;
        return false;
    }


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if(audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        anim = GetComponent<Animator>();
        if (anim == null)
        {
            anim = gameObject.AddComponent<Animator>();
        }
    }
    public void PunchAnimationEvent()
    {
        if(punchActionsMiss != null && !hitTarget)
        {
            punchActionsMiss.Invoke();
        }
        if (punchActionsHit != null && hitTarget)
        {
            hitTarget = false;
            hitObjectSO.RaiseEvent(hitObject.transform.gameObject);
            punchActionsHit.Invoke();
        }
    }

    public void PlaySound(bool miss)
    {
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        if(miss)
            audioSource.PlayOneShot(regularClips[Random.Range(0, regularClips.Length - 1)]);
        else
            audioSource.PlayOneShot(succesfulClips[Random.Range(0, succesfulClips.Length - 1)]);
    }

    IEnumerator AttackingCooldown()
    {
        yield return new WaitForSeconds(0.1f);
        attacking = false;
    }

}

[System.Serializable]
public class BrokeEvents
{
    public UnityEvent OnEventRaised_Broke;
}

[System.Serializable]
public class PickUpEvents
{
    public UnityEvent OnEventRaised_PickUp;
}

[System.Serializable]
public class DropEvents
{
    public UnityEvent OnEventRaised_Drop;
}