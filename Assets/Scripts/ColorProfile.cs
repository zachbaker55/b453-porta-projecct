using System;
using UnityEngine;

[System.Serializable]
public struct ColorProfile
{
    public bool errorMode;
    public float transitionTime;
    [Range(-101, 100)] public float saturation;
    [Range(-101, 100)] public float brightness;
    [Range(-101, 100)] public float contrast;
}

