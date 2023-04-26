using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ObjectType;

public class Brasero : Interactibles
{
    [SerializeField] private float m_BurnDistance;
    [SerializeField] private bool m_IsBurning;
    private List<ParticleSystem> m_ParticleSystem;
    private BurningObject m_BurningObject;
    private void Start()
    {
        m_ParticleSystem = new (GetComponentsInChildren<ParticleSystem>());
    }

    public override IEnumerator OnOff() 
    {
        yield return null;
    }

    public override void FixedUpdate() 
    {
        IsOn = m_IsBurning;
        Collider[] burn = Physics.OverlapSphere(transform.position, m_BurnDistance);

        foreach (Collider c in burn)
        {
            if (c.TryGetComponent(out BurningObject burningObject))
            {
                m_BurningObject = c.GetComponent<BurningObject>();

                if (m_BurningObject.IsBurning)
                    m_IsBurning = true;

                else if (!m_BurningObject.IsBurning && m_IsBurning)
                    m_BurningObject.IsBurning = true;

                else if (c.gameObject.tag == "Water")
                    m_IsBurning = false;

                if (m_ParticleSystem != null)
                {
                    if (m_IsBurning)
                        m_ParticleSystem.ForEach((flame) => flame.Play());
                    
                    else
                        m_ParticleSystem.ForEach((flame) => flame.Stop());
                }
            }

        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_BurnDistance);
    }
}
