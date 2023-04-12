using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindObjects : MonoBehaviour
{
    private Rigidbody m_Rigidbody;
    private bool inWind;
    private WindZone m_WindZone;

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_WindZone = FindObjectOfType<WindZone>();
    }
    private void Update()
    {
        //TODO : verification par rapport a la position du windzone, position du transform et le forward du windzone
        // WORKING IF TRANSFORM >= 1
        if (m_WindZone.gameObject.activeSelf
            && transform.position.x >= m_WindZone.transform.forward.normalized.x
            && transform.position.y <= m_WindZone.transform.forward.normalized.y)
            inWind = true;

        else
            inWind = false;
        Debug.Log("\n");
        Debug.Log(transform.position);
        Debug.Log(m_WindZone.transform.forward.normalized);
    }
    private void FixedUpdate()
    {
        if (inWind)
            m_Rigidbody.AddForce((m_WindZone.transform.forward * m_WindZone.windMain) / Vector3.Distance(transform.position, m_WindZone.transform.position), ForceMode.Force);
        
        else
            inWind = false;
    }
}
