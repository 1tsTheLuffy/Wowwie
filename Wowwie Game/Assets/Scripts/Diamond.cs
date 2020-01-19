using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    [SerializeField] GameObject Effect;

    private void OnDestroy()
    {
        GameObject d = Instantiate(Effect, transform.position, Quaternion.identity);
        Destroy(d, 5f);
    }
}
