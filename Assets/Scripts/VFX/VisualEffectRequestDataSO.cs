using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VisualEffectRequestData
{
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

    public Vector3 position;


}
