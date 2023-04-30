using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Assertions;

public class MovingPlateform : MonoBehaviour
{
    public List<Transform> plateformPath;
    [HideInInspector] public float movingPlateformSpeed;
    public float speed;

    private int m_CurrentWaypoint;
    private Vector3 m_MovementOfCurrentFrame;

    // Start is called before the first frame update
    private void Start()
    {
        movingPlateformSpeed = speed;
        Assert.IsTrue(plateformPath.Count != 0);
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
        transform.position = Vector3.MoveTowards(transform.position, plateformPath[m_CurrentWaypoint].transform.position,
            (speed * Time.deltaTime));

        m_MovementOfCurrentFrame = transform.position - prevPos;
        if (Vector3.Distance(plateformPath[m_CurrentWaypoint].transform.position, transform.position) <= 0)
            m_CurrentWaypoint++;
        
        if (m_CurrentWaypoint != plateformPath.Count)
            return;
        
        plateformPath.Reverse();
        m_CurrentWaypoint = 0;
    }

    private void OnCollisionStay(Collision collision)
    {
        collision.transform.position += m_MovementOfCurrentFrame;
    }
}
