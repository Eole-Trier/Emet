using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.EventSystems;

public class EmetBehaviour : Golem
{
    private PlayerMovement m_Player;

    [SerializeField] private float m_PickUpDist = 1f;
    [SerializeField] private float m_ObjectDist = 1f;
    [SerializeField] private float m_ObjectHeight = 1f;
    private float m_JumpStrength;
    private GameObject m_CarriedObject;
    private int m_PickupLayer;

    [SerializeField]
    BoxCollider m_ObjectCollider;

    // Start is called before the first frame update
    void Start()
    {
        m_Player = FindObjectOfType<PlayerMovement>();
        m_CancelAnimator = 1f;
        m_PickupLayer = 1 << LayerMask.NameToLayer("Pickup");
        m_CarriedObject = null;
        m_JumpStrength = m_Player.GetJumpStrength();

        // m_ObjectCollider = new();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_CarriedObject != null)
        {
            
            m_CarriedObject.transform.parent = transform;
            
        }
    }

    public override void UseCapacity(double timePressed)
    {
        if (m_CarriedObject != null)
        {
            Drop();
            return;
        }

        if (m_Player.GetIsGrounded())
            PickUp();
    }

    private void PickUp()
    {
        Collider[] pickups = Physics.OverlapSphere(transform.position, m_PickUpDist, m_PickupLayer);

        float dist = Mathf.Infinity;
        for (int i = 0; i < pickups.Length; i++)
        {
            float newDist = (transform.position - pickups[i].transform.position).sqrMagnitude;
            if (newDist < dist)
            {
                m_CarriedObject = pickups[i].gameObject;

                dist = newDist;
            }
        }

        if (m_CarriedObject != null)
        {
            m_CarriedObject.transform.rotation = Quaternion.identity;
            m_CarriedObject.transform.localPosition = Vector3.zero;
            m_Player.SetJumpStrength(0);

            m_CarriedObject.GetComponent<Rigidbody>().isKinematic = true;
            m_CarriedObject.transform.position = transform.position + m_ObjectDist * transform.forward;
            m_CarriedObject.transform.Translate(0, m_ObjectHeight, 0);

            BoxCollider objectCollider = m_CarriedObject.GetComponent<BoxCollider>();

            m_ObjectCollider.enabled = true;
            m_ObjectCollider.center = new(0, m_ObjectHeight, 0);
            m_ObjectCollider.size = objectCollider.size;

            objectCollider.enabled = false;

            m_ObjectCollider.center = new(0, m_ObjectHeight, 0);
        }

    }
    private void Drop()
    {
        m_CarriedObject.transform.parent = null;
        m_Player.SetJumpStrength(m_JumpStrength);
        m_CarriedObject.GetComponent<Rigidbody>().isKinematic = false;

        BoxCollider objectCollider = m_CarriedObject.GetComponent<BoxCollider>();
        objectCollider.enabled = true;
        m_ObjectCollider.enabled = false;

        m_CarriedObject = null;
    }
}
