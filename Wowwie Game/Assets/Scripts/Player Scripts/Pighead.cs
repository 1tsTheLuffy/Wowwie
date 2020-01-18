using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pighead : MonoBehaviour
{
    private float x;
    [SerializeField] float speed;

    PlayerController pc;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        
    }
}
