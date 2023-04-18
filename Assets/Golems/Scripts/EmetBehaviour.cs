using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.EventSystems;

public class EmetBehaviour : Golem
{
    private PlayerMovement m_Player;

    [SerializeField] private float m_ThrowForce = 100f;
    [SerializeField] private float m_PickUpDist = 1f;
    [SerializeField] private float m_ObjectDropDistance = 1f;
    [SerializeField] private float m_ObjectDistance = 1f;
    [SerializeField] private float m_ObjectHeight = 1f;
    [SerializeField] private float m_TimeKeyPressedToThrow;
    private float m_JumpStrength;
    private GameObject m_CarriedObject;
    private int m_PickupLayer;

    private Golem m_Golem;
    [SerializeField]
    BoxCollider m_ObjectCollider;

    // Start is called before the first frame update
    void Start()
    {
        m_Type = Type.EMET;
        m_Player = FindObjectOfType<PlayerMovement>();
        m_CancelAnimator = false;
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

    public override IEnumerator UseCapacity(double timePressed)
    {
        if (m_CarriedObject != null)
        {
            Drop(timePressed);
            yield return null;
        }

        if (m_Player.GetIsGrounded())
            PickUp();
        
        yield return null;
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
            if (m_CarriedObject.TryGetComponent(out Golem golem))
            {
                golem.m_CancelAnimator = true;
                if (m_CarriedObject.TryGetComponent(out EnkiBehaviour enki))
                {
                    if (enki.IsFreezed())
                    {
                        m_CarriedObject = null;
                        return;
                    }
                }
            }
            m_CarriedObject.transform.rotation = Quaternion.identity;
            m_CarriedObject.transform.localPosition = Vector3.zero;
            m_Player.SetJumpStrength(0);

            m_CarriedObject.GetComponent<Rigidbody>().isKinematic = true;
            m_CarriedObject.transform.position = transform.position + m_ObjectDistance * transform.forward;
            m_CarriedObject.transform.Translate(0, m_ObjectHeight, 0);

            BoxCollider objectCollider = m_CarriedObject.GetComponent<BoxCollider>();

            m_ObjectCollider.enabled = true;
            m_ObjectCollider.center = new(0, m_ObjectHeight, 0);
            m_ObjectCollider.size = objectCollider.size;

            objectCollider.enabled = false;

            m_ObjectCollider.center = new(0, m_ObjectHeight, 0);
        }

    }
    private void Drop(double timePressed)
    {

        if (m_CarriedObject.TryGetComponent(out Golem golem))
        {
            golem.m_CancelAnimator = false;
        }
        m_Player.SetJumpStrength(m_JumpStrength);
        m_CarriedObject.GetComponent<Rigidbody>().isKinematic = false;

        BoxCollider objectCollider = m_CarriedObject.GetComponent<BoxCollider>();
        objectCollider.enabled = true;
        m_ObjectCollider.enabled = false;

        if (timePressed >= m_TimeKeyPressedToThrow)
        {
            m_CarriedObject.GetComponent<Rigidbody>().AddForce(transform.forward * m_ThrowForce, ForceMode.Impulse);
        }
        else
        {
            Vector3 position = m_CarriedObject.transform.position;
            Vector3 offset = new(transform.forward.x * m_ObjectDropDistance, 0, transform.forward.z);
            m_CarriedObject.transform.position = position + offset;
        }
        m_CarriedObject.transform.parent = null;

        m_CarriedObject = null;
    }
}
