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
        if (IsOn)
            m_AudioManager.Play("lever_activate");
        IsOn ^= true;
    }
}
