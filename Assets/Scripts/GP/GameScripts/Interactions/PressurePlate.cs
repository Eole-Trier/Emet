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
            m_AudioManager.m_AudioSourceList.Find(s => s.name == "pressurePlate_on").Play();
            m_AudioManager.m_AudioSourceList.Find(s => s.name == "pressurePlate_on").transform.position = transform.position;
            m_Played = true;
        }
        else if (!IsOn && m_Played)
        {
            m_AudioManager.m_AudioSourceList.Find(s => s.name == "pressurePlate_off").Play();
            m_AudioManager.m_AudioSourceList.Find(s => s.name == "pressurePlate_off").transform.position = transform.position;
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