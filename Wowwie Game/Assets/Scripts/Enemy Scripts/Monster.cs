using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] float minDistance;
    [SerializeField] float timer;
    [SerializeField] float timeBtwSpawn;

    [SerializeField] GameObject fireBall;

    [SerializeField] Transform spawnPoint;
    private Transform Player;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;

        timer = timeBtwSpawn;
    }

    private void Update()
    {
        float distance = Vector2.Distance(transform.position, Player.position);

        if(distance <= minDistance && timer <= 0)
        {
            Instantiate(fireBall, spawnPoint.position, Quaternion.identity);
            timer = timeBtwSpawn;
        }else
        {
            timer -= Time.deltaTime;
        }
    }
}
