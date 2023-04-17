using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float m_Speed;
    [SerializeField] private float m_JumpStrength;
    private Vector3 m_MoveDirection;
    private Animator m_Animator;
    private Rigidbody m_Rigidbody;
    private Transform m_GolemTransform;
    private Interact m_Interact;
    private Golem m_Golem;

    private bool IsGrounded { get { return Physics.Raycast(transform.position + Vector3.up * 0.05f, Vector3.down, 0.1f); } }

    // Start is called before the first frame update
    void Start()
    {
        m_Interact = FindObjectOfType<Interact>();
    }

    // Update is called once per frame
    void Update()
    {

        m_Animator.SetFloat("SpeedX", m_MoveDirection.x * m_Golem.m_CancelAnimator);
        m_Animator.SetFloat("SpeedY", m_MoveDirection.y * m_Golem.m_CancelAnimator);

        if (m_MoveDirection != Vector3.zero && m_Golem.m_CancelAnimator != 0)
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
        var input = _context.ReadValue<Vector2>();
        m_MoveDirection = new Vector3(input.x, 0, input.y);
    }

    public void OnJump(InputAction.CallbackContext _context)
    {
        if (IsGrounded)
        {
            Jump();
        }
    }

    public void OnCapacity(InputAction.CallbackContext _context)
    {
        if (m_Golem.m_Type == Golem.Type.EMET)
        {
            if (_context.canceled)
            {
                double time = _context.duration;
                m_Golem.UseCapacity(time);
            }
        }
        else
            if (_context.started)
        {
            double time = _context.time;
            if (Vector3.Distance(m_Interact.m_Interactibles[0].transform.position, transform.position) < m_Interact.rangeToActivate &&
                m_Interact.m_Interactibles[0].tag == "Interactible")
            {
                m_Interact.action = true;
            }
            else
                m_Golem.UseCapacity(time);
        }
    }

    public void SetGolem(Golem golem)
    {
        m_Golem = golem;
        m_GolemTransform = golem.transform;
        CopyTransform(m_GolemTransform);
        m_Rigidbody = golem.GetComponent<Rigidbody>();
        m_Animator = golem.GetComponent<Animator>();
    }

    private void Jump()
    {
        Vector3 currentVelocity = m_Rigidbody.velocity;
        currentVelocity.y = 0;
        m_Rigidbody.velocity = currentVelocity;
        m_Rigidbody.AddForce(transform.up * m_JumpStrength * Time.fixedDeltaTime, ForceMode.Impulse);
    }
    private void Movement()
    {
        Vector3 vel = new(m_MoveDirection.x * m_Speed * Time.fixedDeltaTime,
            m_Rigidbody.velocity.y, m_MoveDirection.z * m_Speed * Time.fixedDeltaTime);
        m_Rigidbody.velocity = vel;

    }
    public Golem GetGolem() => m_Golem;

    public void SetMoveDirection(Vector3 move) => m_MoveDirection = move;
    public void SetJumpStrength(float strength) => m_JumpStrength = strength;
    public Vector3 GetMoveDirection() => m_MoveDirection;
    public float GetJumpStrength() => m_JumpStrength;
    public bool GetIsGrounded() => IsGrounded;
}