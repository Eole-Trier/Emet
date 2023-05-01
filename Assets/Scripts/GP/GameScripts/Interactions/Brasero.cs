using System.Collections.Generic;
using UnityEngine;
using static ObjectType;

public class Brasero : Interactibles
{
    private BurningObject m_Brasero;
    private List<ParticleSystem> m_ParticleSystem;
    private bool m_Play;

    private void Start()
    {
        m_AudioManager = FindObjectOfType<AudioManager>();
        m_Brasero = GetComponent<BurningObject>();


        m_ParticleSystem = new (GetComponentsInChildren<ParticleSystem>());
    }

    public override void OnOff() {;}

    public void FixedUpdate() 
    {
        IsOn = m_Brasero.IsBurning;

        if (IsOn)
        {
            Active();
            m_ParticleSystem.ForEach((flame) => flame.Play());
        }
        else
        {
            Desactive();
            m_ParticleSystem.ForEach((flame) => flame.Stop());
        }

        if (m_Brasero.IsBurning && m_Play)
        {
            m_AudioManager.m_AudioSourceList.Find(s => s.name == "fire_on").Play();
            m_AudioManager.m_AudioSourceList.Find(s => s.name == "fire_on").transform.position = m_Brasero.transform.position;

            m_AudioManager.m_AudioSourceList.Find(s => s.name == "fire_burning").Play();
            m_AudioManager.m_AudioSourceList.Find(s => s.name == "fire_burning").transform.position = m_Brasero.transform.position;
            m_Play = false;
        }

        else if (!m_Brasero.IsBurning && !m_Play)
        {
            m_AudioManager.m_AudioSourceList.Find(s => s.name == "fire_burning").Stop();
            m_AudioManager.m_AudioSourceList.Find(s => s.name == "fire_off").Play();
            m_AudioManager.m_AudioSourceList.Find(s => s.name == "fire_off").transform.position = m_Brasero.transform.position;
            m_Play = true; 
        }

    }

}
