using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    public int i;
    public bool isRight;

    [SerializeField] float movementSpeed;
    [SerializeField] float timer;
    [SerializeField] float timeBtwSpawn;

    [SerializeField] GameObject destroyParticle;
    [SerializeField] GameObject[] powerUps;
    [SerializeField] GameObject fireBall;

    [SerializeField] Transform[] movePoints;
    [SerializeField] Transform rayPoint;

    [SerializeField] LayerMask Player;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        i = Random.Range(0, powerUps.Length);
    }


    private void Update()
    {
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

        Debug.Log(isRight);
        // Raycast..

        RaycastHit2D rayLeft = Physics2D.Raycast(rayPoint.position, Vector2.left, 5, Player);
        RaycastHit2D rayRight = Physics2D.Raycast(rayPoint.position, Vector2.right, 5, Player);
        if(rayLeft.collider != null)
        {
            if(rayLeft.collider.transform.CompareTag("Player") && timer <= 0 && isRight == true)
            {
                Instantiate(fireBall, rayPoint.position, rayPoint.rotation);
                //Debug.Log(rayLeft.transform.name);
                timer = timeBtwSpawn;
            }else
            {
                timer -= Time.deltaTime;
            }
        }else if(rayRight.collider != null)
        {
            if(rayRight.collider.transform.CompareTag("Player") && timer <= 0 && isRight == false)
            {
                Instantiate(fireBall, rayPoint.position, rayPoint.rotation);
                //Debug.Log(rayLeft.transform.name);
                timer = timeBtwSpawn;
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("BulletTag"))
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        GameObject d = Instantiate(destroyParticle, transform.position, Quaternion.identity);
        Instantiate(powerUps[i], transform.position, Quaternion.identity);
        Destroy(d, 5f);
    }
}
