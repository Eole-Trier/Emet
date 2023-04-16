using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : Interactibles
{
    private PlayerMovement m_PlayerMovement;

    private void Start()
    {
        m_PlayerMovement = FindObjectOfType<PlayerMovement>();
    }

    public override void Update()
    {
        return;
    }

    public override void OnOff()
    {
        if (Vector3.Distance(m_PlayerMovement.transform.position, transform.position) < 1.5f)
            isOn ^= true;
    }
}
