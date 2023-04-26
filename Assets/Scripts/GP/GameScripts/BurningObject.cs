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

    [SerializeField] private float m_BurningTime;

    private void Start()
    {
        m_Lights = new(GetComponentsInChildren<Light>());
        m_Particles = new(GetComponentsInChildren<ParticleSystem>());
        m_Visual = new(GetComponentsInChildren<VisualEffect>());
    }

    // Update is called once per frame
    void Update()
    {
        if (IsBurning)
        {
            Burn();
            m_Particles.ForEach((flame) => flame.Play());
            m_Visual.ForEach((visual) => visual.enabled = true);
            m_Lights.ForEach((lights) => lights.enabled = true);
        }
        else
        {
            m_Particles.ForEach((flame) => flame.Stop());
            m_Visual.ForEach(visual => visual.enabled = false);
            m_Lights.ForEach((lights) => lights.enabled = false);
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
                if (gameObject.TryGetComponent(out EfritBehaviour burningObject))
                {
                    gameObject.GetComponent<BurningObject>().IsBurning = true;
                    continue;
                }

                m_GameObjects.Add(gameObject);
                StartCoroutine(OwnDestroy(gameObject));
            }
            else if (gameObject.TryGetComponent(out Brasero brasero) && brasero.IsOn)
                IsBurning = true;
            else if (gameObject.tag == "Water")
                IsBurning = false;
        }
    }
    public IEnumerator OwnDestroy(GameObject go)
    {
        if (go.TryGetComponent(out VisualEffect visualEffect))
        {
            go.GetComponent<VisualEffect>().enabled = true;
            yield return new WaitForSeconds(m_BurningTime);
            m_GameObjects.Remove(go);
            Destroy(go);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, BurnDistance);
    }
}
