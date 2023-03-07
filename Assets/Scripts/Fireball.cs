using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This script is for the fireball object, it shouldnt do much
*/
public class Fireball : MonoBehaviour
{

    [field:SerializeField] public float lifeTime {get; set;} = 5f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject,lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "TilemapWall")
        {
             Destroy(this.gameObject);
        }
        else if(collision.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
        
    }

}
