using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement2 : MonoBehaviour
{
    [SerializeField] private PlayerMovement2 m_Player;
    [SerializeField] private Vector3 m_PositionFromPlayer;

    // Update is called once per frame
    void Update()
    {
        transform.position = m_Player.GetGolem().transform.position + m_PositionFromPlayer;
    }
}
