using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject prefabFireBall;

    [SerializeField] private Vector2 targetLocation;

    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private Camera mainCamera;

    [SerializeField] private Animator anim;



    [field:SerializeField] public float speed {get; set;} = 2.5f;

    [field:SerializeField] public float health {get; set;} = 100f;

    [field:SerializeField] public bool isDead {get; set;} = false;

    void Start()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        targetLocation = this.gameObject.transform.position;
    }

    void Update()
    {
        if(Input.GetButton("Fire2"))
        {
            Debug.Log("New Location");
            targetLocation = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        }

        if(Input.GetButtonDown("Fire1") && !isDead)
        {
            Debug.Log("Shot Fireball");
            CreateFireball();
        }


    }

    void FixedUpdate()
    {
        float distance = Vector2.Distance(targetLocation, this.transform.position);

        if(distance > .1f)
        {
            anim.Play("Walk");
            rb.velocity = (targetLocation - (Vector2)this.transform.position).normalized * speed;
        }
        else
        {
            //Setting velocity to 0  or targeting self helps fix the sprite from jiggling when extremely close to target
            //targetLocation = this.gameObject.transform.position;
            rb.velocity = Vector2.zero;
            anim.Play("Idle");
        }
    }

    void CreateFireball()
    {
        GameObject Fireball = Instantiate(prefabFireBall, this.transform.position, Quaternion.identity);
        Rigidbody2D Fireballrb = Fireball.GetComponent<Rigidbody2D>();
        Fireballrb.velocity =  mainCamera.ScreenToWorldPoint(Input.mousePosition)-this.transform.position ;
    }

}
