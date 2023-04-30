using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private new GameObject gameObject;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Golem golem))
        {
            if (golem.gameObject.TryGetComponent(out ObjectType obj))
            {
                gameObject.SetActive(true);
                obj.InitialPosition = obj.transform.position;
            }
        }
    }
}
