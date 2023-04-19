using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interact : MonoBehaviour
{
    [SerializeField] public List<Interactibles> m_Interactibles;
    [SerializeField] private List<Mechanism> m_Mechanism;
    [HideInInspector] public bool action;
    public float rangeToActivate;

    // Start is called before the first frame update
    void Start()
    {
        action = false;
    }

    // Update is called once per frame
    void Update()
    {
        SortList();
        if(action)
        {
            Interacted();
            action = false;
        }
    }

    public void Interacted()
    {
        foreach(Interactibles interactibles in m_Interactibles)
        {
            if (interactibles.tag == "Interactible")
                interactibles.OnOff();
        }    
    }

    public void SortList()
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
