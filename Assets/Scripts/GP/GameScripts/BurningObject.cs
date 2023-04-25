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
    private List<ParticleSystem> m_Flame;
    [SerializeField] private float m_BurningTime;

    private void Start()
    {
        m_Flame = new(GetComponentsInChildren<ParticleSystem>());
    }

    // Update is called once per frame
    void Update()
    {
        if (IsBurning)
        {
            Burn();
            m_Flame.ForEach((flame) => flame.Play());
        }
        else
            m_Flame.ForEach((flame) => flame.Stop());
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
        go.GetComponent<VisualEffect>().enabled = true;
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
