using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float m_Speed;
    private float m_InitalSpeed;
    [SerializeField] private float m_JumpStrength;
    private Vector2 m_Position;
    public Vector3 m_MoveDirection;
    private Animator m_Animator;
    private Rigidbody m_Rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_InitalSpeed = m_Speed;
    }

    // Update is called once per frame
    void Update()
    {
        m_MoveDirection = new Vector3(m_Position.x, 0, m_Position.y);
        transform.position += new Vector3(m_Position.x * m_Speed * Time.deltaTime, 0, m_Position.y * m_Speed * Time.deltaTime);
        m_Animator.SetFloat("SpeedX", m_Position.x * m_Speed);
        m_Animator.SetFloat("SpeedY", m_Position.y * m_Speed);

        if (m_MoveDirection != Vector3.zero && m_Speed != 0)
        {
            Quaternion ToRotation = Quaternion.LookRotation(m_MoveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, ToRotation, 720 * Time.deltaTime);
        }

    }
    public void OnMovement(InputAction.CallbackContext _context)
    {
        var input = _context.ReadValue<Vector2>();
        m_Position = input;
    }

    public void OnJump(InputAction.CallbackContext _context)
    {
        Vector3 currentVelocity = m_Rigidbody.velocity;
        currentVelocity.y = 0;
        m_Rigidbody.velocity = currentVelocity;
        m_Rigidbody.AddForce(transform.up * m_JumpStrength);
    }
    
    public void SetSpeed(float speed) => m_Speed = speed;
    public float GetInitalSpeed() => m_InitalSpeed;

}
