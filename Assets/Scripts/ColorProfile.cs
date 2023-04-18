using UnityEngine;

[CreateAssetMenu(fileName = "ColorProfile", menuName = "ScriptableObjects/ColorProfile", order = 1)]
public class ColorProfile : ScriptableObject
{
    public bool errorMode;
    public bool instantTransition;
    public float transitionTime;
    [Range(-101, 100)] public float saturation;
    [Range(-101, 100)] public float brightness;
    [Range(-101, 100)] public float contrast;

}