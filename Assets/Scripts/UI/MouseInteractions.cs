using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MouseInteractions : MonoBehaviour
{
    [SerializeField] MeshRenderer mesh;
    [SerializeField] Material selected;
    [SerializeField] Material unSelected;

    public UnityEvent OnEventRaised;

    private void OnMouseOver()
    {
        
    }
    private void OnMouseEnter()
    {
        mesh.material = selected;
    }
    private void OnMouseExit()
    {
        mesh.material = unSelected;
    }

    private void OnMouseDown()
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke();
    }

    
}
