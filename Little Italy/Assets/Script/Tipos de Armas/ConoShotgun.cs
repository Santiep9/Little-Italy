using UnityEngine;

public class ConoShotgun : MonoBehaviour
{
    void Start()
    {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;


        Vector3[] vertices = new Vector3[3];
        Vector2[] uv = new Vector2[3];
        int[] triangulos = new int[3];

        vertices[0] = Vector3.zero;
        vertices[1] = new Vector3(0, 5);
        vertices[2] = new Vector3(5, 5);

        triangulos[0] = 0;
        triangulos[1] = 1;
        triangulos[2] = 2;

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangulos;
        
    }
}
