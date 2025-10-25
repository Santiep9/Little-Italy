using UnityEngine;
using UnityEngine.InputSystem;

public class Proyectil : MonoBehaviour
{
    [Header("Ajustes del disparo")]
    public GameObject bullet;           //Prefab del proyectil
    public float shootForce = 10f;      //Fuerza del disparo aka velocidad
    public float timeBetweenShooting = 0.2f;
    public float spread = 0f;           //Spread
    public int bulletsPerTap = 1;
    public bool allowButtonHold = true;

    [Header("Recoil y referencias")]
    public Rigidbody2D playerRb;        //RB2
    public float recoilForce = 5f;
    public Transform attackPoint;       //Lugar de origen
    public Transform playerTransform;   // Referencia al jugador
    public GameObject muzzleFlash;      //Efecto visual para mas tarde

    [Header("Input System")]
    public InputActionAsset inputActions;   //InputActions
    private InputAction shootAction;        //Accion

    private bool shooting;
    private bool readyToShoot = true;
    private int bulletsShot;
    private bool allowInvoke = true;

    private void OnEnable()
    {
        //Input system y mapeado shit
        var playerMap = inputActions.FindActionMap("Player");
        if (playerMap != null)
        {
            shootAction = playerMap.FindAction("Shoot");
            if (shootAction != null)
                shootAction.Enable();
            else
                Debug.Log("Error del mapeado en Shoot probablemente no este asignado");
        }
        else
        {
            Debug.LogWarning("Error del mapeado main probablemente no este asignado en el objeto");
        }
    }

    private void OnDisable()
    {
        if (shootAction != null)
            shootAction.Disable();
    }

    private void Update()
    {
        MyInput();
    }

    private void MyInput()
    {
        if (shootAction == null)
            return;

        //Input System bullshit
        if (allowButtonHold)
            shooting = shootAction.IsPressed();
        else
            shooting = shootAction.WasPressedThisFrame();

        if (readyToShoot && shooting)
        {
            bulletsShot = 0;
            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;

        //Direccion y disparo al mouse
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 direction = (mouseWorldPos - attackPoint.position);
        direction.Normalize();

        //SPread
        float angle = Random.Range(-spread, spread);
        direction = Quaternion.Euler(0, 0, angle) * direction;

        // Crear proyectil
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);
        currentBullet.transform.right = direction;

        //Evita que te destruyas a ti mismo
        Collider2D bulletCollider = currentBullet.GetComponent<Collider2D>();
        Collider2D playerCollider = playerTransform.GetComponent<Collider2D>();
        if (bulletCollider != null && playerCollider != null)
        {
            Physics2D.IgnoreCollision(bulletCollider, playerCollider);
        }

        // Aplicar fuerza a la bala con rb2
        Rigidbody2D bulletRb = currentBullet.GetComponent<Rigidbody2D>();
        bulletRb.AddForce(direction * shootForce, ForceMode2D.Impulse);

        //Efecto de disparo
        if (muzzleFlash != null)
            Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);

        //Retroceso para los spammers
        if (playerRb != null)
            playerRb.AddForce(-direction * recoilForce, ForceMode2D.Impulse);

        bulletsShot++;

        //Rafagas
        if (bulletsShot < bulletsPerTap)
            Invoke(nameof(Shoot), timeBetweenShooting);

        if (allowInvoke)
        {
            Invoke(nameof(ResetShot), timeBetweenShooting);
            allowInvoke = false;
        }
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }
}



