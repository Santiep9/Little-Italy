using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class ConoShotgun : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private LayerMask layerEnemigos;
    private Mesh mesh;
    private Vector3 origin;
    private float startingAngle;
    private float fov;
    public int damage = 25;
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        origin = Vector3.zero;
        fov = 90f; //esto es el cono basicamente
    }

    private void FixedUpdate()
    {
        int rayCount = 50;
        float angle = startingAngle;
        float angleAumento = fov / rayCount;
        float viewDistance = 5f;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangulos = new int[rayCount * 3];

        vertices[0] = origin;

        int verticeIndex = 1;
        int trianguloIndex = 0;
        bool isShooting = Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame;

        List<Enemy> enemiesHitThisShot = new List<Enemy>();

        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, GetVectorFromAngle(angle), viewDistance, layerMask);
            RaycastHit2D raycastHit2DEnemy = Physics2D.Raycast(origin, GetVectorFromAngle(angle), viewDistance, layerEnemigos);
            if (raycastHit2D.collider == null)
            {
                //no esta colisionando con nada
                vertex = origin + GetVectorFromAngle(angle) * viewDistance;
            }
            else
            {
                //si colisiona con algo, compruebo si es enemigo
                    vertex = raycastHit2D.point;
            }

            if (isShooting && raycastHit2DEnemy.collider != null)
            {
                Enemy enemy = raycastHit2DEnemy.collider.GetComponent<Enemy>();
                if (enemy != null && !enemiesHitThisShot.Contains(enemy))
                {
                    enemy.TakeDamage(damage);
                    enemiesHitThisShot.Add(enemy);
                }
            }
            vertices[verticeIndex] = vertex;

            if (i > 0)
            {
                triangulos[trianguloIndex + 0] = 0;
                triangulos[trianguloIndex + 1] = verticeIndex - 1;
                triangulos[trianguloIndex + 2] = verticeIndex;

                trianguloIndex += 3;
            }

            verticeIndex++;
            angle -= angleAumento;
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangulos;

    }

    public void SetOrigin(Vector3 origin)
    {
        this.origin = origin;
    }

    public void SetAimDirection(Vector3 aimDirection)
    {
        startingAngle = GetAngleFromVectorFloat(aimDirection) - fov / 2f;
    }
    public static Vector3 GetVectorFromAngle(float angle)
    {
        //el angulo se convierte de 0 a 360
        float angleRad = angle * (Mathf.PI / 180);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    public static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }
}

