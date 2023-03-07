using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This script is a SINGLETON for that manages whatever global script behavior they had in the godot version
*/
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [field:SerializeField] public int DeathCounter  { get; set; } = 0;

    [field:SerializeField] public int LevelCheckPoint  { get; set; } = 0;

    void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
            DontDestroyOnLoad(this);
        } 
    }
}
