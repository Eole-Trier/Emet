using UnityEngine;

public class Eolienne : Interactibles
{
    private Collider m_Collider;
    private EoleBehaviour m_EoleBehavior;
    private bool m_Play;

    private void Start()
    {
        m_AudioManager = FindObjectOfType<AudioManager>();
        m_Collider = GetComponent<BoxCollider>();
        m_EoleBehavior = FindObjectOfType<EoleBehaviour>();
    }

    public void FixedUpdate()
    {
        if (m_EoleBehavior.listCollider.Contains(m_Collider))
        {
            IsOn = true;
            Active();

            if (!m_AudioManager.IsPlaying("eole_on") && m_AudioManager.IsPlaying("propeller_rotate"))
                m_AudioManager.Stop("propeller_rotate");

            else if (m_AudioManager.IsPlaying("eole_on") && !m_AudioManager.IsPlaying("propeller_rotate"))
                m_AudioManager.Play("propeller_rotate");
        }
        else
        {
            IsOn = false;
            Desactive();
            if (m_AudioManager.IsPlaying("propeller_rotate"))
                m_AudioManager.Stop("propeller_rotate");
        }
    }

    public override void OnOff() {}
}
