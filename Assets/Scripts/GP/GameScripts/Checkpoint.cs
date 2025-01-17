using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Golem golem))
        {
            if (golem.gameObject.TryGetComponent(out ObjectType obj))
            {
                obj.InitialPosition = obj.transform.position;
            }
        }
    }
}
