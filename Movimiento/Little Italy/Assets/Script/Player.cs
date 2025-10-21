using UnityEngine;

//para ver el mensaje bien necesario instalar la extiension "Better Comments".
//
//!ATENCION:
//!Este codigo se ha hecho a altas horas de la noche, no tocar nada si no es estrictamente necesario.
//!Bajo los efectos de la cafeina, desesperación y un bug que  solo se manifiesta cuando nadies lo mira.

//!No funciona si lo entiendes.
//!No lo entiendes si funciona.

//!Si necesitas cambiar algo, primero consulta con el autor original, reza y luego haz una copia de seguridad.




//No hace falta que hagas caso a lo de arriba, solo a la linea 12.
public class Player : MonoBehaviour
{
    [Header("Player Settings")]
    private int maxHealth = 100; //! Salud maxima, no modificar 
    private int health = 100;
    public int Health
    {
        get { return health; }
        set { health = Mathf.Clamp(value, 0, maxHealth); }
    }

    private Rigidbody2D rb;

    [Header("Movement Settings")]
    [SerializeField] private float speed; // poner la speed a 7
    private float moveHorizontal;
    private float moveVertical;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update se usa para leer input cada frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");
    }

    // FixedUpdate para aplicar la física
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector2 input = new Vector2(moveHorizontal, moveVertical);
        if (input.sqrMagnitude > 1f)
        {
            input.Normalize();//Normaliza el vector para que la velocidad diagonal no sea mayor a la speed establecida
        }
        rb.linearVelocity = input * speed;
    }
}
