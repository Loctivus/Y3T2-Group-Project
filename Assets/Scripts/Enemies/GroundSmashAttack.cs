using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSmashAttack : Attacks
{
    [SerializeField] float range;
    [SerializeField] float camShakeRange;
    /*
    private void Update()
    {
        if (canAttack())
        {
            attacking = true;
            StartCoroutine(WindUp());
        }
    }

    public override IEnumerator Attack()
    {
        if (atkEvents.OnEventRaised_Attack != null)
            atkEvents.OnEventRaised_Attack.Invoke();
        yield return new WaitForSeconds(attackTime);
        StartCoroutine(WindDown());
    }

    public override IEnumerator WindUp()
    {
        if (atkEvents.OnEventRaised_WindUP != null)
            atkEvents.OnEventRaised_WindUP.Invoke();
        yield return new WaitForSeconds(windUpTime);
        StartCoroutine(Attack());
    }
    public override IEnumerator WindDown()
    {
        if (atkEvents.OnEventRaised_WindDown != null)
            atkEvents.OnEventRaised_WindDown.Invoke();
        yield return new WaitForSeconds(windDownTime);
        StartCoroutine(Cooldown());
        attacking = false;

    }

    public override bool canAttack()
    {

        if (!attacking && !inCD && !lockedAttack)
        {
            if (targeting.inRange(range))
            {
                return true;
            }
        }

        return false;
    }*/
}
