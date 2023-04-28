using System.Collections.Generic;
using UnityEngine;

public class Mechanism : MonoBehaviour
{
    public float timer;
    [HideInInspector] public float myTimer;
    [HideInInspector] public bool IsActive;

    private void Start()
    {
        IsActive = gameObject.activeSelf;
    }

    public bool TimerStatus(Mechanism mechanism)
    {
        if (mechanism.timer <= 0)
            return true;
        else
            return false;
    }

}
