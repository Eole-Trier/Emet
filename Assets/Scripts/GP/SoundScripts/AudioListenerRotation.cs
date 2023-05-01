using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioListenerRotation : MonoBehaviour
{
    private AudioListener m_AudioListener;
    // Start is called before the first frame update
    void Start()
    {
        m_AudioListener = GetComponent<AudioListener>();
    }

    // Update is called once per frame
    void Update()
    {
        m_AudioListener.transform.eulerAngles = Vector3.zero;
    }
}
