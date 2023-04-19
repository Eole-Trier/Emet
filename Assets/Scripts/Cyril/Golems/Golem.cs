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

    public float m_Speed;
    public float m_JumpStrength;
    [HideInInspector] public float m_InitialSpeed;
    [HideInInspector] public float m_InitialJumpStrength;
    [HideInInspector] public bool m_CancelAnimator;
    [HideInInspector] public Type m_Type;

    public abstract IEnumerator UseCapacity(double timePressed);

}
