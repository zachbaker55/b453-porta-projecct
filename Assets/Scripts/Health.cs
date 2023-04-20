using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private Sprite full;
    [SerializeField] private Sprite threeFourth;
    [SerializeField] private Sprite half;
    [SerializeField] private Sprite oneFourth;

    private List<Sprite> spriteList;
    private int spriteIndex;

    private SpriteRenderer spriteRenderer;

    private void Awake() 
    {
        spriteList = new List<Sprite> {full, threeFourth, half, oneFourth}; 
        spriteIndex = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    private void OnEnable() 
    {
        GameManager.PlayerWasDamaged += TakeDamage;
        GameManager.PlayerDied += OnDeath;
    }

    private void OnDisable() 
    {
        GameManager.PlayerWasDamaged -= TakeDamage;
        GameManager.PlayerDied -= OnDeath;
    }

    private void TakeDamage(float healthRemaining) 
    {
        if (spriteIndex < spriteList.Count) 
        {
            spriteIndex++;

            if (spriteIndex != spriteList.Count) 
            {
                spriteRenderer.sprite = spriteList[spriteIndex];   
            } else 
            {
                spriteRenderer.sprite = null;
            }
        }
    }

    private void OnDeath() {
        spriteRenderer.sprite = null;
    }
}
