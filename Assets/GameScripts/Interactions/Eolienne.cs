using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eolienne : Interactibles
{
    private EoleBehaviour m_EoleBehavior;

    private void Start()
    {
        m_EoleBehavior = FindObjectOfType<EoleBehaviour>();
    }

    public override void OnOff()
    {
    }

    public override void Update()
    {
        if (m_EoleBehavior.m_WindActive && m_EoleBehavior.m_ListCollider.Contains(GetComponent<Collider>()))
            isOn = true;
        else
            isOn = false;
    }
}
