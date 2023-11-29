using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshSwitcher : MonoBehaviour
{
    [SerializeField] GameObject baseMesh;
    [SerializeField] GameObject dmgMesh;
    public void SwitchMesh()
    {
        baseMesh.SetActive(false);
        dmgMesh.SetActive(true);
    }
}
