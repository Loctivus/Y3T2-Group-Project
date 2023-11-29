using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHighlightSelect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float startPos;
    public float highPos;
    public float tweenTime;
    public float exitTime;
    public AudioSource audioSource;

    public void OnPointerEnter(PointerEventData eventData)
    {
        LeanTween.moveLocalX(gameObject, highPos, tweenTime).setIgnoreTimeScale(true); 
        audioSource.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.moveLocalX(gameObject, startPos, tweenTime).setIgnoreTimeScale(true); 
    }
    public void exitButton()
    {
        LeanTween.moveLocalX(gameObject, startPos, exitTime).setIgnoreTimeScale(true); 
    }
}
