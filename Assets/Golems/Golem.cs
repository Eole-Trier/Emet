using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Golem : MonoBehaviour
{
    [HideInInspector]
    public enum Type
    {
        EMET,
        ENKI,
        EOLE,
        EFRIT
    }

    [HideInInspector] public bool m_CancelAnimator;
    [HideInInspector] public Type m_Type;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract IEnumerator UseCapacity(double timePressed);
}
