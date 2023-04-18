using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;

public class EoleBehaviour : Golem
{
    [SerializeField] private float m_WindForce;
    [HideInInspector] public List<Collider> listCollider = new();
    [HideInInspector] public bool windActive;
    private PlayerSwitch m_PlayerSwitch;
    private PlayerMovement m_PlayerMovement;

    // Start is called before the first frame update
    void Start()
    {
        m_Type = Type.EOLE;
        m_CancelAnimator = false;
        windActive = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!windActive)
            return;

        foreach (Collider collider in listCollider)
        {
            if (collider.attachedRigidbody == null)
                continue;

            if (collider.TryGetComponent(out Golem golem) && collider.name == m_PlayerSwitch.golems[m_PlayerSwitch.m_CurrentGolem].name)
            {
                if (m_PlayerMovement.GetMoveDirection() == Vector3.zero)
                {
                    golem.GetComponent<Rigidbody>().AddForce((transform.forward * m_WindForce) * 12);
                }
                else
                {
                    golem.GetComponent<Rigidbody>().AddForce((transform.forward * m_WindForce) * 4);
                }
            }
            else
                collider.attachedRigidbody.AddForce(transform.forward * m_WindForce);
        }
    }

    public override IEnumerator UseCapacity(double timePressed)
    {
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
            if (other.GetComponent<Rigidbody>() != null && other.TryGetComponent(out Golem golem) && other.name == m_PlayerSwitch.golems[m_PlayerSwitch.m_CurrentGolem].name)
            {
                listCollider.Remove(other);
            }
            else if (other.GetComponent<Rigidbody>() != null)
            {
                other.GetComponent<Rigidbody>().velocity = Vector3.zero;
                listCollider.Remove(other);
            }
            else
                listCollider.Remove(other);
        }
    }
}
