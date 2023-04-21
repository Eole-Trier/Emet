using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform m_ObjectToFollow;
    [SerializeField] private Vector3 m_PositionFromPlayer;

    // Update is called once per frame
    void Update()
    {
        transform.position = m_ObjectToFollow.transform.position + m_PositionFromPlayer;
    }
}
