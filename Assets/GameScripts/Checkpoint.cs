using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private PlayerSwitch m_PlayerSwitch;
    // Start is called before the first frame update
    void Start()
    {
        m_PlayerSwitch = FindObjectOfType<PlayerSwitch>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Golem golem))
        {
            if (m_PlayerSwitch.m_CurrentRoom + 1 < m_PlayerSwitch.Rooms.Count)
            {
                m_PlayerSwitch.m_CurrentRoom += 1;
                m_PlayerSwitch.m_CurrentGolem = m_PlayerSwitch.Rooms[m_PlayerSwitch.m_CurrentRoom].Golems.Count-1;
            }
            else
            {
                //change level
            }
        }
    }
}
