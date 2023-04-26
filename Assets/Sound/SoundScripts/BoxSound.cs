using UnityEngine;

public class BoxSound : MonoBehaviour
{
    private AudioSource m_AudioSource;
    private AudioClip m_AudioClip;
    private Rigidbody m_Rigidbody;
    [SerializeField] private float m_Volume;

    // Start is called before the first frame update
    void Start()
    {
        m_AudioClip = (AudioClip)Resources.Load("box_movement");
        m_AudioSource = FindObjectOfType<AudioSource>();
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if ((!m_AudioSource.isPlaying && m_Rigidbody.velocity.x != 0 && m_Rigidbody.velocity.y == 0) || (!m_AudioSource.isPlaying && m_Rigidbody.velocity.y == 0 && m_Rigidbody.velocity.z != 0))
        {
            m_AudioSource.PlayOneShot(m_AudioClip, m_Volume);
        }
    }
}
