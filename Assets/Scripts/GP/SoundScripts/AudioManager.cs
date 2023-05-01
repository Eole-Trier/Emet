using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    // Start is called before the first frame update
    void Awake()
    {

        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.spatialBlend = 1.0f;
            s.source.rolloffMode = AudioRolloffMode.Logarithmic;
            s.source.minDistance = s.minDistance;
            s.source.maxDistance = s.maxDistance;
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }
    }
    
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s.source != null)
        {
            s.source.Play();
        }
        else
            Debug.LogWarning("Sound " + s.source.name + " not Found");
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s.source != null)
            s.source.Stop();
        else
            Debug.LogWarning("Sound " +  s.source.name + " not Found");
    }

    public bool IsPlaying(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s.source == null)
        {
            Debug.LogWarning("Sound " + name + " not Found");
            return false;
        }
        else
        {
            return s.source.isPlaying;
        }
    }

    public void PlayOnce(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s.source == null)
            Debug.LogWarning("Sound " + name + " not Found");
        else if (!s.played)
        {
            s.source.Play();
            s.played = true;
        }
    }
}
