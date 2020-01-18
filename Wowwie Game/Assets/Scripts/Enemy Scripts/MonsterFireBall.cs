using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFireBall : MonoBehaviour
{
    [SerializeField] float speed;
    private Vector2 playerPos;
    Transform Player;

    PlayerController pc;

    Rigidbody2D rb;
    Animator animator;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        playerPos = new Vector2(Player.position.x, Player.position.y);

        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerPos, speed * Time.deltaTime);

        if(transform.position.x == playerPos.x && transform.position.y == playerPos.y)
        {
            animator.SetTrigger("isBurst");
            StartCoroutine(Stop());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Stop()
    {
        yield return new WaitForSeconds(.2f);
        Destroy(gameObject);
    }
}
