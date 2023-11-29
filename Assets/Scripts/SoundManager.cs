using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioClip[] clips;
    [SerializeField] AudioSource audioSource;

    public void PlayClip()
    {
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.PlayOneShot(clips[Random.Range(0, clips.Length - 1)]);
    }
}
