using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Golem : MonoBehaviour
{
    public float m_CancelAnimator;
    public bool m_Portable;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void UseCapacity();
}
