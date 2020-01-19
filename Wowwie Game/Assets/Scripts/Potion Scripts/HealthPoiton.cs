using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPoiton : MonoBehaviour
{
    [SerializeField] int i;
    PlayerController pc;

    private void Start()
    {
        
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();


        i = Random.Range(1, 3);
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
            pc.health += 5;
        }
        else if(collision.CompareTag("Player") && i == 2)
        {
            Destroy(gameObject);
            pc.health -= 5;
        }
    }
}
