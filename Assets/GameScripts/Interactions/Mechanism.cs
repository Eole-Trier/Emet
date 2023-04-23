using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mechanism : MonoBehaviour
{
    public List<Interactibles> m_InteractiblesList = new();
    private List<Interactibles> m_InteractiblesOnList = new();

    public void MechanismUpdate()
    {
        foreach (Interactibles interactible in m_InteractiblesList)
        {
            Debug.Log(interactible.name);
            if (interactible.IsOn)
            {
                if(!m_InteractiblesOnList.Contains(interactible))
                    m_InteractiblesOnList.Add(interactible);
            }
            else
            {
                if (m_InteractiblesOnList.Contains(interactible))
                    m_InteractiblesOnList.Remove(interactible);
            }
        }
        if (m_InteractiblesList.Count == m_InteractiblesOnList.Count)
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);
        Debug.Log(m_InteractiblesList.Count);
        Debug.Log(m_InteractiblesOnList.Count);
    }
}
