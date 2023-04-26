using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : Interactibles
{
    public override IEnumerator OnOff() 
    {
        if (Physics.Raycast(transform.position + Vector3.down * 0.05f, Vector3.up, 0.25f))
        {
            yield return new WaitForSeconds(TimeToActive);
            IsOn = true;
        }
        IsOn = false;
        yield return null;
    }

    public override void FixedUpdate()
    {
        StartCoroutine(OnOff());
    }
}