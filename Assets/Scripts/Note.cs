using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;


public enum NoteDirection {
    left,
    up,
    down,
    right
}


public class Note : MonoBehaviour
{
    public NoteDirection direction;
    [field:SerializeField] public float NoteSpeed {get; set;} = 1f;
    [field:SerializeField] public float DeathTime {get; set;} = 1f;
    private Rigidbody2D rigidBody;
    [HideInInspector] public bool IsEnabled {get; set;} = false;
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
        rigidBody.velocity = new Vector2(0,1).normalized * NoteSpeed;
    }

    public void OnMiss()
    {
        misses += 1;
        NoteMissed?.Invoke(misses);
        IsEnabled = false;
        StartCoroutine(ShrinkDown());
        rigidBody.velocity = Vector2.zero;
        Invoke("DoDestroy", DeathTime);
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
