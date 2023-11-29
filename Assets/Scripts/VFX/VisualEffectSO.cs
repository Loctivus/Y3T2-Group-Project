using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[CreateAssetMenu(menuName = "Visual Effect/Visual Effect Object")]
public class VisualEffectSO : ScriptableObject
{
    public string visualEffectName;
    public VisualEffectAsset visualEffect;
    public dataType visualType;
    public enum dataType
    {
        empty,
        position,
        rotation,
        scale,
        fullTransform,
        animator
    }
}
