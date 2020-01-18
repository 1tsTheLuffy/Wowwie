using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    private bool isRight;
    public int i;
    [SerializeField] float movementSpeed;

    [SerializeField] GameObject bloodParticle;

    [SerializeField] Transform[] movePoints;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        i = Random.Range(1, 4);
    }

    private void Update()
    {
        if (!isRight)
        {
            transform.Translate(movementSpeed * Time.deltaTime, 0, 0);
        }
        else
        {
            transform.Translate(-movementSpeed * Time.deltaTime, 0, 0);
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

    private void OnDestroy()
    {
        GameObject d = Instantiate(bloodParticle, transform.position, Quaternion.identity);
        Destroy(d, 5f);
    }
}
