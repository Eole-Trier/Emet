using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnkiBehaviour : Golem
{
    [SerializeField] private float m_TimeBeforeIdle;
    private Rigidbody m_RigidBody;
    private PlayerMovement m_PlayerMovement;
    private float m_IdleTimer;
    private bool m_Freezed;
    private List<BoxCollider> m_BoxCollider;

    // Start is called before the first frame update
    void Start()
    {
        m_BoxCollider = new(GetComponents<BoxCollider>());
        m_Type = GolemType.ENKI;
        m_PlayerMovement = FindObjectOfType<PlayerMovement>();
        m_RigidBody = GetComponent<Rigidbody>();
        m_Freezed = false;
        m_CancelAnimator = false;
        m_InitialJumpStrength = m_JumpStrength;
        m_InitialSpeed = m_Speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_PlayerMovement.GetMoveDirection() == Vector3.zero && !m_Freezed)
        {
            m_IdleTimer -= Time.fixedDeltaTime;
            if (m_IdleTimer <= 0)
            {
                m_PlayerMovement.GetAnimator().Play("Idle");
                m_IdleTimer = m_TimeBeforeIdle;
            }
        }
        else
            m_IdleTimer = m_TimeBeforeIdle;
    }

    public override IEnumerator UseCapacity(double timePressed)
    {
        if (!m_Freezed)
        {
            m_RigidBody.constraints = RigidbodyConstraints.FreezeAll;
            m_Freezed = true;
            m_PlayerMovement.SetMoveDirection(Vector3.zero);
            m_CancelAnimator = true;
            m_BoxCollider.ForEach((box) => box.enabled ^= true);
            m_PlayerMovement.GetAnimator().Play("EnkiPlateform");
        }
        else
        {
            m_RigidBody.constraints = RigidbodyConstraints.FreezeRotation;
            m_Freezed = false;
            m_CancelAnimator = false;
            m_BoxCollider.ForEach((box) => box.enabled ^= true);
            m_PlayerMovement.GetAnimator().Play("EnkiGolem");
        }
        yield return null;
    }
    public bool IsFreezed() => m_Freezed;
}