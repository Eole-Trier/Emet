using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    private PlayerMovement m_PlayerMovement;
    // Start is called before the first frame update

    private void Start()
    {
        m_PlayerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        m_PlayerMovement.IsGrounded = true;
        m_PlayerMovement.canJump = true;
    }
}
