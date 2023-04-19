using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfritBehaviour : Golem
{
    

    // Start is called before the first frame update
    void Start()
    {
        m_Type = Type.EFRIT;
        m_InitialJumpStrength = m_JumpStrength;
        m_InitialSpeed = m_Speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override IEnumerator UseCapacity(double timePressed)
    {
        Burn();
        yield return null;
    }
    private void Burn()
    {

    }
}
