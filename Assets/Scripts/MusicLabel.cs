using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MusicLabel : MonoBehaviour
{
    private TMP_Text _text;
    public TMP_Text Text 
    { 
        get { return _text; }
    }


    [Range(0.0f,3.0f)]
    [SerializeField] private float labelTime = 1.0f;
    [SerializeField] private float remainTime = 10.0f;
    [SerializeField] private int endFlashes = 4;
    private string _musicName = "Example Song Name";
    public string MusicName 
    { 
        get { return _musicName; }
    }

    private void Awake() {
        _text = GetComponent<TMP_Text>();
    }

    private void OnEnable() 
    {
        MusicPlayer.SongStart += ActivateLabel;
    }

    private void OnDisable() 
    {
        MusicPlayer.SongStart -= ActivateLabel;
    }

    private void ActivateLabel(string musicName, float startingDelay) 
    {
        _musicName = musicName;
        StartCoroutine(Label());
    }

    IEnumerator Label() 
    {
        string musicString = "Music: " + _musicName;
        foreach (char c in musicString) 
        {
            _text.text += c;
            yield return new WaitForSeconds(labelTime * 0.1f);
        }
        Invoke("DeactivateLabel",remainTime);
    }

    private void DeactivateLabel() 
    {

        StartCoroutine(RemoveLabel());
    }

    IEnumerator RemoveLabel() 
    {
        string musicString = _musicName;
        foreach (char c in _musicName) 
        {
            musicString = musicString.Substring(1);
            _text.text = "Music: " + musicString;
            yield return new WaitForSeconds(labelTime * 0.1f);
        }
        bool flash = true;
        for (int i = 0; i < endFlashes*2; i++) {
            if (flash)
            {
                _text.text = "";
                flash = false;
            } else
            {
                _text.text = "Music:";
                flash = true;
            }
            yield return new WaitForSeconds(labelTime * 0.1f);
        }
        _text.text = "";
    }

}
