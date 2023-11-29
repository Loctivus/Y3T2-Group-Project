using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Visual Effect Event Channel")]
public class VisualEffectEventChannelSO : ScriptableObject
{
	public UnityAction<VisualEffectSO,GameObject> OnVFXCued;

	public void RaiseEventCue(VisualEffectSO visualID,GameObject sender)
	{
		if (OnVFXCued != null)
		{
			OnVFXCued.Invoke(visualID,sender);
		}
		else
		{
		}
	}

	public UnityAction<VisualEffectSO, GameObject> OnVFXCuedWithData;

	public void RaiseEventCueWithData(VisualEffectSO visualID, GameObject sender)
	{
		if (OnVFXCuedWithData != null)
		{
			OnVFXCuedWithData.Invoke(visualID, sender);
		}
		else
		{
		}
	}

}
