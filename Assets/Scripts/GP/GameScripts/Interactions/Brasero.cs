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
        m_Brasero = FindObjectOfType<BurningObject>();
        IsOn = m_Brasero.IsBurning;


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
            m_AudioManager.Play("fire_on");
            m_AudioManager.Play("fire_burning");
            m_Play = false;
        }

        else if (!m_Brasero.IsBurning && !m_Play)
        {
            m_AudioManager.Stop("fire_burning");
            m_AudioManager.Play("fire_off");
            m_Play = true;
        }

    }

}
