using System.Collections.Generic;
using UnityEngine;

public class Mechanism : MonoBehaviour
{
    public float timer;
    public bool playOnce;
    [HideInInspector] public float myTimer;
    [HideInInspector] public bool IsActive;
    [HideInInspector] public List<Interactibles> m_InteractibleList;

    private void Start()
    {
        IsActive = gameObject.activeSelf;
    }
}
