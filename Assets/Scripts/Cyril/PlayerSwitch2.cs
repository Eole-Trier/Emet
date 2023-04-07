using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerSwitch2 : MonoBehaviour
{
    [Tooltip("List of Golems")]
    [SerializeField] private List<Golem2> m_Golems;
    
    [SerializeField] private int m_CurrentGolem;
    
    [Tooltip("PlayerMovement Script")]
    [SerializeField] private PlayerMovement2 m_Player;

    private void Start()
    {
        Assert.IsTrue(m_Golems.Count != 0);
        m_Player.SetGolem(m_Golems[0]);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnSwitch(InputAction.CallbackContext _context)
    {
       if (_context.started)
       {
            m_CurrentGolem = (m_CurrentGolem + 1) % m_Golems.Count;
            m_Player.SetGolem(m_Golems[m_CurrentGolem]);
        }
    }
}
