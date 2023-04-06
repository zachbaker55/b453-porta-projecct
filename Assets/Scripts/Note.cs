using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;


public class Note : MonoBehaviour
{
    [SerializeField] private NoteDirection direction;
    public NoteDirection Direction { 
        get { return direction; }
        set { direction = value; }
    }

    [field:SerializeField] public float NoteSpeed {get; set;} = 1f;
    [field:SerializeField] public float DeathTime {get; set;} = 1f;
    public bool Triggered {get; set;} = false;
    private bool hit = false;
    private bool missed = false;
    private Rigidbody2D rigidBody;
    public static event Action<int> NoteHit;
    public static event Action<int> NoteMissed;

    private static int hits;
    private static int misses;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start() 
    {
        rigidBody.velocity = new Vector2(0,-1).normalized * NoteSpeed;
    }

    private void Update() {
        if (Triggered) {
            if (Input.GetButtonDown("Left") && direction == NoteDirection.left) {
                OnHit();
            }
            if (Input.GetButtonDown("Up") && direction == NoteDirection.up) {
                OnHit();
            }
            if (Input.GetButtonDown("Down") && direction == NoteDirection.down) {
                OnHit();
            }
            if (Input.GetButtonDown("Right") && direction == NoteDirection.right) {
                OnHit();
            }
        }
    }



    public void OnHit() {
        if (!missed) {
            hit = true;
            hits += 1;
            NoteHit?.Invoke(hits);
            DoDestroy();
        }
    }

    public void OnMiss()
    {
        if (!hit) {
            missed = true;
            misses += 1;
            NoteMissed?.Invoke(misses);
            StartCoroutine(ShrinkDown());
            rigidBody.velocity = Vector2.zero;
            Invoke("DoDestroy", DeathTime);
        }
    }
    IEnumerator ShrinkDown() 
    {
        Vector2 initialScale = transform.localScale;
        for (float t = 0; t < DeathTime; t += Time.deltaTime) {
            float progress = t / DeathTime;
            transform.localScale = Vector2.Lerp(initialScale, Vector2.zero, progress);
            rigidBody.velocity = Vector2.Lerp(Vector2.zero, new Vector2(0,2), progress);
            yield return null;
        }
    }

    private void DoDestroy() 
    {
        Destroy(this.gameObject);
    }
}
