using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatFireBall : MonoBehaviour
{
    [SerializeField] float speed;

    Bat bat;

    Rigidbody2D rb;

    private void Start()
    {
        bat = GameObject.FindGameObjectWithTag("Bat").GetComponent<Bat>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(bat.isRight)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }else if(!bat.isRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }

        Destroy(gameObject, 1f);
    }
}
