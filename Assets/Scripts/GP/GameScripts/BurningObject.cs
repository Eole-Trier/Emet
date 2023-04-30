using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using static ObjectType;

public class BurningObject : MonoBehaviour
{
    public bool IsBurning;
    public float BurnDistance;
    private List<GameObject> m_GameObjects = new();
    private List<ParticleSystem> m_Particles;
    private List<VisualEffect> m_Visual;
    private List<Light> m_Lights;
    private bool m_PlaySound;

    [SerializeField] private float m_BurningTime;

    private void Start()
    {
        m_PlaySound = IsBurning;
        m_Lights = new(GetComponentsInChildren<Light>());
        m_Particles = new(GetComponentsInChildren<ParticleSystem>());
        m_Visual = new(GetComponentsInChildren<VisualEffect>());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (IsBurning)
        {
            Burn();
            m_Particles.ForEach((flame) => flame.Play());
            m_Visual.ForEach((visual) => visual.enabled = true);
            m_Lights.ForEach((lights) => lights.enabled = true);
            if (m_PlaySound)
            { 
                FindObjectOfType<AudioManager>().Play("efrit_on");
                m_PlaySound = false;
            }
        }
        else
        {
            m_Particles.ForEach((flame) => flame.Stop());
            m_Visual.ForEach(visual => visual.enabled = false);
            m_Lights.ForEach((lights) => lights.enabled = false);
            if (!m_PlaySound)
            {
                FindObjectOfType<AudioManager>().Stop("efrit_on");
                FindObjectOfType<AudioManager>().Play("efrit_off");
                m_PlaySound = true;
            }
        }
    }

    private void Burn()
    {
        Collider[] burn = Physics.OverlapSphere(transform.position, BurnDistance);

        foreach (Collider c in burn)
        {
            GameObject gameObject = c.gameObject;
            if (m_GameObjects.Contains(gameObject))
                continue;

            if (gameObject.TryGetComponent(out ObjectType type) && type.ObjType.HasFlag(Type.Burnable))
            {
                if (gameObject.TryGetComponent(out EfritBehaviour efrit) || gameObject.TryGetComponent(out Brasero brasero))
                {
                    gameObject.GetComponent<BurningObject>().IsBurning = true;
                    continue;
                }
                m_GameObjects.Add(gameObject);
                StartCoroutine(OwnDestroy(gameObject));
            }
            else if (gameObject.tag == "Water")
                IsBurning = false;
        }
    }
    public IEnumerator OwnDestroy(GameObject go)
    {
        go.GetComponent<BurningObject>().IsBurning = true;
        yield return new WaitForSeconds(m_BurningTime);
        m_GameObjects.Remove(go);
        Destroy(go);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, BurnDistance);
    }
}
