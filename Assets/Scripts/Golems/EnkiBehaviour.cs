using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnkiBehaviour : MonoBehaviour
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCapacity(InputAction.CallbackContext _context)
    {
        if (m_Freezed == false)
        {
            m_RigidBody.constraints = RigidbodyConstraints.FreezeAll;
            m_Freezed = true;
            m_Player.SetSpeed(0);
        }
        else
        {
            m_RigidBody.constraints = RigidbodyConstraints.FreezeRotation;
            m_Freezed = false;
            m_Player.SetSpeed(m_Player.GetInitalSpeed());

        }
    }
}
