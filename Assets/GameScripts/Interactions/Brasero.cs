using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brasero : Interactibles
{
    [SerializeField] private float m_BurnDistance;
    [SerializeField] private bool m_IsBurning;
    private ParticleSystem m_ParticleSystem;

    private void Start()
    {
        m_ParticleSystem = GetComponent<ParticleSystem>();
    }

    public override void OnOff() {;}

    public override void FixedUpdate() 
    {
        IsOn = m_IsBurning;
        Collider[] burn = Physics.OverlapSphere(transform.position, m_BurnDistance);

        foreach (Collider c in burn)
        {
            if (c.TryGetComponent(out BurningObject burningObject) && burningObject.IsBurning)
                m_IsBurning = true;

            else if (c.TryGetComponent(out BurningObject burning) && !burning.IsBurning && m_IsBurning)
                burningObject.IsBurning = true;

            else if (c.gameObject.tag == "Water")
                m_IsBurning = false;
        }

        if (m_ParticleSystem != null)
        {
            if (m_IsBurning)
                m_ParticleSystem.Play();

            else
                m_ParticleSystem.Stop();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_BurnDistance);
    }
}
