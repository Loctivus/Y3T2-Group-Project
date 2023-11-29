using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    [SerializeField] Animator anim;
    string s;
    [SerializeField] float animationDelay = 0.5f;
    private void Start()
    {
        float randomDelay = Random.Range(1-animationDelay, 1 + animationDelay);
        anim.speed = randomDelay;
    }
    public void SetString(string ss)
    {
        s = ss;
    }
    public void PlayAnimationTrigger()
    {
        anim.SetTrigger(s);
    }
    public void PlayAnimationBool(bool b)
    {
        anim.SetBool(s,b);
    }

}
