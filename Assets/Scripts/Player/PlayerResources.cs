using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerResources : MonoBehaviour
{
    public FloatVariable playerHealth;
    public BoolVariable playerDead;
    //public FloatVariable playerHealthMax;
    public bool dmgCD;
    public VoidEventChannelSO playerDied;
    bool dead;
    [SerializeField] FloatVariable playerWeaponValue;
    [SerializeField] BoolVariable flagParry;
    [SerializeField] FloatEventChannelSO damagedEventChannelSO;
    [SerializeField] VoidEventChannelSO globalhit;
    [SerializeField] VoidEventChannelSO hitUI;
    [SerializeField] VoidEventChannelSO fadeFromBlack;

    [SerializeField] GameObject[] weapons;
    public UnityEvent damagedEvents;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        fadeFromBlack.RaiseEvent();
    }

    private void Start()
    {
        playerHealth.RuntimeValue = playerHealth.InitialValue;
        flagParry.RuntimeValue = flagParry.InitialValue;
    }

    private void OnEnable()
    {
        damagedEventChannelSO.OnEventRaised += Damaged;
    }

    private void OnDisable()
    {
        damagedEventChannelSO.OnEventRaised -= Damaged;
    }

    public void Damaged(float value)
    {
        //if (damagedEvents != null)
            //damagedEvents.Invoke();
        playerHealth.RuntimeValue -= value;
        hitUI.RaiseEvent();
        
        if(playerHealth.RuntimeValue <= 0)
        {
            playerDead.RuntimeValue = true;
            playerDied.RaiseEvent();
        }
    }

    private void Update()
    {
        /*
        if(playerHealth.RuntimeValue > 0)
        {
            playerHealth.RuntimeValue -= 0.25f * Time.deltaTime;
        }
        */

        /*
        if (playerHealth.RuntimeValue <= 0 && ! dead)
        {
            dead = true;
            playerDied.RaiseEvent();
        }
        */
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Damage" && dmgCD == false)
        {
            playerHealth.RuntimeValue -= 30;
            if (playerHealth.RuntimeValue <= 0)
            {
                playerDead.RuntimeValue = true;
                playerDied.RaiseEvent();
                Time.timeScale = 0f;
            }
            dmgCD = true;
            StartCoroutine(DamageCD());

        }
    }


    IEnumerator DamageCD()
    {
        yield return new WaitForSeconds(0.25f);
        dmgCD = false; 
    }

 

}
