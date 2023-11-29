using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


// Serialized class to hold Attack Events
[System.Serializable]
public class Attack_Events
{
    public UnityEvent OnEventRaised_InRange;
    public UnityEvent OnEventRaised_LeftRange;
}
// Serialized class to hold Attack Events
[System.Serializable]
public class Attack_Events_Attack
{
    public UnityEvent OnEventRaised_WindUP;
    public UnityEvent OnEventRaised_Attack;
    public UnityEvent OnEventRaised_WindDown;
}

public class Attacks : MonoBehaviour
{
    public Targeting targeting;
    [SerializeField] bool windUP;
    [SerializeField] bool activate;
    [SerializeField] float range;
    [SerializeField] bool canAttack = false;
    [SerializeField] bool inrange = false;

    public Attack_Events_Attack atkEvents;
    public Attack_Events events;

    private void FixedUpdate()
    {
        //if target is detected inrange set can attack true and invoke inrange actions
        // actions invoked are set in inspector on the object
        if (targeting.inRange(range))
        {
            if (!inrange)
            {
                if(events.OnEventRaised_InRange != null)
                    events.OnEventRaised_InRange.Invoke();
            }
            inrange = true;
            canAttack = true;
        }
        //if object was in range and now no longer is invoke all leaving range actions
        //reset canAttack
        else
        {
            if (inrange)
            {
                if (events.OnEventRaised_LeftRange != null)
                    events.OnEventRaised_LeftRange.Invoke();
            }
            inrange = false;
            canAttack = false;
        }
    }

    // function used in animations to signify when an entity may attack again
    // used when animations arent required to entirely finish before object is ready to attack
    public void ResetAttacks()
    {       
        Debug.Log("Reset");
        canAttack = true;
    }


    //also used in animations as an animation event to invoke attack actions
    public void Attack()
    {
        if (atkEvents.OnEventRaised_Attack != null)
            atkEvents.OnEventRaised_Attack.Invoke();
    }

    //also used as an animation event to invoke actions
    public void WindDown()
    {
 
        if (atkEvents.OnEventRaised_WindDown != null)
            atkEvents.OnEventRaised_WindDown.Invoke();
    }

    //fired on objects to start attack cycle when the physics handler ends movement
    public void RequestAttack()
    {
        if (canAttack)
        {
            // if object has special windup logic invoke actions instead of attacks
            if (windUP)
            {
                canAttack = false;
                if (atkEvents.OnEventRaised_WindUP != null)
                    atkEvents.OnEventRaised_WindUP.Invoke();
            }
            else
            {
                canAttack = false;
                if (atkEvents.OnEventRaised_Attack != null)
                    atkEvents.OnEventRaised_Attack.Invoke();
            }
           
        }
    }
}
