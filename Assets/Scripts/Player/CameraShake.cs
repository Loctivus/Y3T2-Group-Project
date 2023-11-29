using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration;
    public float xIntensity;
    public float yIntensity;

    public void LongShake()
    {
        shakeDuration = 10;
        Vector3 originalPos = transform.localPosition;
        transform.localPosition = originalPos;
        StopAllCoroutines();
        StartCoroutine(ShakeCam(1));
    }

    public void CamShake(float strength)
    {
        Vector3 originalPos = transform.localPosition;
        transform.localPosition = originalPos;
        StopAllCoroutines();
        StartCoroutine(ShakeCam(strength));
    }

    public IEnumerator ShakeCam(float strength)
    {
        Vector3 originalPos = transform.localPosition;
        float elapsedTime = 0f;
        while (elapsedTime < shakeDuration)
        {
            elapsedTime += Time.deltaTime;
            float x = (Mathf.PerlinNoise(elapsedTime * xIntensity, 0f) * 2f - 1f) * strength;
            float y = (Mathf.PerlinNoise(0f, elapsedTime * yIntensity) * 2f - 1f) * strength;
            transform.localPosition = originalPos + new Vector3(x, y, 0f);
            
            yield return null;
        }

        transform.localPosition = originalPos;
    }

}
