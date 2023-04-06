using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{

    //Song 0 is the song used on Main Menu and levels if song is null
    //Songs 1-4 are used on levels 1-4 respectively 
    [field:SerializeField] public float StartingDelay {get; set;} = 1f;   
    private AudioSource audioSource;
    private AudioClip song;
    public static event Action<string> SongStart;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
        song = audioSource.clip;
    }


    private void OnEnable() 
    {
        DialogueEvent.DialogueEnd += OnDialogueEnd;
    }

    private void OnDisable() 
    {
        DialogueEvent.DialogueEnd -= OnDialogueEnd;
    }

    private void OnDialogueEnd(object sender, DialogueArgs args) 
    {
        if(args.DialogueContext == "StartingDialogue") PlaySong();
    }

    private void PlaySong() {
        SongStart?.Invoke(song.name);
        audioSource.PlayDelayed(StartingDelay);
    }
}
