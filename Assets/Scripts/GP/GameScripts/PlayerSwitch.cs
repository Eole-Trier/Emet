using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

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
    private Golem m_Golem;
    private SpriteRenderer m_SpriteRenderer;

    private void Start()
    {
        m_Player = FindObjectOfType<PlayerMovement>();
        Assert.IsTrue(Rooms[m_CurrentRoom].Golems.Count != 0);
        m_Player.SetGolem(Rooms[m_CurrentRoom].Golems[m_CurrentGolem]);
        m_Eole = FindObjectOfType<EoleBehaviour>();
        m_Golem = m_Player.GetGolem();
        m_SpriteRenderer = Rooms[m_CurrentRoom].Golems[m_CurrentGolem].GetComponentInChildren<SpriteRenderer>();
        m_SpriteRenderer.enabled = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Rooms[m_CurrentRoom].Golems[m_CurrentGolem].m_Type != Golem.GolemType.EOLE && Rooms[m_CurrentRoom].Golems.Contains(m_Eole))
        {
            m_Eole.EoleUpdate();
        }
    }

    public void OnSwitch(InputAction.CallbackContext _context)
    {
       if (_context.started)
       {
            if (Rooms[m_CurrentRoom].Golems[1] != null)
            {
                Rooms[m_CurrentRoom].Golems[m_CurrentGolem].GetComponentInChildren<SpriteRenderer>(true).enabled = false;
                m_CurrentGolem = (m_CurrentGolem + 1) % Rooms[m_CurrentRoom].Golems.Count;
                m_Player.SetGolem(Rooms[m_CurrentRoom].Golems[m_CurrentGolem]);
                Rooms[m_CurrentRoom].Golems[m_CurrentGolem].GetComponentInChildren<SpriteRenderer>(true).enabled = true;
            }
        }
    }
    
}
