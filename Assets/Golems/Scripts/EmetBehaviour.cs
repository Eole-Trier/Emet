using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static ObjectType;

public class EmetBehaviour : Golem
{
    private PlayerMovement m_Player;

    [SerializeField] private float m_ThrowForce = 75f;
    [SerializeField] private float m_PickUpDist = 1f;
    [SerializeField] private float m_ObjectDropDistance = 1f;
    [SerializeField] private float m_ObjectDistance = 1f;
    [SerializeField] private float m_ObjectHeight = 1f;
    [SerializeField] private float m_TimeKeyPressedToThrow;
    [SerializeField] BoxCollider m_ObjectCollider;
    private GameObject m_CarriedObject;
    private int m_PickupLayer;

    private Golem m_Golem;

    private float m_GDShit;


    // Start is called before the first frame update
    void Start()
    {
        m_Type = GolemType.EMET;
        m_Player = FindObjectOfType<PlayerMovement>();
        m_CancelAnimator = false;
        m_PickupLayer = 1 << LayerMask.NameToLayer("Pickup");
        m_CarriedObject = null;
        m_InitialJumpStrength = m_JumpStrength;
        m_InitialSpeed = m_Speed;
        m_GDShit = 55.27727f;
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
            Drop(timePressed);
        else if (true)//m_Player.GetIsGrounded())
            PickUp();

        yield return null;
    }

    private void PickUp()
    {
        m_JumpStrength = 0;
        Collider[] pickups = Physics.OverlapSphere(transform.position, m_PickUpDist);

        float dist = Mathf.Infinity;
        for (int i = 0; i < pickups.Length; i++)
        {
            GameObject go = pickups[i].gameObject;
            if (go == gameObject)
                continue;

            if (go.TryGetComponent(out ObjectType type) && type.ObjType.HasFlag(Type.Pickup))
            {
                float newDist = (transform.position - pickups[i].transform.position).sqrMagnitude;
                if (newDist < dist)
                {
                    m_CarriedObject = go;

                    dist = newDist;
                }
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

            m_CarriedObject.GetComponent<Rigidbody>().isKinematic = true;
            m_CarriedObject.transform.position = transform.position + m_ObjectDistance * transform.forward;
            m_CarriedObject.transform.Translate(0, m_ObjectHeight, 0);

            BoxCollider objectCollider = m_CarriedObject.GetComponent<BoxCollider>();

            m_ObjectCollider.enabled = true;
            m_ObjectCollider.center = new(0, m_ObjectHeight * m_GDShit, 0);
            m_ObjectCollider.size = objectCollider.size * m_GDShit;

            objectCollider.enabled = false;

        }

    }
    private void Drop(double timePressed)
    {
        m_JumpStrength = m_InitialJumpStrength;
        if (m_CarriedObject.TryGetComponent(out Golem golem))
        {
            golem.m_CancelAnimator = false;
        }
        m_CarriedObject.GetComponent<Rigidbody>().isKinematic = false;

        BoxCollider objectCollider = m_CarriedObject.GetComponent<BoxCollider>();
        objectCollider.enabled = true;
        m_ObjectCollider.enabled = false;

        if (timePressed >= m_TimeKeyPressedToThrow)
        {
            Vector3 test = new Vector3(transform.forward.x * m_ThrowForce, m_ThrowForce, transform.forward.z * m_ThrowForce);
            m_CarriedObject.GetComponent<Rigidbody>().AddForce(test, ForceMode.Impulse);
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, m_PickUpDist);
    }
}