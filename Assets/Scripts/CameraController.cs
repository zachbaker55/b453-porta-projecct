using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.PlayerWasDamaged += ScreenShake;
    }

    void OnDisable()
    {
        GameManager.PlayerWasDamaged -= ScreenShake;
    }

    void OnDestroy()
    {
        GameManager.PlayerWasDamaged -= ScreenShake;
    }

    void ScreenShake()
    {
        Debug.Log("Screen Shake Started");
    }
}