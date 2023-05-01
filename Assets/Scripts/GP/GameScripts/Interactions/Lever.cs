
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
            m_AudioManager.m_AudioSourceList.Find(s => s.name == "lever_activate").Play();
            m_AudioManager.m_AudioSourceList.Find(s => s.name == "lever_activate").transform.position = transform.position;
    }
}
