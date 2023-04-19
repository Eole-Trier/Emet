using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EfritBehaviour : Golem
{
    [SerializeField] private bool m_IsBurning;
    [SerializeField] private float m_BurnDistance;
    
    private int m_BurnableLayer;


    // Start is called before the first frame update
    void Start()
    {
        m_Type = Type.EFRIT;
        m_InitialJumpStrength = m_JumpStrength;
        m_InitialSpeed = m_Speed;
        m_BurnableLayer = 1 << LayerMask.NameToLayer("Burnable");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_IsBurning)
        {
            Burn();
        }
    }

    public override IEnumerator UseCapacity(double timePressed)
    {
        yield return null;
    }
    private void Burn()
    {
        Collider[] burn = Physics.OverlapSphere(transform.position, m_BurnDistance, m_BurnableLayer);

        foreach (Collider c in burn)
        {

            Destroy(c.gameObject);
        }
    }
}
