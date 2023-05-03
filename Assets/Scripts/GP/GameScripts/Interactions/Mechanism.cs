using System.Collections.Generic;
using UnityEngine;

public class Mechanism : MonoBehaviour
{
    public float timer;
    public bool playOnce;
    [HideInInspector] public float myTimer;
    public bool IsActive;
    public List<Interactibles> m_InteractibleList;
}
