using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class Lever : Interactibles
{
    public override void FixedUpdate() {;}

    public override IEnumerator OnOff()
    {
        
        yield return new WaitForSeconds(TimeToActive);
        IsOn ^= true;
    }
}
