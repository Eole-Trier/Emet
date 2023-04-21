using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using static ObjectType;

public class BurningObject : MonoBehaviour
{
    public bool IsBurning;
    [SerializeField] private float m_BurnDistance;
    private List<GameObject> m_GameObjects = new();
    [SerializeField] private VisualEffect m_GivedFlame;
    [SerializeField] private float m_BurningTime;
    private int m_BurnableLayer;


    // Start is called before the first frame update
    void Start()
    {
        m_BurnableLayer = 1 << LayerMask.NameToLayer("Burnable");
    }

    // Update is called once per frame
    void Update()
    {
        m_GivedFlame.enabled = IsBurning;
        if (IsBurning)
            Burn();
    }

    private void Burn()
    {
        Collider[] burn = Physics.OverlapSphere(transform.position, m_BurnDistance);

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

        }
    }
    private IEnumerator OwnDestroy(GameObject go)
    {
        go.GetComponent<VisualEffect>().enabled = true;
        yield return new WaitForSeconds(m_BurningTime);
        m_GameObjects.Remove(go);
        Destroy(go);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_BurnDistance);
    }
}
