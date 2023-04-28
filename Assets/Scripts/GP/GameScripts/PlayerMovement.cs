using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    
    private List<Lever> m_Interactibles = new();
    public Vector3 m_MoveDirection;
    private Mechanism m_Mechanism;
    public Animator m_Animator;
    private Rigidbody m_Rigidbody;
    private Transform m_GolemTransform;
    private Golem m_Golem;
    [SerializeField] private float m_RangeToActivate;
    [HideInInspector] public bool CanPlay;
    [SerializeField] private float TimeBeforePlay;
    [HideInInspector] public bool IsGrounded;
    private bool m_IsMoving { get { return m_MoveDirection != Vector3.zero; } }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        CanPlay = false;
        m_Mechanism = FindObjectOfType<Mechanism>(true);
        m_Interactibles = new(FindObjectsOfType<Lever>());
        yield return new WaitForSeconds(TimeBeforePlay);
        CanPlay = true;
    }

    // Update is called once per frame
    void Update()
    {
        IsGrounded = Grounded();
        CopyTransform(m_GolemTransform);

        if (m_Golem.m_CancelAnimator != false)
            return;

        m_Animator.SetFloat("SpeedX", m_MoveDirection.x);
        m_Animator.SetFloat("SpeedY", m_Rigidbody.velocity.y);
        if(IsGrounded)
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

    private bool Grounded()
    {
        BoxCollider bc = m_Golem.GetComponent<BoxCollider>();
        var trans = bc.transform;
        var min = bc.center - bc.size * 0.5f;
        var max = bc.center + bc.size * 0.5f;

        var p4 = trans.TransformPoint(new Vector3(max.x, min.y, max.z));

        for (float i = 0; i < bc.size.x; i += bc.size.x/5)
        {
            var p2 = trans.TransformPoint(new Vector3(min.x + i, min.y, max.z));

            for (float j = 0; j < bc.size.z; j += bc.size.z/5)
            {


                var p1 = trans.TransformPoint(new Vector3(min.x + i, min.y, min.z + j));
                var p3 = trans.TransformPoint(new Vector3(max.x, min.y, min.z + j));
                Debug.DrawRay(p1 + Vector3.up * 0.10f, Vector3.down, Color.red);
                Debug.DrawRay(p2 + Vector3.up * 0.10f, Vector3.down, Color.red);
                Debug.DrawRay(p3 + Vector3.up * 0.10f, Vector3.down, Color.red);
                Debug.DrawRay(p4 + Vector3.up * 0.10f, Vector3.down, Color.red);
                if (Physics.Raycast(p1 + Vector3.up * 0.10f, Vector3.down, 0.20f)
               || Physics.Raycast(p3 + Vector3.up * 0.10f, Vector3.down, 0.20f)
               || Physics.Raycast(p3 + Vector3.up * 0.10f, Vector3.down, 0.20f)
               || Physics.Raycast(p4 + Vector3.up * 0.10f, Vector3.down, 0.20f))
                    return true;
            }

        }
        
        return false;
      
    }
    private void CopyTransform(Transform _transform)
    {
        transform.position = _transform.position;
        transform.rotation = _transform.rotation;
        transform.localScale = _transform.localScale;
    }

    public void OnMovement(InputAction.CallbackContext _context)
    {
        if (CanPlay)
        {
            var input = _context.ReadValue<Vector2>();
            m_MoveDirection = new Vector3(input.x, 0, input.y);
        }
        
    }

    public void OnJump(InputAction.CallbackContext _context)
    {
        if (CanPlay)
        {
            if (IsGrounded && _context.started && m_Golem.CanJump)
            {
                m_Animator.Play("Jump");
                Jump();
            }
        }
       
    }



    public void OnCapacity(InputAction.CallbackContext _context)
    {
        if (CanPlay)
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
                    Interactibles interactible = m_Interactibles.Find((interactible) => Vector3.Distance(transform.position, interactible.transform.position) <= m_RangeToActivate);
                    if (interactible != null)
                    {
                        interactible.OnOff();
                    }
                    else
                        StartCoroutine(m_Golem.UseCapacity(time));
                }
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
    public Vector3 GetMoveDirection() => m_MoveDirection;

    public Animator GetAnimator() => m_Animator;
}