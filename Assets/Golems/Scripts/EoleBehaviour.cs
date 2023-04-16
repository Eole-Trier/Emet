using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;

public class EoleBehaviour : Golem
{
    [SerializeField] private float m_WindForce;
    [HideInInspector] public List<Collider> m_ListCollider = new();
    [HideInInspector] public bool m_WindActive;

    // Start is called before the first frame update
    void Start()
    {
        m_CancelAnimator = 1.0f;
        m_WindActive = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_WindActive)
        {
            for (int i = 0; i < m_ListCollider.Count; i++)
            {
                if (m_ListCollider[i].attachedRigidbody != null)
                m_ListCollider[i].attachedRigidbody.AddForce(transform.forward * m_WindForce);
            }
        }
    }

    public override void UseCapacity()
    {
        m_WindActive ^= true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!m_ListCollider.Contains(other))
        {
            m_ListCollider.Add(other);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (m_ListCollider.Contains(other))
        {
            m_ListCollider.Remove(other);
        }
    }
}
