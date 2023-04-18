using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Camera mCamera;
    [SerializeField] float Duration = .2f;
    [SerializeField] float Frequency = 20f;
    [SerializeField] float Amplitude = 10f;
    [SerializeField] float Priority = 0f;
    [SerializeField] AnimationCurve curve;
    Vector3 originalPosition;
    void Awake()
    {
        mCamera = Camera.main;
    }

    void Start()
    {
        GameManager.PlayerWasDamaged += ScreenShake;
        originalPosition = mCamera.transform.position;
    }

    void OnDestroy()
    {
        GameManager.PlayerWasDamaged -= ScreenShake;
    }

    void ScreenShake()
    {
        //Debug.Log("Screen Shake Started");
        StartCoroutine( Shake(Duration, Frequency, Amplitude, Priority));
    }

    IEnumerator Shake(float duration, float frequency, float amplitude, float priority)
    {
        //setting the variables for the shake and for potential to override the default shaking
        float shakeDuration = this.Duration;
        float shakeFrequency = 1/this.Frequency;
        float shakeAmplitude = this.Amplitude;

        if(priority > this.Priority)
        {
            shakeDuration = duration;
            shakeFrequency = 1/frequency;
            shakeAmplitude = amplitude;
        }
        //instead of another co-routine to manage a wait timer, just resetting elapsedtime and changing screen shake position
        float totalElapsedTime = 0;
        float elapsedTime = shakeFrequency;

        //Uses a curve to make shaking weaker the longer it lasts
        while(totalElapsedTime < shakeDuration)
        {
            totalElapsedTime += Time.deltaTime;
            elapsedTime += Time.deltaTime;
            if(elapsedTime >= shakeFrequency)
            {
                elapsedTime = 0;
                float ShakeStrength = curve.Evaluate(totalElapsedTime/shakeDuration);
                Vector3 newPosition = mCamera.transform.position + (Random.insideUnitSphere * Amplitude * ShakeStrength);
                mCamera.transform.position = Vector3.Slerp(originalPosition, newPosition, shakeFrequency);
            }
            yield return null;
        }
        mCamera.transform.position = originalPosition;
    }
}
