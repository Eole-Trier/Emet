using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class EmetBehaviour : Golem
{
    [SerializeField] private float m_PickUpDist = 1f;
    [SerializeField] private float m_ObjectDist = 1f;
    [SerializeField] private float m_ObjectHeight = 1f;
    private GameObject m_CarriedObject;
    private int m_PickupLayer;
    // Start is called before the first frame update
    void Start()
    {
        m_CancelAnimator = 1f;
        m_PickupLayer = 1 << LayerMask.NameToLayer("Pickup");
        m_CarriedObject = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_CarriedObject != null)
        {
            m_CarriedObject.transform.position = transform.position + m_ObjectDist * transform.forward;
            m_CarriedObject.transform.Translate(0, m_ObjectHeight, 0);
            m_CarriedObject.transform.rotation = transform.rotation;
        }
    }

    public override void UseCapacity()
    {
        if (m_CarriedObject != null)
            Drop();
        else 
            PickUp();
    }
    private void PickUp()
    {

        // Collect every pickups around. Make sure they have a collider and the layer Pickup
        Collider[] pickups = Physics.OverlapSphere(transform.position, m_PickUpDist, m_PickupLayer);

        // Find the closest
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
            m_CarriedObject.GetComponent<Rigidbody>().isKinematic = true;

    }
    private void Drop()
    {
        m_CarriedObject.GetComponent<Rigidbody>().isKinematic = false;
        m_CarriedObject = null;
    }
}
