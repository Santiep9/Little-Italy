using UnityEngine.InputSystem;
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
    public float speed; // poner la speed a 7
    public float baseSpeed = 7;
    private float moveHorizontal;
    private float moveVertical;

    [Header("Input Settings")]
    public InputActionAsset inputActions; // Da acceso a todas las acciones de input definidas en el Input Action Asset
    private InputAction m_moveAction;// Se utiliza para almacenar la acción que queremos utilizar
    private Vector2 m_moveAmt;

    private void OnEnable()//Se habilita el Action Map del jugador
    {
        inputActions.FindActionMap("Player").Enable();
    }
    private void OnDisable()//Se deshabilita el Action Map del jugador
    {
        inputActions.FindActionMap("Player").Disable();
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        m_moveAction = InputSystem.actions.FindAction("Move");//Busca la acción "Move" definida en el Input Action Asset
    }

    // Update se usa para leer input cada frame
    void Update()
    {
        m_moveAmt = m_moveAction.ReadValue<Vector2>();//Lee el valor del vector de los inputs
    }

    // FixedUpdate para aplicar la física
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        
        if (m_moveAmt.sqrMagnitude > 1f)
        {
            m_moveAmt.Normalize();//Normaliza el vector para que la velocidad diagonal no sea mayor a la speed establecida
        }
        rb.linearVelocity = m_moveAmt * speed;
        ////print(m_moveAmt);
    }

    //Speed
    public float GetSpeed()
    {
        return speed;
    }

    public void AddSpeed(float amount)
    {
        speed += amount;
    }

    public void ResetSpeed()
    {
        speed = baseSpeed;
    }
}
