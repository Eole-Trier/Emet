using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mechanism : MonoBehaviour
{
    public List<Interactibles> m_InteractiblesList = new();
    private List<Interactibles> m_InteractiblesOnList = new();
    [HideInInspector] public bool isOn;

    public void MechanismUpdate()
    {
        foreach (Interactibles interactible in m_InteractiblesList)
        {
            if (interactible.IsOn)
            {
                if (!m_InteractiblesOnList.Contains(interactible))
                    m_InteractiblesOnList.Add(interactible);
            }
            else
            {
                if (m_InteractiblesOnList.Contains(interactible))
                    m_InteractiblesOnList.Remove(interactible);
            }
        }

        if (m_InteractiblesList.Count == m_InteractiblesOnList.Count)
        {
            if(isOn)
                gameObject.SetActive(false);
            else if(!isOn)
                gameObject.SetActive(true);
        }
        else
        {
            if (isOn)
                gameObject.SetActive(true);
            else if(!isOn)
                gameObject.SetActive(false);
        }
    }
}
