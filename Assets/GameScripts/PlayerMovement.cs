using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    
    private Vector3 m_MoveDirection;
    private Animator m_Animator;
    private Rigidbody m_Rigidbody;
    private Transform m_GolemTransform;
    private Interact m_Interact;
    private Golem m_Golem;
    private bool m_IsMoving { get { return m_MoveDirection != Vector3.zero; } }

    // Start is called before the first frame update
    void Start()
    {
        m_Interact = FindObjectOfType<Interact>();
    }

    // Update is called once per frame
    void Update()
    {
        CopyTransform(m_GolemTransform);

        if (m_Golem.m_CancelAnimator != false)
            return;

        m_Animator.SetFloat("SpeedX", m_MoveDirection.x);
        m_Animator.SetFloat("SpeedY", m_Rigidbody.velocity.y);
        if(IsGrounded())
            m_Animator.SetFloat("SpeedY", 0);
        m_Animator.SetFloat("SpeedZ", m_MoveDirection.z);
        m_Animator.SetBool("Moving", m_IsMoving);

        if (m_MoveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(m_MoveDirection, Vector3.up);
            m_GolemTransform.rotation = Quaternion.RotateTowards(m_GolemTransform.rotation, toRotation, 720 * Time.deltaTime);
        }
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
        if (IsGrounded() && _context.started)
        {
            m_Animator.Play("Jump");
            Jump();
        }
    }

    public void OnCapacity(InputAction.CallbackContext _context)
    {
        if (m_Golem.m_Type == Golem.GolemType.EMET)
        {
            if (_context.canceled)
            {
                double time = _context.duration;
                StartCoroutine(m_Golem.UseCapacity(time));
            }
        }
        else
        {
            if (_context.started)
            {
                double time = _context.time;
                /*if (Vector3.Distance(m_Interact.m_Interactibles[0].transform.position, transform.position) < m_Interact.rangeToActivate &&
                    m_Interact.m_Interactibles[0].CompareTag("Interactible"))
                {
                    m_Interact.action = true;
                }
                else
                */
                    StartCoroutine(m_Golem.UseCapacity(time));
            }
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
        m_Rigidbody.AddForce(transform.up * m_Golem.m_JumpStrength * Time.fixedDeltaTime, ForceMode.Impulse);
    }
    private void Movement()
    {
        Vector3 vel = new(m_MoveDirection.x * m_Golem.m_Speed * Time.fixedDeltaTime,
            m_Rigidbody.velocity.y, m_MoveDirection.z * m_Golem.m_Speed * Time.fixedDeltaTime);
        m_Rigidbody.velocity = vel;
    }
    public Golem GetGolem() => m_Golem;

    public void SetMoveDirection(Vector3 move) => m_MoveDirection = move;
    //public void SetJumpStrength(float strength) => m_JumpStrength = strength;
    public Vector3 GetMoveDirection() => m_MoveDirection;
    //public float GetJumpStrength() => m_JumpStrength;

    public Animator GetAnimator() => m_Animator;

    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position + Vector3.up * 0.10f, Vector3.down, 0.20f);
    }
}