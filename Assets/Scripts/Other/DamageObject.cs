using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObject : MonoBehaviour
{
    [SerializeField] Collider myCol;
    [SerializeField] FloatEventChannelSO dmgEvent;
    [SerializeField] GameObjectEventChannelSO hitPlayer;
    [SerializeField] float dmg;
    [SerializeField] GameObject alien;
    [SerializeField] BoolVariable parryFlag;
    [SerializeField] Collider gamePlayerCol;
    [SerializeField] bool hit = false;


    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Player")
        {
            //Debug.Log("Hit");
            if (!parryFlag.RuntimeValue && !hit)
            {
                Debug.Log("Hit player");
                hit = true;
                //gamePlayerCol.enabled = false;
                myCol.enabled = false;
                dmgEvent.RaiseEvent(dmg);
                hitPlayer.RaiseEvent(alien);
                parryFlag.RuntimeValue = false;
                StopAllCoroutines();
                StartCoroutine(LayerCollision());
            }

        }
    }



    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if (!parryFlag.RuntimeValue && !hit)
            {

                Debug.Log("Hit");
                Debug.Log("Hit player");
                hit = true;
                //gamePlayerCol.enabled = false;
                myCol.enabled = false;
                dmgEvent.RaiseEvent(dmg);
                hitPlayer.RaiseEvent(alien);
                parryFlag.RuntimeValue = false;
                StopAllCoroutines();
                StartCoroutine(LayerCollision());
            }
            
        }
    }


    IEnumerator LayerCollision()
    {
        yield return new WaitForSeconds(0.5f);
        gamePlayerCol.enabled = true;
        hit = false;
    }
    
}
