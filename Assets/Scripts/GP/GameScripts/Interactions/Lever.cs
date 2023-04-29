
public class Lever : Interactibles
{
    public void FixedUpdate()
    {
        if (IsOn)
            Active();
        else
            Desactive();
    }

    public override void OnOff()
    {
        IsOn ^= true;
        if (IsOn)
            m_AudioManager.Play("lever_activate");
    }
}
