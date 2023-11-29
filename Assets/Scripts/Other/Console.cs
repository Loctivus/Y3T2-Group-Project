using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Console : Interactable
{
    [SerializeField] int state = 0 ;
    [SerializeField] MeshRenderer[] mr;
    [SerializeField] UnityEvent breakActions;
    [SerializeField] UnityEvent unlockHitActions;
    [SerializeField] UnityEvent lockHitActions;
    private void Start()
    {
        ColourMeshes(Color.gray);
    }
    public override void Interacted()
    {
        base.Interacted();
        switch (state)
        {
            case 0:
                if (unlockHitActions != null)
                {
                    unlockHitActions.Invoke();
                }
                break;
            case 1:
                if (lockHitActions != null)
                {
                    lockHitActions.Invoke();
                }
                break;
        }

    }
    public void Break()
    {
        state = 2;
        UpdateColour();
        if (breakActions != null)
        {
            breakActions.Invoke();
        }
    }
    public void Unlock()
    {
        if (state != 2)
        {
            state = 0;
            UpdateColour();
            
        }
    }
    public void Lock() 
    {
        if(state != 2)
        {
            state = 1;
            UpdateColour();
        }
        
        
    }
    public void UpdateColour()
    {
        switch (state)
        {
            default:
                break;
            case 0:
                ColourMeshes(Color.gray);
                break;
            case 1:
                ColourMeshes(Color.red * 3.5f);
                break;
            case 2:
                ColourMeshes(Color.green * 3.5f);
                break;
        }
    }
    void ColourMeshes(Color clr)
    {
        for (int i = 0; i < mr.Length; i++)
        {
            Material mymat = mr[i].material;
            mymat.SetColor("_EmissionColor", clr);
        }
    }
}
