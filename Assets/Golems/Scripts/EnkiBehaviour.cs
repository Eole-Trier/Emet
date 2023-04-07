using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnkiBehaviour : Golem
{
    Rigidbody m_RigidBody;
    private bool m_Freezed;
    private PlayerMovement m_Player;

    // Start is called before the first frame update
    void Start()
    {
        m_Player = FindObjectOfType<PlayerMovement>();
        m_RigidBody = GetComponent<Rigidbody>();
        m_Freezed = false;
        m_CancelAnimator = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void UseCapacity()
    {
        if (m_Freezed == false)
        {
            m_RigidBody.constraints = RigidbodyConstraints.FreezeAll;
            m_Freezed = true;
            m_Player.SetMoveDirection(Vector3.zero);
            m_CancelAnimator = 0.0f;
        }
        else
        {
            m_RigidBody.constraints = RigidbodyConstraints.FreezeRotation;
            m_Freezed = false;
            m_CancelAnimator = 1.0f;
        }
    }
}
