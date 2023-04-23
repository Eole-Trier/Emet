using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    PlayerSwitch playerSwitch;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Golem golem))
        {
            if (playerSwitch.m_CurrentRoom + 1 < playerSwitch.Rooms.Count)
            {
                Debug.Log("Crok");
                playerSwitch.m_CurrentRoom += 1;
                Debug.Log(playerSwitch.m_CurrentRoom);
            }
            else
            {
                //change level
            }
        }
    }
}
