using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class WaterParticles : MonoBehaviour
{
    [SerializeField] private float m_TimerToShutDown;
    private float timer;

    private void Start()
    {
        FindObjectOfType<AudioManager>().m_AudioSourceList.Find(s => s.name == "water_screen").Play();
        timer = m_TimerToShutDown;
    }

    private void OnParticleCollision(GameObject other)
    {
        if(other.TryGetComponent(out BurningObject burningObject) && burningObject.IsBurning)
        {
            if (timer <= 0)
            {
                timer = m_TimerToShutDown;
                burningObject.IsBurning = false;
            }
            timer -= Time.fixedDeltaTime;
        }
        else
            timer = m_TimerToShutDown;
    }
}
