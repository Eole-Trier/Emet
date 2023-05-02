using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Rendering;
using UnityEngine;

public abstract class Interactibles : MonoBehaviour
{
    [HideInInspector] public bool IsOn;
    protected float m_Timer;
    public List<Mechanism> MechanismList = new();
    protected AudioManager m_AudioManager;
    private bool m_PlayOnce;

    private void Start()
    {
        m_AudioManager = FindObjectOfType<AudioManager>();

        foreach (Mechanism m in MechanismList)
            m.myTimer = m.timer;
    }

    public abstract void OnOff();

    private void OnValidate()
    {
        foreach (Mechanism m in MechanismList)
        {
            if (!m.m_InteractibleList.Contains(this))
                m.m_InteractibleList.Add(this);
        }
    }

    public void Active()
    {
        foreach (Mechanism m in MechanismList)
        {
            if (m.TryGetComponent(out MovingPlateform ma) && !m_PlayOnce)
            {
                m_AudioManager.m_AudioSourceList.Find(s => s.name == "moving_platform").Play();
                m_PlayOnce = true;
            }

            //If it's a moving platerform we make it move
            if (m.TryGetComponent(out MovingPlateform mp))
            {
                if(m_AudioManager.m_AudioSourceList.Find(s => s.name == "moving_platform").isPlaying)
                    m_AudioManager.m_AudioSourceList.Find(s => s.name == "moving_platform").transform.position = mp.transform.position;
                mp.speed = 2;
                continue;
            }
            


            // If every interactibles are on then activate/desactivate the object
            if (m.myTimer <= 0 && m.gameObject.activeInHierarchy == m.IsActive && m.m_InteractibleList.TrueForAll(interactibles => interactibles.IsOn))
            {
                if (m.ToString().Contains("Door"))
                    m_AudioManager.m_AudioSourceList.Find(s => s.name == "door_on").Play();
                m.gameObject.SetActive(!m.gameObject.activeInHierarchy);
            }
            //If playOnce is true then loop the activate/desactivate state if timer > 0
            if (!m.playOnce && m.myTimer < 0 && m.timer > 0)
            {
                m.gameObject.SetActive(!m.gameObject.activeInHierarchy);
                m.myTimer = m.timer;
            }
            if(m.myTimer >= 0)
                m.myTimer -= Time.deltaTime;
        }
    }
    public void Desactive()
    {
        foreach (Mechanism m in MechanismList)
        {
            if (m.TryGetComponent(out MovingPlateform mp))
            {
                mp.transform.position = mp.plateformPath[0].transform.position;
                mp.speed = 0;
                m_AudioManager.m_AudioSourceList.Find(s => s.name == "moving_platform").Stop();
                m_PlayOnce = false;
            }

            m.myTimer = m.timer;
            if (m.gameObject.activeInHierarchy != m.IsActive && m.m_InteractibleList.FindAll(interactible => interactible.IsOn).Count != m.m_InteractibleList.Count)
            {
                m.gameObject.SetActive(!m.gameObject.activeInHierarchy);
                if (m.ToString().Contains("Door") && m.gameObject.activeInHierarchy)
                    m_AudioManager.m_AudioSourceList.Find(s => s.name == "door_on").Stop();
            }
        }
    }
}
