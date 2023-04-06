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

    private void ActivateLabel(string musicName) 
    {
        _musicName = musicName;
        StartCoroutine(Label());
    }

    IEnumerator Label() 
    {
        foreach (char c in _musicName) {
            _text.text += c;
            yield return new WaitForSeconds(labelTime * 0.1f);
        }
    }

}
