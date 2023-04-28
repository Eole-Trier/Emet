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
    private bool m_PlayOnce;

    private void Start()
    {
        IsOn = m_IsBurning;
        m_ParticleSystem = new (GetComponentsInChildren<ParticleSystem>());
    }

    public override void OnOff() {;}

    public override void FixedUpdate() 
    {
        if (m_IsBurning)
        {
            if (m_PlayOnce)
            {
                FindObjectOfType<AudioManager>().Play("fire_on");
                FindObjectOfType<AudioManager>().Play("fire_burning");
                m_PlayOnce = false;
            }
        }

        else
        {
            if(!m_PlayOnce) 
            {
                FindObjectOfType<AudioManager>().Stop("fire_burning");
                FindObjectOfType<AudioManager>().Play("fire_off");
                m_PlayOnce = true;
            }
        }

        IsOn = m_IsBurning;
        if (IsOn)
            Active();
        else
            Desactive();

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
