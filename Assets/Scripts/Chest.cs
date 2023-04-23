using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Chest : MonoBehaviour
{
    public GameObject chestOpen;
    public GameObject chestClosed;
    public Player player;

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
        if(args.DialogueContext == "EndingDialogue") MakeVisible();
    }

    public void MakeVisible()
    {
        if(player.isDead != true )
        {
        StartCoroutine(Open());
        }
    }

    IEnumerator Open()
    {

        chestClosed.SetActive(true);
        yield return new WaitForSeconds(5); 
        chestClosed.SetActive(false);
        chestOpen.SetActive(true);
    }

}
