using System;
using UnityEngine;

[Serializable]
public class Sound
{
    public string name;
    public float minDistance;
    public float maxDistance;

    public AudioClip clip;

    [Range(0f, 1f)] 
    public float volume;

    [Range(0f, 1f)]
    public float spatialBlend;

    public bool loop;
    [HideInInspector] public bool played;
    [HideInInspector]
    public AudioSource source;
}
