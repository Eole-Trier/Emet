using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnkiBehaviour2 : Golem2
{
    private Rigidbody m_RigidBody;
    private bool m_Freezed;
    private PlayerMovement2 m_Player;

    // Start is called before the first frame update
    void Start()
    {
        m_Player = FindObjectOfType<PlayerMovement2>();
        m_RigidBody = GetComponent<Rigidbody>();
        m_Freezed = false;
    }

    public override void UseCapacity()
    {
        if (m_Freezed == false)
        {
            m_Freezed = true;
            m_RigidBody.constraints = RigidbodyConstraints.FreezeAll;
            m_Player.SetMoveDirection(Vector3.zero);
            transform.eulerAngles = new Vector3(90, transform.eulerAngles.y, transform.eulerAngles.z);
        }

        else
        {
            m_Freezed = false;
            m_RigidBody.constraints = RigidbodyConstraints.FreezeRotation;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
        }
    }
}
