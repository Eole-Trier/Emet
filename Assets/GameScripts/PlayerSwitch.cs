using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerSwitch : MonoBehaviour
{
    [SerializeField] public List<Golem> golems;
    [SerializeField] public int m_CurrentGolem;
    private PlayerMovement m_Player;

    private void Start()
    {
        m_Player = FindObjectOfType<PlayerMovement>();
        Assert.IsTrue(golems.Count != 0);
        m_Player.SetGolem(golems[m_CurrentGolem]);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnSwitch(InputAction.CallbackContext _context)
    {
       if (_context.started)
       {
            if (golems[1] != null)
            {
                m_CurrentGolem = (m_CurrentGolem + 1) % golems.Count;
                m_Player.SetGolem(golems[m_CurrentGolem]);
            }
        }
    }
}
