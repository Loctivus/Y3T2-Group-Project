using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialColliderScript : MonoBehaviour
{
    public GameObject tutorialScreen;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            tutorialScreen.SetActive(true);
        }
     }
    private void OnTriggerExit(Collider other)
    {
        tutorialScreen.SetActive(false);
    }
}
