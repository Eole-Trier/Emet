using System.Reflection;
using UnityEngine;

public class Button : Interactibles
{
    
    private void FixedUpdate()
    {
        if(IsOn)
            OnOff();
    }

    public override void OnOff()
    {
        m_AudioManager.m_AudioSourceList.Find(s => s.name == "button_pressed").transform.position = transform.position;
        IsOn = true;
        foreach (Mechanism m in MechanismList)
        {
            if (m.myTimer <= 0)
            {
                m.gameObject.SetActive(!m.gameObject.activeInHierarchy);
                IsOn = false;
                m.myTimer = m.timer;
            }
            if (m.myTimer > 0)
                m.myTimer -= Time.fixedDeltaTime;
        }
    }
}
