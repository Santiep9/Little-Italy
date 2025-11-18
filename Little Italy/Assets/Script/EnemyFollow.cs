using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float speed = 3f;
    public float gravedadArtificial = 1f;

    private Transform player; //lo que siguen

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Direccion hacia el player
        Vector3 direction = (player.position - transform.position).normalized;

        //gravedad aritificial
        direction.y -= gravedadArtificial;

        transform.position += direction.normalized * speed * Time.deltaTime; //utilizo normalized para separar la direccion de la magnitud y mantener velocidad constante para las diagonales raras
    }
}