using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCombiner : MonoBehaviour
{
    [SerializeField] private List<MeshFilter> sourceMeshFilters;
    [SerializeField] private MeshFilter targetMeshFilter;
    [ContextMenu("Mesh Combiner")]
    private void CombineMeshes()
    {
        var combine = new CombineInstance[sourceMeshFilters.Count];
        for(var i = 0; i < sourceMeshFilters.Count; i++)
        {
            combine[i].mesh = sourceMeshFilters[i].sharedMesh;
            combine[i].transform = sourceMeshFilters[i].transform.localToWorldMatrix;
        }

        var mesh = new Mesh();
        mesh.CombineMeshes(combine);
        targetMeshFilter.mesh = mesh;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
