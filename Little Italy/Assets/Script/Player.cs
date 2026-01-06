using UnityEngine.InputSystem;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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

    [Header("Damage Settings")]
    public float damageCooldown = 1f; // cd del damage
    private bool canTakeDamage = true; //validacion
    private bool invincible = false;

    [Header("Atributos especiales escopeta")]
    public ConoShotgun cono;
    public Vector3 AimDirection { get; private set; }
    public float cooldownEscopeta;
    float lastShot;
    [SerializeField] private Transform visual;

    public void Rotate90()
    {
        visual.localRotation *= Quaternion.Euler(0f, 0f, 90f);
    }

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

        LookAtMouse();
        ShotgunShot();
    }

    // FixedUpdate para aplicar la física
    void FixedUpdate()
    {
        Move();
        if(cono != null)
        {
            cono.SetOrigin(transform.position);
            cono.SetAimDirection(AimDirection); //de donde sale el cono
        }
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

    public void TakeDamage(int damage)
    {
        //cd del damage
        if (!canTakeDamage)
            return;

        //boost invencible
        if (invincible)
            return;

        Health -= damage;
        Debug.Log("PLAYER DAMAGE: -" + damage + " | HP: " + Health);

        if (Health <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(DamageCooldown());
        }
    }

    private IEnumerator DamageCooldown() //COROUTINA DEL CD CON VALIDACION
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(damageCooldown);
        canTakeDamage = true;
    }

    private void Die()
    {
        Debug.Log("PLAYER DEAD");
        Time.timeScale = 0f;
        SceneManager.LoadScene("Morido");

        gameObject.SetActive(false);
        // Detener el tiempo para la memoria
        
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

    //HEAL
    public void Heal(int amount)
    {
        Health += amount;
    }

    //INVENCIBLE
    public void SetInvincible(bool value)
    {
        invincible = value;
    }

    public bool IsInvincible()
    {
        return invincible;
    }

    private void LookAtMouse()
    {
        Vector2 mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dir = (Vector3)(mousePos - (Vector2)transform.position);

        AimDirection = dir.normalized;
        transform.up = AimDirection;
    }

    public void ShotgunShot()
    {
        bool shotThisFrame = Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame;

        if (shotThisFrame && cono != null && Time.time >= lastShot + cooldownEscopeta)
        {
            lastShot = Time.time;
            cono.TriggerShot();
        }
    }
    
}
