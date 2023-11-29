using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelSelect : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private AudioSource highlightAudio;
    public void OnPointerEnter(PointerEventData eventData)
    {
        highlightAudio.Play();
    }
}
