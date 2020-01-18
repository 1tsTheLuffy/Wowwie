using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pighead : MonoBehaviour
{
    private bool isGrounded;
    [SerializeField] float jumpForce;

    Transform Player;

    [SerializeField] LayerMask Ground;

    PlayerController pc;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(transform.position, .5f, Ground);

        if(isGrounded)
        {
            rb.velocity = Vector2.up * jumpForce * Time.deltaTime;
        }
    }
}
