using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactibles : MonoBehaviour
{
    [HideInInspector] public bool IsOn { get; protected set; }

    public abstract void Update();
    public abstract void OnOff();
}