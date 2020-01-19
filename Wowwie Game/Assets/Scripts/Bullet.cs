using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int i;
    [SerializeField] float speed;
    [SerializeField] float destroyTime;

    PlayerController pc;

    CapsuleCollider2D cc;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CapsuleCollider2D>();

        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        i = Random.Range(1, 4);
        if(i == 1)
        {
            cc.enabled = true;
        }else if(i == 2)
        {
            cc.enabled = false;
        }
    }

    private void Update()
    {
        Destroy(gameObject, destroyTime);
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.right * speed * Time.fixedDeltaTime);
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collision.CompareTag("Wall"))
    //    {
    //        Destroy(gameObject);
    //        pc.health -= 1; 
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Hog")  || collision.CompareTag("Pighead") || 
            collision.CompareTag("Spider") || collision.CompareTag("Bat") || collision.CompareTag("Monster"))
        {
            Destroy(gameObject);
            pc.health -= 1;
        }
    }
}
