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
