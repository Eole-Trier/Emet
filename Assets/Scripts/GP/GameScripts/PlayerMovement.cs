using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float m_RangeToActivate;
    [SerializeField] private float TimeBeforePlay;
    [SerializeField] private float m_WalkingSoundTimer;
    [HideInInspector] public bool IsGrounded;
    [HideInInspector] public bool CanPlay;
    private List<Lever> m_LeverList = new();
    private List<Button> m_ButtonList = new();
    private Rigidbody m_Rigidbody;
    private Transform m_GolemTransform;
    private Golem m_Golem;
    private Animator m_Animator;
    private AudioManager m_AudioManager;
    private Vector3 m_MoveDirection;
    private float m_Timer;
    private List<Interactibles> m_InteractibleList = new();

    public bool IsMoving { get { return m_MoveDirection != Vector3.zero; } }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        m_Timer = m_WalkingSoundTimer;
        m_AudioManager = FindObjectOfType<AudioManager>();
        m_LeverList = new(FindObjectsOfType<Lever>());
        m_ButtonList = new(FindObjectsOfType<Button>());
        foreach (Lever l in m_LeverList)
            m_InteractibleList.Add(l);
        foreach (Button b in m_ButtonList)
            m_InteractibleList.Add(b);

        CanPlay = false;
        yield return new WaitForSeconds(TimeBeforePlay);
        CanPlay = true;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
        IsGrounded = Grounded();
        CopyTransform(m_GolemTransform);

        if (m_Golem.m_CancelAnimator != false)
            return;

        if (IsMoving && IsGrounded)
        {
            int i = Random.Range(0, 5);
            if (m_Timer <= 0)
            {
                if (m_AudioManager.m_AudioSourceList.Find(s => s.name == "golem_footsteps_" + i) != null)
                    m_AudioManager.m_AudioSourceList.Find(s => s.name == "golem_footsteps_" + i).Play();
                m_Timer = m_WalkingSoundTimer;
            }
            else
                m_Timer -= Time.fixedDeltaTime;
        }
        if (m_MoveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(m_MoveDirection, Vector3.up);
            m_GolemTransform.rotation = Quaternion.RotateTowards(m_GolemTransform.rotation, toRotation, 720 * Time.deltaTime);
            
        }
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
        transform.SetPositionAndRotation(_transform.position, _transform.rotation);
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
                m_AudioManager.m_AudioSourceList.Find(s => s.name == "golem_jump").Play();
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
                    Interactibles interactible = m_InteractibleList.Find((interactible) => Vector3.Distance(transform.position, interactible.transform.position) <= m_RangeToActivate);
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

    public void Quit(InputAction.CallbackContext _context)
    {
        if(_context.started)
            SceneManager.LoadScene(0);
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
        m_Rigidbody.AddForce((transform.up * m_Golem.m_JumpStrength) * Time.fixedDeltaTime, ForceMode.Impulse);
    }
    private void Movement()
    {
        Vector3 vel = new(m_MoveDirection.x * m_Golem.m_Speed * Time.fixedDeltaTime,
            m_Rigidbody.velocity.y, m_MoveDirection.z * m_Golem.m_Speed * Time.fixedDeltaTime);
        Vector2 velo = new(vel.x, vel.z);
        Vector2 rbvelo = new(m_Rigidbody.velocity.x, m_Rigidbody.velocity.z);

        if (velo.sqrMagnitude + 0.1 < rbvelo.sqrMagnitude && velo.sqrMagnitude != 0)
        {
        }
       
        else
        {
            m_Rigidbody.velocity = vel; 
        }
    }
    public Golem GetGolem() => m_Golem;

    public void SetMoveDirection(Vector3 move) => m_MoveDirection = move;
    public Vector3 GetMoveDirection() => m_MoveDirection;

    public Rigidbody GetRigidbody() => m_Rigidbody;

    public Animator GetAnimator() => m_Animator;
}