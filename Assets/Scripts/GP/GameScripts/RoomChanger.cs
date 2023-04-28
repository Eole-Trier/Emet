using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomChanger : MonoBehaviour
{
    private PlayerSwitch m_PlayerSwitch;
    [SerializeField] private CinemachineVirtualCamera[] m_CameraList = new CinemachineVirtualCamera[2];
    private int m_ActualCamera;

    // Start is called before the first frame update
    void Start()
    {
        m_PlayerSwitch = FindObjectOfType<PlayerSwitch>();
        m_ActualCamera = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Golem golem))
        {
            if (m_PlayerSwitch.m_CurrentRoom + 1 < m_PlayerSwitch.Rooms.Count)
            {
                if (m_CameraList.Length != 0 && m_ActualCamera < m_CameraList.Length)
                {
                    m_CameraList[m_ActualCamera].enabled = false;
                    m_ActualCamera += 1;
                    m_CameraList[m_ActualCamera].enabled = true;
                }
                m_PlayerSwitch.m_CurrentRoom += 1;
                m_PlayerSwitch.m_CurrentGolem = m_PlayerSwitch.Rooms[m_PlayerSwitch.m_CurrentRoom].Golems.Count-1;
            }
            else
            {
                //change level
            }
            gameObject.SetActive(false);
        }
        
    }
}
