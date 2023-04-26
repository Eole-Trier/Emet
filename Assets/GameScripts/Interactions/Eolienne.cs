using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eolienne : Interactibles
{
    private EoleBehaviour m_EoleBehavior;

    private void Start()
    {
        m_EoleBehavior = FindObjectOfType<EoleBehaviour>();
    }

    public override IEnumerator OnOff()
    {
        if (m_EoleBehavior.windActive && m_EoleBehavior.listCollider.Contains(GetComponent<Collider>()))
        {
            yield return new WaitForSeconds(TimeToActive);
            IsOn = true;
        }
        IsOn = false;
        yield return null;
    }

    public override void FixedUpdate()
    {
        StartCoroutine(OnOff());
    }
}
