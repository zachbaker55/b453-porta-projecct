
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorProfileList", menuName = "ScriptableObjects/ColorProfileList", order = 2)]
public class ColorProfileList : ScriptableObject
{

    [field: SerializeField] public List<ColorProfile> colorProfiles;

}


