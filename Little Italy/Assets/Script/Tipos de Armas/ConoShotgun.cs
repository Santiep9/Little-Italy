using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class ConoShotgun : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private LayerMask layerEnemigos;
    private Mesh mesh;
    private Vector3 origin;
    private float startingAngle;
    private float fov;
    public int damage = 70;

    private bool doShotThisFrame;
    private float showTimer;
    private float showDuration = 0.05f;
    private MeshRenderer meshRenderer;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        fov = 90f; //esto es el cono basicamente
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
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

        vertices[0] = Vector3.zero;

        int verticeIndex = 1;
        int trianguloIndex = 0;

        List<Enemy> enemiesHitThisShot = new List<Enemy>();

        bool applyDamage = doShotThisFrame;

        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 dirWorld = GetVectorFromAngle(angle);
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, dirWorld, viewDistance, layerMask);
            RaycastHit2D raycastHit2DEnemy = Physics2D.Raycast(origin, dirWorld, viewDistance, layerEnemigos);

            Vector3 hitWorldPos;

            if (raycastHit2D.collider == null)
            {
                //no esta colisionando con nada
                hitWorldPos = origin + dirWorld * viewDistance;
            }
            else
            {
                //si colisiona con algo, compruebo si es enemigo
                 hitWorldPos = raycastHit2D.point;
            }

            Vector3 vertexLocal = transform.InverseTransformPoint(hitWorldPos);
            vertices[verticeIndex] = vertexLocal;

            if (applyDamage && raycastHit2DEnemy.collider != null)
            {
                Enemy enemy = raycastHit2DEnemy.collider.GetComponent<Enemy>();
                if (enemy != null && !enemiesHitThisShot.Contains(enemy))
                {
                    enemy.TakeDamage(damage);
                    enemiesHitThisShot.Add(enemy);
                }
            }

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

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangulos;

        if(doShotThisFrame)
        {
            meshRenderer.enabled = true;
            showTimer = showDuration;
        }
        if(showTimer > 0f)
        {
            showTimer -= Time.fixedDeltaTime;
            if(showTimer <= 0f)
            {
                meshRenderer.enabled = false;
            }
        }

        doShotThisFrame = false;
    }

    public void SetOrigin(Vector3 originWorld)
    {
        origin = originWorld;
        transform.position = originWorld;
    }

    public void SetAimDirection(Vector3 aimDirection)
    {
        float angle = GetAngleFromVectorFloat(aimDirection);
        angle += 90f;
        startingAngle = angle - fov / 2f;
    }
    public void TriggerShot()
    {
        doShotThisFrame = true;
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

