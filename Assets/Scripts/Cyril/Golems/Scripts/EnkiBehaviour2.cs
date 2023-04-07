using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
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
            Debug.Log(transform.eulerAngles.y);
            transform.eulerAngles = new Vector3(90, transform.eulerAngles.y, transform.eulerAngles.z);
        }

        else
        {
            m_RigidBody.constraints = RigidbodyConstraints.FreezeRotation;
            m_Freezed = false;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);

        }
    }
}
