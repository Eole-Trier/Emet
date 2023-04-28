using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnSystem : MonoBehaviour
{
    [SerializeField] private float m_RespawnTime;
    private void OnTriggerEnter(Collider other)
    {
        if (other is BoxCollider && other.gameObject.TryGetComponent(out ObjectType obj))
        {
            if (obj.ObjType.HasFlag(ObjectType.Type.Respawn))
            {
                if (obj.TryGetComponent(out Golem golem))
                {
                    Die();
                }
                obj.transform.position = obj.InitialPosition;
            }
        }
    }

    private IEnumerator Die()
    {
        // add sound
        yield return new WaitForSeconds(m_RespawnTime);
    }
}

