using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeathText : MonoBehaviour
{
    public GameObject deathText;

    private void OnEnable() 
    {
        DialogueEvent.DialogueStart += OnDialogueStart;
    }

    private void OnDisable() 
    {
        DialogueEvent.DialogueStart -= OnDialogueStart;
    }

    private void OnDialogueStart(object sender, DialogueArgs args) 
    {
        if(args.DialogueContext == "DeathDialogue") MakeVisible();
    }

    public void MakeVisible()
    {
        Debug.Log("DeathText Visible");
        deathText.SetActive(true);
    }

}
