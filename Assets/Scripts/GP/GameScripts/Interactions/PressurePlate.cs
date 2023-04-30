using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PressurePlate : Interactibles
{
    private bool m_Played;
    public override void OnOff() {; }

    public void FixedUpdate()
    {
        if (IsOn)
            Active();

        else
            Desactive();

        if (IsOn && !m_Played)
        {
            m_AudioManager.Play("pressurePlate_on");
            m_Played = true;
        }
        else if (!IsOn && m_Played)
        {
            m_AudioManager.Play("pressurePlate_off");
            m_Played = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.position.y >= transform.position.y)
            IsOn = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        IsOn = false;
    }
}