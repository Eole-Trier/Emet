using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class EfritBehaviour : Golem
{
    private BoxCollider c;
    // Start is called before the first frame update
    void Start()
    {
        m_Type = GolemType.EFRIT;
        c = GetComponent<BoxCollider>(); 
        m_InitialJumpStrength = m_JumpStrength;
        m_InitialSpeed = m_Speed;
        m_PlayerMovement = FindObjectOfType<PlayerMovement>();
        CanJump = true;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (m_PlayerMovement.IsGrounded)
        {
            c.material = null;
        }
        else
        {
            c.material = PhysicMaterial;
        }
    }


    public override IEnumerator UseCapacity(double timePressed)
    {
        yield return null;
    }
}
