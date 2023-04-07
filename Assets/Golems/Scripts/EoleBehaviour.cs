using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EoleBehaviour : Golem
{
    Rigidbody m_RigidBody;
    private bool m_Helicopter;
    private PlayerMovement m_Player;

    // Start is called before the first frame update
    void Start()
    {
        m_Player = FindObjectOfType<PlayerMovement>();
        m_RigidBody = GetComponent<Rigidbody>();
        m_Helicopter = false;
        m_CancelAnimator = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Helicopter)
        {
            transform.Rotate(0, 0, 10);
            transform.position += new Vector3(0, 0.1f, 0);
        }

    }

    public override void UseCapacity()
    {
        if (m_Helicopter == false)
        {
            m_RigidBody.constraints = RigidbodyConstraints.FreezeAll;
            m_Helicopter = true;
            m_Player.SetMoveDirection(Vector3.zero);
            m_CancelAnimator = 0.0f;
            transform.Rotate(-90, 0, 0);

        }
        else
        {
            m_RigidBody.constraints = RigidbodyConstraints.FreezeRotation;
            m_Helicopter = false;
            m_CancelAnimator = 1.0f;
        }
    }
}
