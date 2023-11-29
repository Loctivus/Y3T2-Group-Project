using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TempHealthUI : MonoBehaviour
{
    [SerializeField] FloatVariable health;
    [SerializeField] Slider healthBar;

    private void Awake()
    {
        healthBar = GetComponent<Slider>();
        healthBar.maxValue = health.InitialValue;
    }

    void Update()
    {
        healthBar.value = health.RuntimeValue;
    }
}
