using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class EoleBehaviour : Golem
{
    [HideInInspector] public List<Collider> listCollider = new();
    [SerializeField] private float m_WindForceHorizontal;
    [SerializeField] private float m_WindForceVertical;
    [SerializeField] private float m_TimeBeforeIdle;
    [HideInInspector] public bool windActive;
    private float m_IdleTimer;
    private List<CapsuleCollider> m_WindCollider = new();
    private PlayerSwitch m_PlayerSwitch;
    private PlayerMovement m_PlayerMovement;
    private bool forward;

    // Start is called before the first frame update
    void Start()
    {
        forward = true;
        m_Type = Type.EOLE;
        m_CancelAnimator = false;
        windActive = false;
        m_PlayerSwitch = FindObjectOfType<PlayerSwitch>();
        m_PlayerMovement = FindObjectOfType<PlayerMovement>();
        GetComponents(m_WindCollider);
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
    }

    private void FixedUpdate()
    {
        if (m_PlayerMovement.GetMoveDirection() == Vector3.zero)
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

    // Update is called once per frame
    public void EoleUpdate()
    {
        foreach (Collider collider in listCollider)
        {
            //if there is no rigidbody on collider we leave
            if (collider.attachedRigidbody == null)
                continue;

            Rigidbody rb = collider.attachedRigidbody;
            if (collider.TryGetComponent(out Golem golem) && collider.name == m_PlayerSwitch.golems[m_PlayerSwitch.m_CurrentGolem].name)
            {
                if(m_PlayerMovement.GetMoveDirection() == Vector3.zero)
                //if wind is in front of eole
                if (forward)
                {
                    if (m_PlayerMovement.GetMoveDirection() == Vector3.zero)
                    {
                        rb.AddForce((transform.forward * m_WindForceHorizontal) * 12);
                    }
                    else
                    {
                        rb.AddForce((transform.forward * m_WindForceHorizontal) * 4);
                    }
                }

                //if wind is above eole
                else
                {
                    if (m_PlayerMovement.GetMoveDirection() == Vector3.zero)
                    {
                        rb.AddForce((transform.up * m_WindForceVertical) * 8);
                    }
                    else
                    {
                        rb.AddForce((transform.up * m_WindForceVertical) * 4);
                    }
                }
            }
            else
            {
                if (forward)
                    rb.AddForce(transform.forward * m_WindForceHorizontal);
                else
                    rb.AddForce(transform.up * m_WindForceVertical);
            }
        }
    }

    public override IEnumerator UseCapacity(double timePressed)
    {
        listCollider.Clear();
        m_WindCollider[0].enabled ^= true;
        m_WindCollider[1].enabled ^= true;
        if (m_PlayerMovement.GetIsGrounded() && m_PlayerMovement.GetMoveDirection() == Vector3.zero)
        {
            forward ^= true;
            if (!forward)
            {
                m_PlayerMovement.GetAnimator().Play("EoleVertical");
                m_PlayerMovement.GetAnimator().SetBool("LookingUP", true);
            }
            else if (forward)
            {
                m_PlayerMovement.GetAnimator().Play("EoleHorizontal");
                m_PlayerMovement.GetAnimator().SetBool("LookingUP", false);
            }
        }
        windActive ^= true;
        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!listCollider.Contains(other))
        {
            listCollider.Add(other);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (listCollider.Contains(other))
        {
            if (other.GetComponent<Rigidbody>() != null && other.TryGetComponent(out Golem golem) && other == m_PlayerSwitch.golems[m_PlayerSwitch.m_CurrentGolem])
            {
                listCollider.Remove(other);
            }
            else if (other.GetComponent<Rigidbody>() != null)
            {
                //other.GetComponent<Rigidbody>().velocity = Vector3.zero;
                listCollider.Remove(other);
            }
            else
                listCollider.Remove(other);
        }
    }
}
