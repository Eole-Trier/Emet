using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BraseroBurn : MonoBehaviour
{
    public bool IsBurning;
    [SerializeField] private float m_BurnDistance;
    private List<GameObject> m_GameObjects = new();
    [SerializeField] private VisualEffect flame;
    private int m_BurnableLayer;


    // Start is called before the first frame update
    void Start()
    {
        m_BurnableLayer = 1 << LayerMask.NameToLayer("Burnable");
    }

    // Update is called once per frame
    void Update()
    {
        if (IsBurning)
        {
            flame.enabled = true;
            Burn();
        }
        else
        {
            flame.enabled = false;
        }
    }

    private void Burn()
    {
        Collider[] burn = Physics.OverlapSphere(transform.position, m_BurnDistance, m_BurnableLayer);

        foreach (Collider c in burn)
        {
            GameObject gameObject = c.gameObject;
            if (m_GameObjects.Contains(gameObject))
            {
                continue;
            }
            m_GameObjects.Add(gameObject);

            if (gameObject.TryGetComponent(out EfritBehaviour efrit))
            {
                efrit.IsBurning = true;
                return;
            }
            StartCoroutine(OwnDestroy(gameObject));
        }
    }
    private IEnumerator OwnDestroy(GameObject go)
    {
        go.GetComponent<VisualEffect>().enabled = true;
        yield return new WaitForSeconds(1);
        Destroy(go);
    }
}
