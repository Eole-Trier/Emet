using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactibles : MonoBehaviour
{
    [HideInInspector] public bool IsOn;
    protected float m_Timer;
    public List<Mechanism> MechanismList = new();

    public abstract void FixedUpdate();
    public abstract void OnOff();

    private void OnValidate()
    {
        foreach (Mechanism m in MechanismList)
        {
            if (m == null)
            {
                Debug.Log("Mechanism missing in : " + name);
                continue;
            }

            else if (!m.m_InteractibleList.Contains(this))
            {
                m.m_InteractibleList.Add(this);
            }
        }
    }

    public void Active()
    {
        foreach (Mechanism m in MechanismList)
        {
            // If every interactibles the object is on then activate/desactivate it
            if (m.timer == 0 && m.gameObject.activeInHierarchy == m.IsActive && m.m_InteractibleList.FindAll(interactible => interactible.IsOn).Count == m.m_InteractibleList.Count)
                m.gameObject.SetActive(!m.IsActive);

            // If timer is at 0 and every interactibles the object is on then activate/desactivate
            if (m.myTimer <= 0 && m.timer > 0 && m.m_InteractibleList.FindAll(interactible => interactible.IsOn).Count == m.m_InteractibleList.Count)
                m.gameObject.SetActive(!m.gameObject.activeInHierarchy);

            //If playOnce is true then loop the activate/desactivate state if timer > 0
            if (m.myTimer <= 0 && !m.playOnce)
                m.myTimer = m.timer;

            m.myTimer -= Time.fixedDeltaTime;
        }
    }
    public void Desactive()
    {
        foreach (Mechanism m in MechanismList)
        {
            if (m == null)
                continue;

            m.myTimer = m.timer;
            if (m.gameObject.activeInHierarchy != m.IsActive && m.m_InteractibleList.FindAll(interactible => interactible.IsOn).Count == 0)
                m.gameObject.SetActive(m.IsActive);
        }
    }
}
