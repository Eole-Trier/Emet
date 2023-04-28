using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnSystem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other is BoxCollider && other.gameObject.TryGetComponent(out ObjectType obj))
        {
            if (obj.ObjType.HasFlag(ObjectType.Type.Respawn))
            {
                obj.transform.position = obj.InitialPosition;
            }
        }
    }
}
