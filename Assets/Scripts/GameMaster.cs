using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


/*
This script manages the dialogue of the game master who starts the cutscenes and stuff
*/
public class GameMaster : MonoBehaviour
{

    [SerializeField] private TMP_Text Dialogue;
    [SerializeField] private GameObject SkipButton;

    //Vary based on level
    [SerializeField] private DialogueData LevelDialogue; 
    [SerializeField] private int NextSceneValue;

    [SerializeField] private bool PlayerDead = false;
    [SerializeField] private bool DialogueSkipped = false;
    [SerializeField] private bool DialogueIsPlaying = false;
    [SerializeField] private bool SentenceIsDone = false;
    [SerializeField] private string CurrentDialogueContext = "";


    void Start()
    {
        GameManager.Instance.LevelCheckPoint = SceneManager.GetActiveScene().buildIndex;
        GameManager.PlayerDied += OnPlayerDead;
        DialogueEvent.DialogueStart += OnDialogueStart;
        DialogueEvent.DialogueEnd += OnDialogueEnd;
        StartCoroutine(PlayDialogue(LevelDialogue.StartingDialogue, "StartingDialogue"));
        
    }
    
    void OnDestroy()
    {
        GameManager.PlayerDied -= OnPlayerDead;
        DialogueEvent.DialogueStart -= OnDialogueStart;
        DialogueEvent.DialogueEnd -= OnDialogueEnd;
    }

    public void SkipDialogue()
    {
        this.DialogueSkipped = true;
        Dialogue.text = "";
    }


    //Would be a little cleaner looking as a switch and not using strings in the Dialogue Event arg, 
    //but it makes it easier to understand with strings
    void OnDialogueStart(object sender, DialogueArgs args)
    {
        Debug.Log("Dialogue Started " + args.DialogueContext);
        DialogueSkipped = false;
        SkipButton.SetActive(true);

        //Leaving this if block here incase we want to add features after dialogue
        if(args.DialogueContext == "StartingDialogue"){}
        else if(args.DialogueContext == "MiddleDialogue")
        {
            SkipButton.SetActive(false);
        }
        /*
        else if(args.DialogueContext == "EndingDialogue"){}
        else if(args.DialogueContext == "DeathDialogue"){}
        else if(args.DialogueContext == "HelpDialogue") {}
        else if(args.DialogueContext == "RespawnDialogue") {}
        */
    }

    void OnDialogueEnd(object sender, DialogueArgs args)
    {
        Debug.Log("Dialogue Ended " + args.DialogueContext);
        DialogueSkipped = false;
        DialogueIsPlaying = false;
        SkipButton.SetActive(false);

        if(args.DialogueContext == "StartingDialogue")
        {
            //After starting dialogue begin unskippable dialogue during the music
            StartCoroutine(PlayDialogue(LevelDialogue.MiddleDialogue, "MiddleDialogue"));
            //call Start music event or music event listens for this event
            WorldChanges();
        }
         //after unskippable dialogue, wait until we receive the end of music event or death
        //else if(args.DialogueContext == "MiddleDialogue"){}

        //if player is not dead, after this ending dialogue then swap scenes
        else if(args.DialogueContext == "EndingDialogue")
        {
            if(!PlayerDead)
            {SceneManager.GetSceneByBuildIndex(NextSceneValue);}
        }

        //if player died, after this dialogue, if death > 5 help, else respawn
        else if(args.DialogueContext == "DeathDialogue")
        {
            if(GameManager.Instance.DeathCounter >= 5)
            {StartCoroutine(PlayDialogue(LevelDialogue.HelpDialogue, "HelpDialogue"));}
            else
            { StartCoroutine(PlayDialogue(LevelDialogue.RespawnDialogue, "RespawnDialogue"));}
        }

        //if the player died > 5, play this. If not skip this and start respawn.
        else if(args.DialogueContext == "HelpDialogue")
        {
             StartCoroutine(PlayDialogue(LevelDialogue.RespawnDialogue, "RespawnDialogue"));
        }
        
        //play this before respawning level
        else if(args.DialogueContext == "RespawnDialogue")
        {
            SceneManager.LoadScene(GameManager.Instance.LevelCheckPoint);
        }
    }

    IEnumerator PlayDialogue( List<DialogueTime> NewDialogue, string DialogueContext)  
    {
        DialogueEvent.InvokeDialogueStart(DialogueContext);
        Dialogue.text = "";
        CurrentDialogueContext = DialogueContext;
        
        int i = 0;
        while(i < NewDialogue.Count)
        {
            //break out of while loop if we skipped

            if(DialogueSkipped)
            {break;}

            //if we do not have any dialogue being displayed, start displaying until the timer for the dialogue ends
            //if it is skipped while the timer is running, do not blink text
            if(!DialogueIsPlaying)
            {
                DialogueIsPlaying = true;
                StartCoroutine(ChangeText(NewDialogue[i].Text));
                StartCoroutine(WaitForSecondsOrSkip(NewDialogue[i].Seconds));
            }
            //Check if our current dialogue lines are done
            //if they are we can blink text and go to next lines
            if(SentenceIsDone)
            {
                this.Dialogue.gameObject.SetActive(false);
                yield return new WaitForSeconds(.15f);
                this.Dialogue.gameObject.SetActive(true);
                yield return new WaitForSeconds(.15f);
                this.Dialogue.gameObject.SetActive(false);
                yield return new WaitForSeconds(.15f);
                this.Dialogue.gameObject.SetActive(true);

                DialogueIsPlaying = false;
                Dialogue.text = "";
                i++;
            }
            SentenceIsDone = false;
            yield return null;
        }
        //End dialogue event and reset the text
        DialogueEvent.InvokeDialogueEnd(CurrentDialogueContext);
        
        Dialogue.text = "";
    }

    IEnumerator WaitForSecondsOrSkip(float Seconds) 
    {
        float totalElapsedTime = 0;
        while(totalElapsedTime <= Seconds && !DialogueSkipped)
        {
            totalElapsedTime += Time.deltaTime;
            yield return null;
        }

        if(!DialogueSkipped)
        {
            SentenceIsDone = true;
        }
    }

    //Cant use yield return waitforseconds here due to features colliding
    //the dialogue end event will reset the DialogueSkipped bool before the  waitforseconds co-routine ends
    //this causes text to continue to appear afterward and to fix this each function timer here needs to be manually coded
    IEnumerator ChangeText(string NewText) 
    {
        float totalElapsedTime = 0;
        int i = 0;
        while(i < NewText.Length)
        {
            if(DialogueSkipped)
            {
                Dialogue.text = "";
                break; 
            }

            totalElapsedTime += Time.deltaTime;

            if(totalElapsedTime > 0.07f)
            {
                Dialogue.text += NewText[i];
                i++;
                totalElapsedTime = 0;
            }
            yield return null;
        }
    }

    void OnPlayerDead()
    {
        PlayerDead = true;

        if(CurrentDialogueContext != "" && PlayerDead)
        {DialogueEvent.InvokeDialogueEnd(CurrentDialogueContext);}

        GameManager.Instance.DeathCounter++;
        StartCoroutine(PlayDialogue(LevelDialogue.DeathDialogue, "DeathDialogue"));
    }

    void OnMusicPlayerFinish()
    {   
        if(!PlayerDead)
        {
            StartCoroutine(PlayDialogue(LevelDialogue.EndingDialogue, "EndingDialogue"));
        }
    }

    void WorldChanges()
    {

    }



}
