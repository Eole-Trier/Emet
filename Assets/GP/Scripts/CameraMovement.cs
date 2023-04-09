using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private PlayerMovement m_Player;
    [SerializeField] private Vector3 m_PositionFromPlayer;


    private void Start()
    {
        m_Player = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = m_Player.GetGolem().transform.position + m_PositionFromPlayer;
    }
}
