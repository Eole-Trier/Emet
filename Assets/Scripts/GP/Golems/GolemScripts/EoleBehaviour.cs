using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EoleBehaviour : Golem
{
    [HideInInspector] public List<Collider> listCollider = new();
    [SerializeField] private float m_WindForceHorizontal;
    [SerializeField] private float m_WindForceVertical;
    [SerializeField] private float m_TimeBeforeIdle;
    [HideInInspector] public bool windActive;
    [HideInInspector] public ParticleSystem particles;
    private List<CapsuleCollider> m_WindCollider = new();
    private Animator m_Animator;
    private bool forward;

    // Start is called before the first frame update
    void Start()
    {
        m_Type = GolemType.EOLE;
        particles = GetComponentInChildren<ParticleSystem>();
        forward = true;
        m_CancelAnimator = false;
        windActive = false;
        m_PlayerMovement = FindObjectOfType<PlayerMovement>();
        GetComponents(m_WindCollider);
        m_Animator = GetComponent<Animator>();
        if (m_WindCollider[0].enabled == true && m_WindCollider[1].enabled == true)
        {
            if (m_WindCollider[0].direction == 2)
            {
                m_WindCollider[0].enabled = true;
                m_WindCollider[1].enabled = false;
            }
            else
            {
                m_WindCollider[0].enabled = false;
                m_WindCollider[1].enabled = true;
            }
        }
        m_InitialJumpStrength = m_JumpStrength;
        m_InitialSpeed = m_Speed;
        CanJump = true;
        
    }

    private void FixedUpdate()
    {
        BoxCollider c = GetComponent<BoxCollider>();
        if (m_PlayerMovement.IsGrounded)
            c.material = null;
        else
            c.material = PhysicMaterial;
    }

    // Update is called once per frame
    public void EoleUpdate()
    {
        if (particles != null && !particles.isPlaying)
            particles.Play();

        foreach (Collider collider in listCollider)
        {
            //if there is no rigidbody on collider we leave
            if (collider.attachedRigidbody == null)
                continue;

            Rigidbody rb = collider.attachedRigidbody;
                if (forward)
                    rb.AddForce(transform.forward * m_WindForceHorizontal);
                else
                    rb.AddForce(transform.up * m_WindForceVertical);
        }
    }

    public override IEnumerator UseCapacity(double timePressed)
    {
        listCollider.Clear();
        m_WindCollider.ForEach(collider => collider.enabled ^= true);

        if (m_PlayerMovement.IsGrounded && m_PlayerMovement.GetMoveDirection() == Vector3.zero)
        {
            forward ^= true;
            FindObjectOfType<AudioManager>().Play("eole_change");

            if (!forward)
            {
                m_Animator.Play("EoleVertical");
                m_Animator.SetBool("LookingUP", true);
            }
            else if (forward)
            {
                m_Animator.Play("EoleHorizontal");
                m_Animator.SetBool("LookingUP", false);
            }
        }
        windActive ^= true;
        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!listCollider.Contains(other))
            listCollider.Add(other);
    }
    private void OnTriggerExit(Collider other)
    {
        if (listCollider.Contains(other))
            listCollider.Remove(other);
    }
}
