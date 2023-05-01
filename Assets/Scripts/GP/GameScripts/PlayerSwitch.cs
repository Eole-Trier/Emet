using System;
using System.Collections.Generic;
using System.Linq;
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
    private bool m_SoundPlayed;
    private PlayerMovement m_Player;
    private EoleBehaviour m_Eole;
    private SpriteRenderer m_SpriteRenderer;
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
            foreach(Golem g in Rooms[m_CurrentRoom].Golems)
            {
                if (g.TryGetComponent(out EoleBehaviour eole))
                {
                    if (Rooms[m_CurrentRoom].Golems[m_CurrentGolem] != g)
                    {
                        eole.EoleUpdate();
                    }
                    else
                    {
                        eole.particles.Clear();
                        eole.particles.Stop();
                    }
                }
            }

        if (Rooms[m_CurrentRoom].Golems[m_CurrentGolem].m_Type == Golem.GolemType.EFRIT && !m_SoundPlayed)
        {
            m_SoundPlayed = true;
            m_AudioManager.m_AudioSourceList.Find(s => s.name == "efrit_incarnate").transform.position = transform.position;
            m_AudioManager.m_AudioSourceList.Find(s => s.name == "efrit_incarnate").Play();

        }
        else if (Rooms[m_CurrentRoom].Golems[m_CurrentGolem].m_Type == Golem.GolemType.ENKI && !m_SoundPlayed)
        {
            m_SoundPlayed = true;
            m_AudioManager.m_AudioSourceList.Find(s => s.name == "enki_incarnate").transform.position = transform.position;
            m_AudioManager.m_AudioSourceList.Find(s => s.name == "enki_incarnate").Play();

        }
        else if (Rooms[m_CurrentRoom].Golems[m_CurrentGolem].m_Type == Golem.GolemType.EMET && !m_SoundPlayed)
        {
            m_SoundPlayed = true;
            m_AudioManager.m_AudioSourceList.Find(s => s.name == "emet_incarnate").transform.position = transform.position;
            m_AudioManager.m_AudioSourceList.Find(s => s.name == "emet_incarnate").Play();

        }
        else if (Rooms[m_CurrentRoom].Golems[m_CurrentGolem].m_Type == Golem.GolemType.EOLE && !m_SoundPlayed)
        {
            m_SoundPlayed = true;
            m_AudioManager.m_AudioSourceList.Find(s => s.name == "eole_incarnate").transform.position = transform.position;
            m_AudioManager.m_AudioSourceList.Find(s => s.name == "eole_incarnate").Play();
        }

        if (Rooms[m_CurrentRoom].Golems.Count > 0)
        {
            foreach (Golem golem in Rooms[m_CurrentRoom].Golems)
            {
                Animator m_GolemAnimator = golem.GetComponent<Animator>();
                m_GolemAnimator.SetFloat("SpeedX", golem.GetComponent<Rigidbody>().velocity.x);
                m_GolemAnimator.SetFloat("SpeedY", golem.GetComponent<Rigidbody>().velocity.y);
                m_GolemAnimator.SetFloat("SpeedZ", golem.GetComponent<Rigidbody>().velocity.z);
                m_GolemAnimator.SetBool("Moving", golem.GetComponent<Rigidbody>().velocity != Vector3.zero);
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
