using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicReader : MonoBehaviour
{

    [System.Serializable]
    public class MusicNote
    {
        public double time;
        public string note;
    }

    [System.Serializable]
    public class MusicNoteList
    {
        public MusicNote[] musicNotes;
    }
    [SerializeField] private TextAsset jsonText;
    [SerializeField] private Transform startLeft;
    [SerializeField] private Transform startUp;
    [SerializeField] private Transform startDown;
    [SerializeField] private Transform startRight;
    [SerializeField] private GameObject noteLeft;
    [SerializeField] private GameObject noteUp;
    [SerializeField] private GameObject noteDown;
    [SerializeField] private GameObject noteRight;
    private float currentTime = -10.0f;
    private int currentIndex = 0;
    private const float noteDropTime = 1.674979f;
    private bool playing = false;
    public MusicNoteList _noteList = new MusicNoteList();
    public MusicNoteList NoteList
    {
        get {return _noteList;}
    }

    private void Start()
    {
        //This one line formats all the text from jsonText into MusicNotes
        _noteList = JsonUtility.FromJson<MusicNoteList>(jsonText.text);
    }

    private void Update()
    {
        if (_noteList.musicNotes.Length > currentIndex) {
            if (currentTime + noteDropTime >= _noteList.musicNotes[currentIndex].time)
            {
                SpawnNote(_noteList.musicNotes[currentIndex]);
                currentIndex++;
            }
        }
        if (playing)
        {
            currentTime += Time.deltaTime;
        }
    }

    private void OnEnable()
    {
        MusicPlayer.SongStart += OnSongStart;
        MusicPlayer.SongEnd += OnSongEnd;
    }

    private void OnDisable()
    {
        MusicPlayer.SongStart -= OnSongStart;
        MusicPlayer.SongEnd += OnSongEnd;
    }

    private void OnSongStart(string name, float startingDelay)
    {
        currentTime = - startingDelay;
        playing = true;
    }
    
    private void OnSongEnd() {
        currentTime = -10.0f;
        playing = false;
    }
    private void SpawnNote(MusicNote musicNote)
    {
        if (musicNote.note == "left")
        {
            GameObject spawnedNote = Instantiate(noteLeft, startLeft.position, Quaternion.identity);
            spawnedNote.transform.parent = gameObject.transform;
        } else if (musicNote.note == "up")
        {
            GameObject spawnedNote = Instantiate(noteUp, startUp.position, Quaternion.identity);
            spawnedNote.transform.parent = gameObject.transform;
        } else if (musicNote.note == "down")
        {
            GameObject spawnedNote = Instantiate(noteDown, startDown.position, Quaternion.identity);
            spawnedNote.transform.parent = gameObject.transform;
        } else if (musicNote.note == "right")
        {
            GameObject spawnedNote = Instantiate(noteRight, startRight.position, Quaternion.identity);
            spawnedNote.transform.parent = gameObject.transform;
        } else
        {
            //If json has an incorrect note- stop and do nothing
            return;
        }
    }
}
