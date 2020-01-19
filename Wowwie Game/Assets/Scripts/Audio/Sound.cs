using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip Clip;

    [Range(0f, 1f)]
    public float Volume;

    [Range(0f, 3f)]
    public float Pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource audioSource;
}
