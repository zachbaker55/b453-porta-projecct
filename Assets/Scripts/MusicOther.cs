using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum NoteDirection {
    left,
    up,
    down,
    right
}


[System.Serializable]
public class JSONNote
{
    public double time;
    public NoteDirection note;
}
