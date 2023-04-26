using UnityEngine;
using Unity.Mathematics;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Vector3 m_PositionFromPlayer;
    [SerializeField] private bool3 FreezeAxis;

    private PlayerMovement m_Player;

    private void Start()
    {
        m_Player =  FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(FreezeAxis.x ? m_PositionFromPlayer.x : m_Player.GetGolem().transform.position.x + m_PositionFromPlayer.x,
            FreezeAxis.y ? m_PositionFromPlayer.y : m_PositionFromPlayer.y + m_Player.GetGolem().transform.position.y,
            FreezeAxis.z ? m_PositionFromPlayer.z : m_PositionFromPlayer.z + m_Player.GetGolem().transform.position.z);
    }
}
