using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement2 : MonoBehaviour
{
    [SerializeField] private float m_Speed;
    [SerializeField] private float m_JumpStrength;
    private Vector3 m_MoveDirection;
    private Rigidbody m_Rigidbody;
    private Transform m_GolemTransform;
    private bool IsGrounded { get { return Physics.Raycast(transform.position + Vector3.up * 0.05f, Vector3.down, 0.1f); } }

    private Golem2 m_Golem;

    // Update is called once per frame
    void Update()
    {
        if (m_MoveDirection != Vector3.zero)
        {
            Quaternion ToRotation = Quaternion.LookRotation(m_MoveDirection, Vector3.up);
            m_GolemTransform.rotation = Quaternion.RotateTowards(m_GolemTransform.rotation, ToRotation, 720 * Time.deltaTime);
        }

        CopyTransform(m_GolemTransform);
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void CopyTransform(Transform _transform)
    {
        transform.position = _transform.position;
        transform.rotation = _transform.rotation;
        transform.localScale = _transform.localScale;
    }

    public void OnMovement(InputAction.CallbackContext _context)
    {
        if (m_Rigidbody.constraints != RigidbodyConstraints.FreezeAll)
        {
            m_MoveDirection = new Vector3(_context.ReadValue<Vector2>().x, 0, _context.ReadValue<Vector2>().y);
        }
    }
    
    public void OnJump(InputAction.CallbackContext _context)
    {
        if (_context.started && IsGrounded)
            Jump();
    }

    public void OnCapacity(InputAction.CallbackContext _context)
    {
        if(_context.started)
            m_Golem.UseCapacity();
    }

    public void SetGolem(Golem2 golem)
    {
        m_Golem = golem;
        m_GolemTransform = golem.transform;
        CopyTransform(m_GolemTransform);
        m_Rigidbody = golem.GetComponent<Rigidbody>();
    }

    private void Jump()
    {
        m_Rigidbody.AddForce(transform.up * m_JumpStrength * Time.fixedDeltaTime, ForceMode.Impulse);
    }
    private void Movement()
    {
        Vector3 vel = new(m_MoveDirection.x * m_Speed * Time.fixedDeltaTime,
            m_Rigidbody.velocity.y, m_MoveDirection.z * m_Speed * Time.fixedDeltaTime);

        m_Rigidbody.velocity = vel;
    }
    public Golem2 GetGolem() => m_Golem;

    public void SetMoveDirection(Vector3 move) => m_MoveDirection = move;
    public Vector3 GetMoveDirection() => m_MoveDirection;

}