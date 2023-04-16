using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interact : MonoBehaviour
{
    [SerializeField] private List<Interactibles> m_Interactibles;
    [SerializeField] private List<Mechanism> m_Mechanism;
    private bool m_Action;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(m_Action)
        {
            Interacted();
            m_Action = false;
        }
            
    }

    public void OnAction(InputAction.CallbackContext _context)
    {
        if(_context.started)
            m_Action = true;
    }

    public void Interacted()
    {
        foreach(Interactibles interactibles in m_Interactibles)
        {
            if (interactibles.tag == "Lever")
                interactibles.OnOff();
        }    
    }

    private void SortList()
    {
        m_Interactibles.Sort((Interactibles a, Interactibles b) => {
            float distanceA = (transform.position - a.transform.position).sqrMagnitude;
            float distanceB = (transform.position - b.transform.position).sqrMagnitude;
            if (distanceA == distanceB)
                return 0;
            if (distanceA < distanceB)
                return -1;
            else
                return 1;
        });
    }
}
