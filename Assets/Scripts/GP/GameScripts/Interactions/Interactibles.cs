using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactibles : MonoBehaviour
{
    public bool IsOn { get; protected set; }

    public abstract void FixedUpdate();
    public abstract void OnOff();
}
