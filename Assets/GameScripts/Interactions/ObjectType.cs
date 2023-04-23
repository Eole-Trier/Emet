using System;
using UnityEngine;

public class ObjectType : MonoBehaviour
{
    public Type ObjType;

    [Flags]
    public enum Type
    {
        Pickup = 1,
        Burnable = 2,
        Checkpoint = 4,
    }
}
