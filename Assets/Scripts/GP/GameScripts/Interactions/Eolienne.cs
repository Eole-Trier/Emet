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

    public override void FixedUpdate()
    {
        if(IsOn)
            Active();
        else
            Desactive();
    }

    public override void OnOff()
    {
        if (m_EoleBehavior.windActive && m_EoleBehavior.listCollider.Contains(GetComponent<Collider>()))
        {
            IsOn = true;
        }

        else
            IsOn = false;
    }
}
