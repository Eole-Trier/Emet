using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using static UnityEngine.ParticleSystem;

public class PlayerSwitch : MonoBehaviour
{
    [Serializable]
    public class GolemListWrapper
    {
        public List<Golem> Golems;
    }

    public List<GolemListWrapper> Rooms = new();

    //[SerializeField] public List<List<Golem>> golems;
    public int m_CurrentRoom;
    public int m_CurrentGolem;
    private PlayerMovement m_Player;
    private EoleBehaviour m_Eole;
    private SpriteRenderer m_SpriteRenderer;
    private bool m_SoundPlayed;
    private AudioManager m_AudioManager;

    private void Start()
    {
        m_AudioManager = FindObjectOfType<AudioManager>();
        m_Player = FindObjectOfType<PlayerMovement>();
        Assert.IsTrue(Rooms[m_CurrentRoom].Golems.Count != 0);
        m_Player.SetGolem(Rooms[m_CurrentRoom].Golems[m_CurrentGolem]);
        m_Eole = FindObjectOfType<EoleBehaviour>();
        m_SpriteRenderer = Rooms[m_CurrentRoom].Golems[m_CurrentGolem].GetComponentInChildren<SpriteRenderer>();
        m_SpriteRenderer.enabled = true;
        m_SoundPlayed = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Rooms[m_CurrentRoom].Golems[m_CurrentGolem].m_Type != Golem.GolemType.EOLE && Rooms[m_CurrentRoom].Golems.Contains(m_Eole))
        {
            m_Eole.EoleUpdate();
        }
        if (Rooms[m_CurrentRoom].Golems[m_CurrentGolem].m_Type == Golem.GolemType.EFRIT && !m_SoundPlayed)
        {
            m_SoundPlayed = true;
            m_AudioManager.Play("efrit_incarnate");
        }
        else if (Rooms[m_CurrentRoom].Golems[m_CurrentGolem].m_Type == Golem.GolemType.ENKI && !m_SoundPlayed)
        {
            m_SoundPlayed = true;
            m_AudioManager.Play("enki_incarnate");
        }
        else if (Rooms[m_CurrentRoom].Golems[m_CurrentGolem].m_Type == Golem.GolemType.EMET && !m_SoundPlayed)
        {
            m_SoundPlayed = true;
            m_AudioManager.Play("emet_incarnate");
        }
        else if (Rooms[m_CurrentRoom].Golems[m_CurrentGolem].m_Type == Golem.GolemType.EOLE && !m_SoundPlayed)
        {
            m_SoundPlayed = true;
            m_AudioManager.Play("eole_incarnate");
            if (m_Eole.particles != null && m_Eole.particles.isPlaying)
            {
                m_Eole.particles.Clear();
                m_Eole.particles.Stop();
                m_AudioManager.Stop("eole_on");
                m_AudioManager.Play("eole_off");
            }
        }
    }

    public void OnSwitch(InputAction.CallbackContext _context)
    {
        if (!m_Player.CanPlay)
            return;

        if (!_context.started)
            return;

        GolemSwitch();
        m_SoundPlayed = false;
    }
      
    public void GolemSwitch()
    {
        Rooms[m_CurrentRoom].Golems[m_CurrentGolem].GetComponentInChildren<SpriteRenderer>(true).enabled = false;
        if (Rooms[m_CurrentRoom].Golems.Count > 1)
        {
            
            m_CurrentGolem = (m_CurrentGolem + 1) % Rooms[m_CurrentRoom].Golems.Count;
        }
        m_Player.SetGolem(Rooms[m_CurrentRoom].Golems[m_CurrentGolem]);
        Rooms[m_CurrentRoom].Golems[m_CurrentGolem].GetComponentInChildren<SpriteRenderer>(true).enabled = true;
    }
    
}
