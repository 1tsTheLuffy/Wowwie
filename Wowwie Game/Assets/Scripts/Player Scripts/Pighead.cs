using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pighead : MonoBehaviour
{
    private int i;
    private bool isGrounded;
    [SerializeField] float jumpForce;
    [SerializeField] float radius;

    [SerializeField] GameObject destroyParticle;
    [SerializeField] GameObject[] powerUps;

    Transform Player;

    [SerializeField] LayerMask Ground;

    PlayerController pc;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        i = Random.Range(0, powerUps.Length);
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(transform.position, radius, Ground);

        if(isGrounded)
        {
            rb.velocity = Vector2.up * jumpForce * Time.fixedDeltaTime;
        }
    }

    private void OnDestroy()
    {
        Instantiate(powerUps[i], transform.position, Quaternion.identity);
        GameObject d = Instantiate(destroyParticle, transform.position, Quaternion.identity);
        Destroy(d, 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
