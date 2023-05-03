using System.Collections.Generic;
using UnityEngine;

public class Eolienne : Interactibles
{
    private Collider m_Collider;
    private List<EoleBehaviour> m_EoleBehavior;
    private bool m_Play;

    private void Start()
    {
        m_AudioManager = FindObjectOfType<AudioManager>();
        m_Collider = GetComponent<BoxCollider>();
        m_EoleBehavior = new(FindObjectsOfType<EoleBehaviour>());
    }

    public void FixedUpdate()
    {
            m_AudioManager.m_AudioSourceList.Find(s => s.name == "propeller_rotate").transform.position = transform.position;
        if (m_EoleBehavior.Find(eole => eole.listCollider.Find(list => list.name == m_Collider.name)))
        {
            IsOn = true;
            Active();

            if (!m_AudioManager.m_AudioSourceList.Find(s => s.name == "propeller_rotate").isPlaying && m_AudioManager.m_AudioSourceList.Find(s => s.name == "propeller_rotate").isPlaying)
                m_AudioManager.m_AudioSourceList.Find(s => s.name == "propeller_rotate").Stop();

            else if (m_AudioManager.m_AudioSourceList.Find(s => s.name == "propeller_rotate").isPlaying && !m_AudioManager.m_AudioSourceList.Find(s => s.name == "propeller_rotate").isPlaying)
                m_AudioManager.m_AudioSourceList.Find(s => s.name == "propeller_rotate").Play();
        }
        else
        {
            IsOn = false;
            Desactive();
            if (m_AudioManager.m_AudioSourceList.Find(s => s.name == "propeller_rotate").isPlaying)
                m_AudioManager.m_AudioSourceList.Find(s => s.name == "propeller_rotate").Stop();
        }
    }

    public override void OnOff() {}
}
