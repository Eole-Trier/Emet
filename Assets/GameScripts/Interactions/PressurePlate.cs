using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : Interactibles
{
    [SerializeField] private Renderer Object;
    [SerializeField] private Material Material1;
    [SerializeField] private Material Material2;

    public override void OnOff()
    {
        return;
    }

    public override void Update()
    {
        isOn = Physics.Raycast(transform.position + Vector3.down * 0.05f, Vector3.up, 0.25f);
        if (isOn)
            Object.material = Material1;
        else
            Object.material = Material2;
    }
}
