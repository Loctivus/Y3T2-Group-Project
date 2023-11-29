using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoomDoor : Interactable
{
    [SerializeField] Animator anim;
    public Color lockedColour;
    public Color unlockedColour;
    public AudioClip lockedSfx;
    public AudioClip unlockedSfx;
    [SerializeField] bool locked;
    [SerializeField] UnityEvent OnEventRaisedUnlocked;
    [SerializeField] UnityEvent OnEventRaisedLocked;
    [SerializeField] FloatEventChannelSO doorCountEvent;
    [SerializeField] int enemyCount;
    [SerializeField] float doorID;
    [SerializeField] AudioSource audioSource;

    private void OnEnable()
    {
        doorCountEvent.OnEventRaised += AddToDoor;
        ToggleDoorColour(lockedColour);
    }
    private void OnDisable()
    {
        doorCountEvent.OnEventRaised -= AddToDoor;
    }
    void AddToDoor(float iD)
    {
        if(iD == doorID)
        {
            enemyCount -= 1;
            if (enemyCount <= 0)
            {
                locked = false;
                ToggleDoorColour(unlockedColour);
            }
        }

    }

    void ToggleDoorColour(Color colour)
    {
        MeshRenderer[] mrS = GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer mr in mrS)
        {
            Material mat = mr.material;
            mat.SetColor("_EmissionColor", colour);
        }
    }

    // if door is interacted with and not locked open dorr and invoke actions
    public override void Interacted()
    {
        if (!locked)
        {
            base.Interacted();
            anim.SetTrigger("Open");
            if(OnEventRaisedUnlocked != null)
            {
                audioSource.clip = unlockedSfx;
                OnEventRaisedUnlocked.Invoke();
            }
        }
        else
        {
            if (OnEventRaisedLocked != null)
            {
                audioSource.clip = lockedSfx; 
                OnEventRaisedLocked.Invoke();
                
            }
        }
    }
}
