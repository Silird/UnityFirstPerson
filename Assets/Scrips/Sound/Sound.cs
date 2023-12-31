using System;
using UnityEngine;

[Serializable]
public class Sound
{

    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(0f, 1f)]
    public float pitch;
    [Range(0f, 1f)]
    public float thirdD;
    public bool loop;
}
