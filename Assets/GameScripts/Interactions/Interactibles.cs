using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Interactibles : MonoBehaviour
{
    public bool isOn;

    public abstract void Update();
    public abstract void OnOff();
}
