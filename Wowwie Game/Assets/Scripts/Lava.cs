using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    public int i;

    BoxCollider2D bc;

    private void Start()
    {
        bc = GetComponent<BoxCollider2D>();

        i = Random.Range(1, 3);

        if(i == 1)
        {
            bc.isTrigger = false;
        }else if(i == 2)
        {
            bc.isTrigger = true;
        }
    }
}
