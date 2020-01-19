using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePotion : MonoBehaviour
{
    public int i;
    [SerializeField] float randomTimeValue;
    PlayerController pc;

    private void Start()
    {
        if(pc == null)
        {
            return;
        }
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();


        i = Random.Range(1, 3);
        randomTimeValue = Random.Range(20, 80);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(pc == null)
        {
            return;
        }

        if(collision.CompareTag("Player") && i == 1)
        {
            Destroy(gameObject);
            pc.time += randomTimeValue;
        }else if(collision.CompareTag("Player") && i == 2)
        {
            Destroy(gameObject);
            pc.time -= randomTimeValue;
        }
    }
}
