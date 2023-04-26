using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSound : MonoBehaviour
{
    private AudioSource m_Audio;
    private Rigidbody m_Rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        m_Audio = GetComponent<AudioSource>();
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if ((!m_Audio.isPlaying && m_Rigidbody.velocity.x != 0 && m_Rigidbody.velocity.y == 0) || (!m_Audio.isPlaying && m_Rigidbody.velocity.y == 0 && m_Rigidbody.velocity.z != 0))
        {
            m_Audio.Play();
        }

        else if (m_Rigidbody.velocity.x == 0 && m_Rigidbody.velocity.y == 0 && m_Rigidbody.velocity.z == 0)
        {
            m_Audio.Stop();
        }
    }
}
