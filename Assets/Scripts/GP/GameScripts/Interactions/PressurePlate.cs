using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : Interactibles
{
    public override void OnOff() {; }

    public override void FixedUpdate()
    {
        IsOn = Physics.Raycast(transform.position + Vector3.down * 0.05f, Vector3.up, 0.25f);
    }
}