using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealthEnemy : Interactable
{
    public float healthGain;
    public FloatVariable playerHealth;
    [SerializeField] SkinnedMeshRenderer smr;
    bool used;

    public override void Interacted()
    {
        base.Interacted();
        if (!used)
        {
            playerHealth.RuntimeValue += healthGain;
            if (playerHealth.RuntimeValue > playerHealth.InitialValue)
            {
                playerHealth.RuntimeValue = playerHealth.InitialValue;
            }
            used = true;

        }
    }

    public void ToggleSkin()
    {
        smr.enabled = false;
    }
}
