using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class EoleBehaviour : Golem
{
    Rigidbody m_RigidBody;
    private bool m_Helicopter;
    private PlayerMovement m_Player;
    [SerializeField] private WindZone m_WindZone;

    // Start is called before the first frame update
    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();
        m_Player = FindObjectOfType<PlayerMovement>();
        m_Helicopter = false;
        m_CancelAnimator = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Helicopter)
        {
            m_WindZone.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            m_WindZone.transform.forward = transform.forward;
        }
        else
            m_WindZone.gameObject.SetActive(false);
    }

    public override void UseCapacity()
    {
        m_WindZone.gameObject.SetActive(true);
        m_Helicopter ^= true;
    }
}
