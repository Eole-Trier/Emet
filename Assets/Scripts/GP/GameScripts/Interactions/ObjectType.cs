using System;
using UnityEngine;

public class ObjectType : MonoBehaviour
{
    public Type ObjType;
    [HideInInspector] public Vector3 InitialPosition;

    [Flags]
    public enum Type
    {
        Pickup = 1,
        Burnable = 2,
        Respawn = 4,
    }
    private void Start()
    {
        InitialPosition = transform.position;
    }
}
