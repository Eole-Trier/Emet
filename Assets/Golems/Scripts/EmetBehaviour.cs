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
    Vector3 m_ObjectColliderCenter;
    BoxCollider m_ObjectCollider;
    // Start is called before the first frame update
    void Start()
    {
        m_Player = FindObjectOfType<PlayerMovement>();
        m_CancelAnimator = 1f;
        m_PickupLayer = 1 << LayerMask.NameToLayer("Pickup");
        m_CarriedObject = null;
        m_JumpStrength = m_Player.GetJumpStrength();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_CarriedObject != null)
        {
            
            m_CarriedObject.transform.parent = transform;
            
        }
    }

    public override void UseCapacity()
    {
        if (m_CarriedObject != null)
            Drop();
        else 
            if (m_Player.GetIsGrounded())
                PickUp();

    }

    Component CopyComponent(Component original, GameObject destination)
    {
        System.Type type = original.GetType();
        Component copy = destination.AddComponent(type);
        // Copied fields can be restricted with BindingFlags
        System.Reflection.FieldInfo[] fields = type.GetFields();
        foreach (System.Reflection.FieldInfo field in fields)
        {
            field.SetValue(copy, field.GetValue(original));
        }
        return copy;
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
            m_CarriedObject.GetComponent<SphereCollider>().enabled = false;
            m_CarriedObject.transform.rotation = new(0, 0, 0, 1);
            m_Player.SetJumpStrength(0);
            m_CarriedObject.GetComponent<Rigidbody>().isKinematic = true;
            m_CarriedObject.transform.position = transform.position + m_ObjectDist * transform.forward;
            m_CarriedObject.transform.Translate(0, m_ObjectHeight, 0);

            BoxCollider objectCollider = m_CarriedObject.GetComponent<BoxCollider>();
            m_ObjectColliderCenter = objectCollider.center;
            m_ObjectCollider = CopyComponent(objectCollider, gameObject) as BoxCollider;
            Destroy(objectCollider);

            m_ObjectCollider.center = new(0, 1, 1);


        }

    }
    private void Drop()
    {
        m_CarriedObject.transform.parent = null;
        m_Player.SetJumpStrength(m_JumpStrength);
        m_CarriedObject.GetComponent<Rigidbody>().isKinematic = false;
        m_CarriedObject.GetComponent<SphereCollider>().enabled = true;

        CopyComponent(m_ObjectCollider, m_CarriedObject);
        Destroy(m_ObjectCollider);
        m_CarriedObject = null;
    }
}
