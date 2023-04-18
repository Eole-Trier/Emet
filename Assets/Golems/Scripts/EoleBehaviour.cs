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
    [HideInInspector] public bool windActive;
    private List<CapsuleCollider> m_WindCollider = new();
    private PlayerSwitch m_PlayerSwitch;
    private PlayerMovement m_PlayerMovement;
    private bool forward;

    // Start is called before the first frame update
    void Start()
    {
        forward = true;
        m_Type = Type.EOLE;
        m_CancelAnimator = 1.0f;
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
    }

    // Update is called once per frame
    public void EoleUpdate()
    {
        foreach (Collider collider in listCollider)
        {
            //if there is no rigidbody on collider we leave
            if (collider.attachedRigidbody == null)
                continue;

            //if wind is in front of eole
            if (collider.TryGetComponent(out Golem golem) && collider.name == m_PlayerSwitch.golems[m_PlayerSwitch.m_CurrentGolem].name)
            {
                if (forward)
                {
                    if (m_PlayerMovement.GetMoveDirection() == Vector3.zero)
                    {
                        golem.GetComponent<Rigidbody>().AddForce((transform.forward * m_WindForceHorizontal) * 12);
                    }
                    else
                    {
                        golem.GetComponent<Rigidbody>().AddForce((transform.forward * m_WindForceHorizontal) * 4);
                    }
                }

                //if wind is above eole
                else
                {
                    if (m_PlayerMovement.GetMoveDirection() == Vector3.zero)
                    {
                        golem.GetComponent<Rigidbody>().AddForce((transform.up * m_WindForceVertical) * 8);
                    }
                    else
                    {
                        golem.GetComponent<Rigidbody>().AddForce((transform.up * m_WindForceVertical) * 4);
                    }
                }
            }
            else
            {
                if(forward)
                    collider.attachedRigidbody.AddForce(transform.forward * m_WindForceHorizontal);
                else
                    collider.attachedRigidbody.AddForce(transform.up * m_WindForceVertical);
            }
        }
    }

    public override void UseCapacity(double timePressed)
    {
        listCollider.Clear();
        m_WindCollider[0].enabled ^= true;
        m_WindCollider[1].enabled ^= true;
        forward ^= true;
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
