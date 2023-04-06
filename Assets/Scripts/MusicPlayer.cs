using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

    //Song 0 is the song used on Main Menu and levels if song is null
    //Songs 1-4 are used on levels 1-4 respectively
    [SerializeField] private List<AudioClip> songs = new List<AudioClip>();

    //Don't actually use this, just use the audiosource from the scene

    //Menu think of something else
    //Level 1 Another Mistake
    //Level 2 01 rail trail halley labs
    //Level 3 The Hollow Spectacle
    //Level 4 wasnt all there halley labs
    private void Start() {
        Debug.Log(songs[0].name);
    }

}
