using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnkiBehaviour : Golem
{
    [SerializeField] private float m_TimeBeforeIdle;
    private Rigidbody m_RigidBody;
    private float m_IdleTimer;
    [HideInInspector] public bool freezed;
    private List<BoxCollider> m_BoxCollider;
  

    // Start is called before the first frame update
    void Start()
    {
        m_BoxCollider = new(GetComponents<BoxCollider>());
        m_Type = GolemType.ENKI;
        m_PlayerMovement = FindObjectOfType<PlayerMovement>();
        m_RigidBody = GetComponent<Rigidbody>();
        freezed = false;
        m_CancelAnimator = false;
        m_InitialJumpStrength = m_JumpStrength;
        m_InitialSpeed = m_Speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_PlayerMovement.IsGrounded)
        {
            foreach(var collider in m_BoxCollider)
            {
                collider.material = null;
            }
        }
        else
        {
            foreach (var collider in m_BoxCollider)
            {
                collider.material = PhysicMaterial;
            }
        }
        /*if (m_PlayerMovement.GetMoveDirection() == Vector3.zero && !m_Freezed)
        {
            m_IdleTimer -= Time.fixedDeltaTime;
            if (m_IdleTimer <= 0)
            {
                m_PlayerMovement.GetAnimator().Play("Idle");
                m_IdleTimer = m_TimeBeforeIdle;
            }
        }
        else
            m_IdleTimer = m_TimeBeforeIdle;*/
    }

    public override IEnumerator UseCapacity(double timePressed)
    {
        if (!(transform.parent != null && transform.parent.TryGetComponent(out Golem golem)))
        {
            if (!freezed)
            {
                m_RigidBody.constraints = RigidbodyConstraints.FreezeAll;
                freezed = true;
                m_PlayerMovement.SetMoveDirection(Vector3.zero);
                m_CancelAnimator = true;
                m_BoxCollider.ForEach((box) => box.enabled ^= true);
                transform.eulerAngles = new Vector3(transform.rotation.x, 180, transform.rotation.z);
                m_PlayerMovement.GetAnimator().Play("EnkiPlateform");
            }
            else
            {
                m_RigidBody.constraints = RigidbodyConstraints.FreezeRotation;
                freezed = false;
                m_CancelAnimator = false;
                m_BoxCollider.ForEach((box) => box.enabled ^= true);
                m_PlayerMovement.GetAnimator().Play("EnkiGolem");
            }
            yield return null;
        }
    }

    public bool IsFreezed() => freezed;
}
