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
    private EoleBehaviour m_Eole;

    private void Start()
    {
        m_Player = FindObjectOfType<PlayerMovement>();
        Assert.IsTrue(golems.Count != 0);
        m_Player.SetGolem(golems[m_CurrentGolem]);
        m_Eole = FindObjectOfType<EoleBehaviour>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (golems[m_CurrentGolem].m_Type != Golem.GolemType.EOLE && golems.Contains(m_Eole))
            m_Eole.EoleUpdate();
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
