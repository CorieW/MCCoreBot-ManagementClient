using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    public static MeshGenerator Instance;

    [SerializeField] private Material _material;

    private void Awake()
    {
        Instance = this;
    }

    public void GenerateMesh(Vector3 meshPos, Vector3[] vertices, int[] triangles, float[] uv)
    {
        UnityThreadCommunicator.RunOnMainThread.Enqueue(() =>
        {
            GameObject newMeshObject = new GameObject("Chunk");
            newMeshObject.transform.position = meshPos;
            MeshRenderer mr = newMeshObject.AddComponent<MeshRenderer>();
            mr.material = _material;
            MeshFilter mf = newMeshObject.AddComponent<MeshFilter>();

            Mesh mesh = mf.mesh = new Mesh();
            mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            // mesh.uv = uv;
            mesh.RecalculateNormals();
        });
    }
}
