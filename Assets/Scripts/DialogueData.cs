using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue Data", menuName = "ScriptableObjects/DialogueData", order = 1)]
public class DialogueData : ScriptableObject
{
    [field: SerializeField] public List<DialogueTime> StartingDialogue { get; set; }
    [field: SerializeField] public List<DialogueTime> MiddleDialogue { get; set; }
    [field: SerializeField] public List<DialogueTime> EndingDialogue { get; set; }
    [field: SerializeField] public List<DialogueTime> DeathDialogue { get; set; }
    [field: SerializeField] public List<DialogueTime> HelpDialogue { get; set; }
    [field: SerializeField] public List<DialogueTime> RespawnDialogue { get; set; }
}

[System.Serializable]
public struct DialogueTime 
{
    [field: SerializeField] public string Text { get; set; }
    [field: SerializeField] public float Seconds { get; set; }
}

public class DialogueArgs : EventArgs
{
    public string DialogueContext = "";
}

public static class DialogueEvent
{
    public static event EventHandler<DialogueArgs> DialogueStart;
    public static event EventHandler<DialogueArgs> DialogueEnd;

    public static void InvokeDialogueStart(string Context)
    {
        DialogueStart(null, new DialogueArgs { DialogueContext = Context });
    }

    public static void InvokeDialogueEnd(string Context)
    {
        DialogueEnd(null, new DialogueArgs { DialogueContext = Context });
    }
}
