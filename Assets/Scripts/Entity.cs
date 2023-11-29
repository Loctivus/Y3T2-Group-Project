using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Entity : MonoBehaviour
{
    [SerializeField] GameObjectEventChannelSO stunnedPostureEventChannelSO;
    [SerializeField] GameObjectEventChannelSO regainPostureEventChannelSO;
    [SerializeField] GameObjectEventChannelSO_Vector3 knockbackEventChannelSO;
    [SerializeField] GameObjectEventChannelSO deathEventChannelSO;
    [SerializeField] SkinnedMeshRenderer entityRenderer;
    [SerializeField] FloatVariable playerDamage;
    [SerializeField] float enemyHealth = 100;
    [SerializeField] float deathDelay;
    float maxPoise = 1;
    float poiseValue = 1;
    public DeathEvents deathEvent;
    public RagdollEvents ragdollEvent;
    public ResetRagdollEvents resetRagdollEvent;
    Coroutine stunCR;

    public void RegainPosture()
    {
        if(resetRagdollEvent.OnEventRaised_ResetRagdoll != null)
        {
            resetRagdollEvent.OnEventRaised_ResetRagdoll.Invoke();
            poiseValue = maxPoise;
        }
    }

    public void Damaged()
    {
        enemyHealth -= playerDamage.RuntimeValue;
        poiseValue -= 100;
        if(enemyHealth <= 0)
        {
            if (deathEvent.OnEventRaised_Death != null)
                deathEvent.OnEventRaised_Death.Invoke();
            //deathEventChannelSO.RaiseEvent(gameObject);
        }
        else
        {
            if(poiseValue <= 0)
            {
                if(ragdollEvent.OnEventRaised_Ragdoll != null)
                {
                    ragdollEvent.OnEventRaised_Ragdoll.Invoke();
                    if (stunCR != null)
                        StopCoroutine(stunCR);
                    stunCR = StartCoroutine(StunnedCD());
                }
            }
        }
    }

    public void KnockBack(Vector3 v3)
    {
        knockbackEventChannelSO.RaiseEvent(gameObject, v3);
    }

    IEnumerator StunnedCD()
    {
        yield return new WaitForSeconds(1f);
        RegainPosture();
    }
    public void DeleteObject()
    {
        StartCoroutine(DeathDelay());
    }   
    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(deathDelay);
        Destroy(gameObject);
    }
}

[System.Serializable]
public class DeathEvents
{
    public UnityEvent OnEventRaised_Death;
}
[System.Serializable]
public class RagdollEvents
{
    public UnityEvent OnEventRaised_Ragdoll;
}
[System.Serializable]
public class ResetRagdollEvents
{
    public UnityEvent OnEventRaised_ResetRagdoll;
}