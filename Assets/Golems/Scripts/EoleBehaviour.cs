using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;

public class EoleBehaviour : Golem
{
    Rigidbody m_RigidBody;
    private bool m_Helicopter;
    private PlayerMovement m_Player;

    // Start is called before the first frame update
    void Start()
    { 
        m_RigidBody = GetComponent<Rigidbody>();
        m_CancelAnimator = 1.0f;
        m_Helicopter = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Helicopter)
        {

        }
    }

    public override void UseCapacity()
    {
        m_Helicopter ^= true;
    }
}
