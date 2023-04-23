using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class Lever : Interactibles
{
    private PlayerMovement m_PlayerMovement;

    private void Start()
    {
        m_PlayerMovement = FindObjectOfType<PlayerMovement>();
    }

    public override void FixedUpdate() {;}

    public override void OnOff()
    {
        IsOn ^= true;
    }
}
