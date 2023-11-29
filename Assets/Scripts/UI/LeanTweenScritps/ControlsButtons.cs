using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ControlsButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public float tweenTime;
	//public float highlightLocation;
	//public float startLocation;
	public Vector2 highlightScale;
	public Vector2 startScale;
	public AudioSource audioSource;

	public void OnPointerEnter(PointerEventData eventData)
	{
		audioSource.Play();
		LeanTween.scale(gameObject, highlightScale, tweenTime).setIgnoreTimeScale(true);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		LeanTween.scale(gameObject, startScale, tweenTime).setIgnoreTimeScale(true);
	}
}
