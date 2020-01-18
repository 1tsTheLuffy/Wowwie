using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    private bool isRight;

    [SerializeField] float movementSpeed;

    [SerializeField] Transform[] movePoints;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
    }
}
