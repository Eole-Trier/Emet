using UnityEngine;

public class Lever : Interactibles
{
    public override void FixedUpdate()
    {
        if (IsOn)
            Active();
        else
            Desactive();
    }

    public override void OnOff()
    {
        IsOn ^= true;
    }
}
