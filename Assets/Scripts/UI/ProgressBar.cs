using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem("GameObject/UI/RadialProgressBar")]
    public static void AddRadialProgressBar()
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>("UI/RadialProgressBar"));
        obj.transform.SetParent(Selection.activeGameObject.transform, false);
    }
#endif

    public int minimum;
    public int maximum;
    public int current;
    public FloatVariable maxAmount;
    public FloatVariable currentAmount;
    public Image fill;
    public Image weaponImage;

    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();
    }
    void GetCurrentFill()
    {
        //float currentOffset = current - minimum;
        //float maximumOffset = maximum - minimum;
        float currentOffset = currentAmount.RuntimeValue - minimum;
        float maximumOffset = maxAmount.RuntimeValue - minimum;
        float fillAmount = currentOffset / maximumOffset;
        fill.fillAmount = fillAmount;
    }
}
