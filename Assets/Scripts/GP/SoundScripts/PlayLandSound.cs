using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayLandSound : MonoBehaviour
{

    private AudioManager m_AudioManager;
    // Start is called before the first frame update
    void Start()
    {
        m_AudioManager = FindObjectOfType<AudioManager>();
    }

    public void PlayLandingSound()
    {
        m_AudioManager.m_AudioSourceList.Find(s => s.name == "golem_land").Play();

    }
}
