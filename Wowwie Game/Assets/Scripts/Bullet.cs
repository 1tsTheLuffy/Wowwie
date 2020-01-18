using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int i;
    [SerializeField] float speed;
    [SerializeField] float destroyTime;

    CapsuleCollider2D cc;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CapsuleCollider2D>();

        i = Random.Range(1, 4);
        if(i == 1)
        {
            cc.enabled = true;
            Debug.Log(i);
        }else if(i == 2)
        {
            cc.enabled = false;
            Debug.Log(i);
        }
        else if(i == 3)
        {
            StartCoroutine(enumerator());
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Hog") || collision.transform.CompareTag("Monster"))
        {
            Destroy(gameObject);
        }
    }

    IEnumerator enumerator()
    {
        yield return new WaitForSeconds(.2f);
        rb.gravityScale = 5f;
        yield return new WaitForSeconds(.2f);
        Destroy(gameObject);
    }
}
