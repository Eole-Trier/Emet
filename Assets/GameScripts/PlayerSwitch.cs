using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerSwitch : MonoBehaviour
{
    [SerializeField] private List<Golem> m_Golems;
    [SerializeField] public int m_CurrentGolem;
    private PlayerMovement m_Player;

    private void Start()
    {
        m_Player = FindObjectOfType<PlayerMovement>();
        Assert.IsTrue(m_Golems.Count != 0);
        m_Player.SetGolem(m_Golems[m_CurrentGolem]);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnSwitch(InputAction.CallbackContext _context)
    {
       if (_context.started)
       {
            if (m_Golems[1] != null)
            {
                m_CurrentGolem = (m_CurrentGolem + 1) % m_Golems.Count;
                m_Player.SetGolem(m_Golems[m_CurrentGolem]);
            }
        }
    }
}
