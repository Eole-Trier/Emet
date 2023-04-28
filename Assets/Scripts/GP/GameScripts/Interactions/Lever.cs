using UnityEngine;

public class Lever : Interactibles
{
    public override void FixedUpdate()
    {
        if (IsOn)
        {
            foreach (Mechanism m in MechanismList)
            {
                if (m.myTimer == 0 && m.gameObject.activeInHierarchy == m.IsActive)
                    m.gameObject.SetActive(!m.IsActive);
                if (m.myTimer <= 0 && m.timer > 0)
                {
                    m.gameObject.SetActive(!m.gameObject.activeInHierarchy);
                    m.myTimer = m.timer;
                }
                m.myTimer -= Time.fixedDeltaTime;
            }
        }
        else
        {
            foreach (Mechanism m in MechanismList)
            {
                m.myTimer = m.timer;
                if (m.myTimer == 0 && m.gameObject.activeInHierarchy != m.IsActive)
                    m.gameObject.SetActive(m.IsActive);
            }
        }
    }

    public override void OnOff()
    {
        IsOn ^= true;
    }
}
