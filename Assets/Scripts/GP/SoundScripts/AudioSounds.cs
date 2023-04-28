using System;
using UnityEditor.Experimental.RestService;
using UnityEngine;

[Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)] 
    public float volume;

    public bool loop;
    [HideInInspector] public bool played;
    [HideInInspector]
    public AudioSource source;
}
