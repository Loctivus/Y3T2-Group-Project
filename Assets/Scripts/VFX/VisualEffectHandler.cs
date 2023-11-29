using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VisualEffectHandler : MonoBehaviour
{
    public VisualEffect[] allEffects;
    public VisualEffectSO[] allTriggerNames;
    public VisualEffectEventChannelSO visualEffectChannel;

    private void OnEnable()
    {
        visualEffectChannel.OnVFXCued += PlayRequested;
        visualEffectChannel.OnVFXCuedWithData += GlobalPlayRequestedWithPosition;
    }
    private void OnDisable()
    {
        visualEffectChannel.OnVFXCued -= PlayRequested;
        visualEffectChannel.OnVFXCuedWithData -= GlobalPlayRequestedWithPosition;
    }

    void PlayRequested(VisualEffectSO visualID, GameObject sender)
    {
        if (sender != gameObject || allEffects.Length<1 || allTriggerNames.Length < 1)
            return;
        for (int i = 0; i < allTriggerNames.Length; i++)
        {
            if(allTriggerNames[i].visualEffectName == visualID.visualEffectName)
            {
                allEffects[i].Play();
            }
        }
    }

    void PlayRequestedWithData(VisualEffectSO visualID,GameObject sender, VisualEffectRequestData data)
    {
        if (sender != gameObject || allEffects.Length < 1 || allTriggerNames.Length < 1)
            return;
        for (int i = 0; i < allTriggerNames.Length; i++)
        {
            if (allTriggerNames[i].visualEffectName == visualID.visualEffectName)
            {
                if(visualID.visualType == VisualEffectSO.dataType.position)
                {
                    allEffects[i].transform.position = data.position;
                }              
                allEffects[i].Play();
            }
        }
    }

    public void PlayActionedVisualEffect(VisualEffectSO visualID)
    {
        
        for (int i = 0; i < allTriggerNames.Length; i++)
        {
            if (allTriggerNames[i].visualEffectName == visualID.visualEffectName)
            {
                Debug.Log("Actioned");
                allEffects[i].Play();
            }
        }
    }

    void GlobalPlayRequestedWithPosition(VisualEffectSO visualID, GameObject sender)
    {
        if (allEffects.Length < 1 || allTriggerNames.Length < 1)
            return;
        for (int i = 0; i < allTriggerNames.Length; i++)
        {
            if (allTriggerNames[i].visualEffectName == visualID.visualEffectName)
            {
                allEffects[i].transform.position = sender.transform.position;

                allEffects[i].Play();
            }
        }
    }
}
