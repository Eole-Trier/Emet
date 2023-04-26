using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PressurePlate : Interactibles
{
    private void Start()
    {
        m_Timer = TimeToActivate;
    }

    public override void OnOff() {; }

    public override void FixedUpdate()
    {
        if (Physics.Raycast(transform.position + Vector3.down * 0.05f, Vector3.up, 0.25f))
        {
            IsOn = true;
        }
        else
        {
            if (m_Timer <= 0)
            {
                IsOn = false;
                m_Timer = TimeToActivate;
            }
            else
                m_Timer -= Time.fixedDeltaTime; 
        }
    }
}