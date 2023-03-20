using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

/*
This script is a SINGLETON for that manages whatever global script behavior they had in the godot version
*/
public class GameManager : MonoBehaviour
{

    public static event Action PlayerWasDamaged;

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

    void Update() 
    {
        if(Input.GetButtonDown("UI Pause"))
        {
            SceneManager.LoadScene(0);
        }
    }

    
    private void OnSceneChange(Scene current, Scene next) 
    {
        int newScene = next.buildIndex;
        if (newScene != 0) LevelCheckPoint = newScene;
    }

    private void OnEnable()
    {
        SceneManager.activeSceneChanged += OnSceneChange;
    }

    private void OnDisable() 
    {
        SceneManager.activeSceneChanged -= OnSceneChange;
    }

    public static void StartPlayerWasDamaged() 
    {
        PlayerWasDamaged.Invoke();
    }


}
