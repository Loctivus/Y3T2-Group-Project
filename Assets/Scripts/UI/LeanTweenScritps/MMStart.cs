using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMStart : MonoBehaviour
{
    public float tweenTime;
    
    //public Vector2 hiddenLocation;
    // Start is called before the first frame update
    public void Start()
    {
        LeanTween.cancel(gameObject);
        LeanTween.moveX(gameObject, 0, tweenTime).setEaseOutBack().setIgnoreTimeScale(true);
    }
    public void Hide()
    {
        var hidLoc = -Screen.width + Screen.width /2;
        LeanTween.cancel(gameObject);
        LeanTween.moveX(gameObject, hidLoc, tweenTime).setEaseInBack().setIgnoreTimeScale(true);

    }
}
