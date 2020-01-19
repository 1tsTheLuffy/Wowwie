using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hog : MonoBehaviour
{
    private int i;
    private bool isRight;
    [SerializeField] float runSpeed;
    [SerializeField] float movementSpeed;
    [SerializeField] float rayDistance;

    [SerializeField] GameObject destroyParticle;
    [SerializeField] GameObject[] powerUps;

    public int health = 2;

    Transform Player;
    [SerializeField] Transform[] movePoints;

    [SerializeField] LayerMask PlayerMask;

    Rigidbody2D rb;
    Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        Player = GameObject.FindGameObjectWithTag("Player").transform;

        i = Random.Range(0, powerUps.Length);
    }

    private void Update()
    {
        if(Player == null)
        {
            return;
        }


        RaycastHit2D raycastRight = Physics2D.Raycast(transform.position, Vector2.right, rayDistance, PlayerMask);
        RaycastHit2D raycastLeft = Physics2D.Raycast(transform.position, -Vector2.right, rayDistance, PlayerMask);
        if (raycastRight.collider != null)
        {
            if (raycastRight.collider.CompareTag("Player"))
            {
                transform.Translate(Vector3.right * runSpeed * Time.deltaTime);
                transform.localScale = new Vector2(-1, 1);
                animator.SetBool("isRunning", true);
                //   Debug.Log(raycastRight.transform.name + "Right Player..");
            }
        }
        else if (raycastLeft.collider != null)
        {
            if (raycastLeft.collider.CompareTag("Player"))
            {
                transform.Translate(Vector3.left * runSpeed * Time.deltaTime);
                transform.localScale = new Vector2(1, 1);
                animator.SetBool("isRunning", true);
                // Flip();
                //  Debug.Log(raycastLeft.transform.name + "Left Player..");
            }
        }
        else
        {
            animator.SetBool("isRunning", false);
            if (!isRight)
            {
                transform.Translate(movementSpeed * Time.deltaTime, 0, 0);
                transform.localScale = new Vector2(-1, 1);
            }
            else
            {
                transform.Translate(-movementSpeed * Time.deltaTime, 0, 0);
                transform.localScale = new Vector2(1, 1);
            }

            if (Vector2.Distance(transform.position, movePoints[1].position) < 1f)
            {
                isRight = true;
            }
            if (Vector2.Distance(transform.position, movePoints[0].position) < 1f)
            {
                isRight = false;
            }
        }

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("BulletTag"))
        {
            health -= 1;
        }
    }

    private void Flip()
    {
        Vector2 faceDirection = transform.localScale;
        faceDirection.x *= -1;
        transform.localScale = faceDirection;
    }

    private void OnDestroy()
    {
        GameObject d = Instantiate(destroyParticle, transform.position, Quaternion.identity);
        Instantiate(powerUps[i], transform.position, Quaternion.identity);
        Destroy(d, 5f);
    }
}
