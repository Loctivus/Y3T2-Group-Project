using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MMButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float tweenTime;
    public Vector2 highlightLocation;
    public Vector2 startLocation;
    public AudioSource audioSource;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("MouseEnter");
        audioSource.Play();
        LeanTween.move(gameObject, highlightLocation, tweenTime).setEaseOutSine().setIgnoreTimeScale(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("MouseExit");
        LeanTween.cancel(gameObject);
        LeanTween.move(gameObject, startLocation, tweenTime).setEaseInSine().setIgnoreTimeScale(true);   
    }
}
