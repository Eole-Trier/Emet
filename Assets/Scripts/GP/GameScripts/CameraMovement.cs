using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Vector3 m_PositionFromPlayer;
    private PlayerMovement m_Player;

    private void Start()
    {
        m_Player =  FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(m_Player.GetGolem().transform.position.x + m_PositionFromPlayer.x, m_PositionFromPlayer.y,m_PositionFromPlayer.z);
    }
}
