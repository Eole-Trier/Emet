using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

public class MovingPlateform : MonoBehaviour
{
    [SerializeField] private List<Transform> m_PlateformPath;
    [SerializeField] private float m_Speed;

    private int m_CurrentWaypoint;
    private Vector3 m_MovementOfCurrentFrame;

    // Start is called before the first frame update
    private void Start()
    {
        Assert.IsTrue(m_PlateformPath.Count != 0);
        m_CurrentWaypoint = 0;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector3 prevPos = transform.position;
        transform.position = Vector3.MoveTowards(transform.position, m_PlateformPath[m_CurrentWaypoint].transform.position,
            (m_Speed * Time.deltaTime));

        m_MovementOfCurrentFrame = transform.position - prevPos;
        if (Vector3.Distance(m_PlateformPath[m_CurrentWaypoint].transform.position, transform.position) <= 0)
            m_CurrentWaypoint++;
        
        if (m_CurrentWaypoint != m_PlateformPath.Count)
            return;
        
        m_PlateformPath.Reverse();
        m_CurrentWaypoint = 0;
    }

    private void OnCollisionStay(Collision collision)
    {
        collision.transform.position += m_MovementOfCurrentFrame;
    }
}
