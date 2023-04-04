using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float m_Speed;
    [SerializeField] private float m_JumpStrength;
    private Vector3 m_Position;
    private Vector3 m_MoveDirection;
    private Animator m_Animator;
    private Rigidbody m_Rigidbody;
    private Transform m_GolemTransform;

    private Golem m_Golem;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        m_MoveDirection = new Vector3(m_Position.x, 0, m_Position.y);
        m_Rigidbody.AddForce(m_MoveDirection * (m_Speed * Time.deltaTime), ForceMode.Impulse);
        m_Animator.SetFloat("SpeedX", m_MoveDirection.x * m_Golem.m_CancelAnimator);
        m_Animator.SetFloat("SpeedY", m_MoveDirection.y * m_Golem.m_CancelAnimator);

        if (m_MoveDirection != Vector3.zero && m_Golem.m_CancelAnimator != 0)
        {
            Quaternion ToRotation = Quaternion.LookRotation(m_MoveDirection, Vector3.up);
            m_GolemTransform.rotation = Quaternion.RotateTowards(m_GolemTransform.rotation, ToRotation, 720 * Time.deltaTime);
        }

        CopyTransform(m_GolemTransform);
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
        m_Position = input;
    }

    public void OnJump(InputAction.CallbackContext _context)
    {
        Vector3 currentVelocity = m_Rigidbody.velocity;
        currentVelocity.y = 0;
        m_Rigidbody.velocity = currentVelocity;
        m_Rigidbody.AddForce(transform.up * m_JumpStrength);
    }

    public void OnCapacity(InputAction.CallbackContext _context)
    {
        m_Golem.UseCapacity();
    }

    public void SetGolem(Golem golem)
    {
        m_Golem = golem;
        m_GolemTransform = golem.transform;
        CopyTransform(m_GolemTransform);
        m_Rigidbody = golem.GetComponent<Rigidbody>();
        m_Animator = golem.GetComponent<Animator>();
    }

    public void SetMoveDirection(Vector3 move) => m_MoveDirection = move;
    public Vector3 GetMoveDirection() => m_MoveDirection;
}
