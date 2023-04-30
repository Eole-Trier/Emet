using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Dissolve : MonoBehaviour
{
    public MeshRenderer Mesh;
    private Material Material;
    public VisualEffect VFXGraph;
    public float dissolveRate = 0.1f;
    public float refreshRate = 0.0250f;
    private BurningObject b;

    void Start()
    {
        Material = Mesh.material;
        
    }

    // Update is called once per frame
    void Update()
    {
        b = GetComponentInParent<BurningObject>();
        if (b.IsBurning)
        {
            StartCoroutine(DissolveCo());
        }
    }
    IEnumerator DissolveCo()
    {
        VFXGraph.Play();
        float counter = 0;
        while(Material.GetFloat("_DissolveAmount") < 10)
        {
            counter += dissolveRate;            
            Material.SetFloat("_DissolveAmount", counter);
            yield return new WaitForSeconds(refreshRate);
        }
    }
}
