using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Golem : MonoBehaviour
{
    [HideInInspector]
    public enum GolemType
    {
        EMET,
        ENKI,
        EOLE,
        EFRIT
    }

    public float m_Speed;
    public float m_JumpStrength;
    public PhysicMaterial PhysicMaterial;
    [HideInInspector] public float m_InitialSpeed;
    [HideInInspector] public float m_InitialJumpStrength;
    [HideInInspector] public bool m_CancelAnimator;
    [HideInInspector] public GolemType m_Type;
    [HideInInspector] public PlayerMovement m_PlayerMovement;
    [HideInInspector] public bool CanJump;


    public abstract IEnumerator UseCapacity(double timePressed);

    private void Start()
    {
        m_PlayerMovement = FindObjectOfType<PlayerMovement>();
    }


}
