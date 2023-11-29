using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetMouseSensitivity : MonoBehaviour
{
    public FloatVariable mouseSensitivity;
    Slider sensSlider;
    TMP_Text sensText;

    private void Awake()
    {
        sensSlider = GetComponent<Slider>();
        sensText = GetComponentInChildren<TMP_Text>();
    }

    private void OnEnable()
    {
        Slider sensSlider = GetComponent<Slider>();
        sensSlider.value = mouseSensitivity.RuntimeValue;
        sensText.text = System.Math.Round(mouseSensitivity.RuntimeValue, 2).ToString();
    }

    public void SetSensitivity(System.Single sens)
    {
        sensText.text = System.Math.Round(sens, 2).ToString();
        mouseSensitivity.InitialValue = sens;
        mouseSensitivity.RuntimeValue = sens;
    }
}