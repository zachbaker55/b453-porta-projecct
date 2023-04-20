using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{

    //Song 0 is the song used on Main Menu and levels if song is null
    //Songs 1-4 are used on levels 1-4 respectively 
    [field:SerializeField] public float StartingDelay {get; set;} = 1f;   
    private AudioSource audioSource;
    private AudioClip song;
    public static event Action<string,float> SongStart;
    public static event Action SongEnd;

    private void Awake() 
    {
        audioSource = GetComponent<AudioSource>();
        song = audioSource.clip;
    }

    private void Start() 
    {
        if (SceneManager.GetActiveScene().buildIndex == 0) 
        {
            StartSong();
        }
    }


    private void OnEnable() 
    {
        DialogueEvent.DialogueEnd += OnDialogueEnd;
        GameManager.PlayerDied += OnSongEnd;
    }

    private void OnDisable() 
    {
        DialogueEvent.DialogueEnd -= OnDialogueEnd;
        GameManager.PlayerDied -= OnSongEnd;
    }

    private void OnDialogueEnd(object sender, DialogueArgs args) 
    {
        if(args.DialogueContext == "StartingDialogue") StartSong();
    }

    private void StartSong() 
    {
        SongStart?.Invoke(song.name,StartingDelay);
        audioSource.PlayDelayed(StartingDelay);
        Invoke("OnSongEnd",StartingDelay + song.length);
    }

    private void OnSongEnd() 
    {
        audioSource.Stop();
        CancelInvoke();
        SongEnd?.Invoke();
    }
}
