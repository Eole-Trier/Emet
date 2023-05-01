using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public List<AudioSource> m_AudioSourceList = new();
    // Start is called before the first frame update
    void Awake()
    {

        foreach(Sound s in sounds)
        {
            GameObject go = new();
            go.transform.parent = gameObject.transform;
            go.name = s.name;
            go.AddComponent<AudioSource>();
            go.GetComponent<AudioSource>().spatialBlend = s.spatialBlend;
            go.GetComponent<AudioSource>().rolloffMode = AudioRolloffMode.Logarithmic;
            go.GetComponent<AudioSource>().minDistance = s.minDistance;
            go.GetComponent<AudioSource>().maxDistance = s.maxDistance;
            go.GetComponent<AudioSource>().clip = s.clip;
            go.GetComponent<AudioSource>().volume = s.volume;
            go.GetComponent<AudioSource>().loop = s.loop;
            m_AudioSourceList.Add(go.GetComponent<AudioSource>());

        }
    }
    
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, s => s.name == name);
        if (s.source != null)
        {
            m_AudioSourceList.Add(s.source);
            m_AudioSourceList.Find(asl => asl.name == s.source.name).Play();
        }
        else
            Debug.LogWarning("Sound " + s.name + " not Found");
    }

    public void Stop(string name)
    {
        AudioSource s = m_AudioSourceList.Find(s => s.name == name);
        if (s != null)
            s.Stop();
        else
            Debug.LogWarning("Sound " +  s.name + " not Found");
    }

    public bool IsPlaying(string name)
    {
        AudioSource s = m_AudioSourceList.Find(s => s.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not Found");
            return false;
        }
        else
        {
            return s.isPlaying;
        }
    }

    public void AddSound(Sound s)
    {
        GameObject go = new();
        go.transform.parent = gameObject.transform;
        go.name = s.name;
        go.AddComponent<AudioSource>();
        go.GetComponent<AudioSource>().spatialBlend = s.spatialBlend;
        go.GetComponent<AudioSource>().rolloffMode = AudioRolloffMode.Logarithmic;
        go.GetComponent<AudioSource>().minDistance = s.minDistance;
        go.GetComponent<AudioSource>().maxDistance = s.maxDistance;
        go.GetComponent<AudioSource>().clip = s.clip;
        go.GetComponent<AudioSource>().volume = s.volume;
        go.GetComponent<AudioSource>().loop = s.loop;
        m_AudioSourceList.Add(go.GetComponent<AudioSource>());
    }
}
