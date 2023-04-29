using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s.source != null)
            s.source.Play();
        else
            Debug.Log("Sound" +  s.source.name + "not Found");
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s.source != null)
            s.source.Stop();
        else
            Debug.Log("Sound" +  s.source.name + "not Found");
    }

    public bool IsPlaying(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s.source == null)
        {
            Debug.Log("Sound" + name + "Not Found");
            return false;
        }
        else
            return s.source.isPlaying;
    }

    public void PlayOnce(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s.source == null)
            Debug.Log("Sound" + name + "Not Found");
        else if (!s.played)
        {
            s.source.Play();
            s.played = true;
        }
    }
}
