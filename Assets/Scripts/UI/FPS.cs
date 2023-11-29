using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPS : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    private float count;
    // Update is called once per frame
    void Update()
    {
        text.text = Mathf.Round(count = 1f / Time.unscaledDeltaTime).ToString();
    }
}
