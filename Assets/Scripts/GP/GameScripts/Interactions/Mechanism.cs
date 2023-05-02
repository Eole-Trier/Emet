using System.Collections.Generic;
using UnityEngine;

public class Mechanism : MonoBehaviour
{
    public float timer;
    public bool playOnce;
    [HideInInspector] public float myTimer;
    [HideInInspector] public bool IsActive;
    public List<Interactibles> m_InteractibleList;

    private void Awake()
    {
        IsActive = Time.time <= 0;
    }
}