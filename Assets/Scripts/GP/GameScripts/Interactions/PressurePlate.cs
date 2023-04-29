using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PressurePlate : Interactibles
{
    private bool m_Played;
    public override void OnOff() {; }

    public override void FixedUpdate()
    {
        IsOn = Pressed();

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
    public bool Pressed()
    {
        Vector3 P1 = new(transform.position.x + transform.localScale.x / 2, transform.position.y, transform.position.z + transform.localScale.z / 2);
        Vector3 P2 = new(transform.position.x + transform.localScale.x / 2, transform.position.y, transform.position.z - transform.localScale.z / 2);
        Vector3 P3 = new(transform.position.x - transform.localScale.x / 2, transform.position.y, transform.position.z + transform.localScale.z / 2);
        Vector3 P4 = new(transform.position.x - transform.localScale.x / 2, transform.position.y, transform.position.z - transform.localScale.z / 2);
        Vector3 P5 = transform.position;

        if (   Physics.Raycast(P5 + Vector3.down * 0.05f, Vector3.up, 0.25f)
            || Physics.Raycast(P1 + Vector3.down * 0.05f, Vector3.up, 0.25f)
            || Physics.Raycast(P2 + Vector3.down * 0.05f, Vector3.up, 0.25f)
            || Physics.Raycast(P3 + Vector3.down * 0.05f, Vector3.up, 0.25f)
            || Physics.Raycast(P4 + Vector3.down * 0.05f, Vector3.up, 0.25f))
            return true;

        return false;
    }
}