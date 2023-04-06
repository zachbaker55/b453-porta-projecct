using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteEnd : MonoBehaviour
{
    public NoteDirection direction;
    private bool pressed = false;

    public void Update() {
        if (Input.GetButtonDown("Left") && direction == NoteDirection.left) {
            ShowPressed();
        }
        if (Input.GetButtonDown("Up") && direction == NoteDirection.up) {
            ShowPressed();
        }
        if (Input.GetButtonDown("Down") && direction == NoteDirection.down) {
            ShowPressed();
        }
        if (Input.GetButtonDown("Right") && direction == NoteDirection.right) {
            ShowPressed();
        }
    }

    private void ShowPressed() {
        if (!pressed) {
            transform.localScale = new Vector2(1.2f,1.2f);
            pressed = true;
            Invoke("RevertPressed",0.2f);
        }
    }
    
    private void RevertPressed() {
        transform.localScale = new Vector2(1.0f,1.0f);
        pressed = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Note note = other.gameObject.GetComponent<Note>();
        if (note != null) {
            note.IsEnabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        Note note = other.gameObject.GetComponent<Note>();
        if (note != null) {
            note.OnMiss();
        }
    }
}
