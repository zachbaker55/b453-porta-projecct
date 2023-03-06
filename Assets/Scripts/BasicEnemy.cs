using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    [SerializeField] private GameObject target; 

    [SerializeField] private Vector2 targetLocation; 

    [SerializeField] private Rigidbody2D rb;
    
    [SerializeField] private Animator anim;

    [field:SerializeField] public float speed {get; set;} = 2f;

    [field:SerializeField] public float health {get; set;} = 100f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(target != null)
        {targetLocation = target.gameObject.transform.position;}

        float distance = Vector2.Distance(targetLocation, this.transform.position);

        if(distance > .15f)
        {
            anim.Play("Walk");
            rb.velocity = (targetLocation - (Vector2)this.transform.position).normalized * speed;
        }
        else
        {
            rb.velocity = Vector2.zero;
            anim.Play("Idle");
        }
    }
}
