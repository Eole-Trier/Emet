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
    private AudioManager m_AudioManager;

    // Start is called before the first frame update
    void Start()
    {
        m_AudioManager = FindObjectOfType<AudioManager>();
        m_BoxCollider = new(GetComponents<BoxCollider>());
        m_Type = GolemType.ENKI;
        m_PlayerMovement = FindObjectOfType<PlayerMovement>();
        m_RigidBody = GetComponent<Rigidbody>();
        freezed = false;
        m_CancelAnimator = false;
        m_InitialJumpStrength = m_JumpStrength;
        m_InitialSpeed = m_Speed;
        CanJump = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_PlayerMovement.IsGrounded && !freezed)
        {
            foreach(BoxCollider collider in m_BoxCollider)
            {
                collider.material = null;
            }
        }
        else
        {
            foreach (BoxCollider collider in m_BoxCollider)
            {
                collider.material = PhysicMaterial;
            }
        }
    }

    public override IEnumerator UseCapacity(double timePressed)
    {
        if (!(transform.parent != null && transform.parent.TryGetComponent(out Golem golem)))
        {
            if (!freezed)
            {
                m_AudioManager.m_AudioSourceList.Find(s => s.name == "enki_on").Play();
                CanJump = false;
                m_BoxCollider.ForEach((box) => box.enabled ^= true);
                freezed = true;
                m_PlayerMovement.SetMoveDirection(Vector3.zero);
                m_CancelAnimator = true;
                transform.eulerAngles = new Vector3(transform.rotation.x, 180, transform.rotation.z);
                m_PlayerMovement.GetAnimator().Play("EnkiPlateform");
                yield return new WaitForSeconds(0.1f);
                m_RigidBody.constraints = RigidbodyConstraints.FreezeAll;

            }
            else
            {
                m_AudioManager.m_AudioSourceList.Find(s => s.name == "enki_off").Play();
                m_RigidBody.constraints = RigidbodyConstraints.FreezeRotation;
                freezed = false;
                m_CancelAnimator = false;
                m_BoxCollider.ForEach((box) => box.enabled ^= true);
                m_PlayerMovement.GetAnimator().Play("EnkiGolem");
                CanJump = true;
            }
            yield return null;
        }
    }

    public bool IsFreezed() => freezed;
}
