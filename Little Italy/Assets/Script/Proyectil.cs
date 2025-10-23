using UnityEngine;

public class Proyectil : MonoBehaviour
{
    public GameObject bullet;
    public float shootForce = 10f;
    public float upwardForce = 0f;
    public float timeBetweenShooting = 0.2f;
    public float spread = 0f;
    public int bulletsPerTap = 1;
    public bool allowButtonHold = true;

    public Rigidbody playerRb;
    public float recoilForce = 5f;

    public Transform attackPoint;     // Where the bullet spawns
    public Transform playerTransform; // Reference to the player object (used for forward direction)
    public GameObject muzzleFlash;

    private bool shooting;
    private bool readyToShoot = true;
    private int bulletsShot;
    private bool allowInvoke = true;
    public Player jugador;


    private void Update()
    {
        MyInput();
    }

    private void MyInput()
    {
        shooting = allowButtonHold ? Input.GetKey(KeyCode.Mouse0) : Input.GetKeyDown(KeyCode.Mouse0);

        if (readyToShoot && shooting)
        {
            bulletsShot = 0;
            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;

        // Shoot in the player's forward (Z) direction
        Vector3 direction = playerTransform.forward;

        // Add spread (optional)
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);
        direction += new Vector3(x, y, 0);
        direction.Normalize();

        // Instantiate bullet
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);
        currentBullet.transform.forward = direction;

        // Ignorar colisión con jugador
        Collider bulletCollider = currentBullet.GetComponent<Collider>();
        Collider playerCollider = playerTransform.GetComponent<Collider>();
        if (bulletCollider != null && playerCollider != null)
        {
            Physics.IgnoreCollision(bulletCollider, playerCollider);
        }

        // Apply force to bullet
        Rigidbody bulletRb = currentBullet.GetComponent<Rigidbody>();
        bulletRb.AddForce(direction * shootForce, ForceMode.Impulse);
        bulletRb.AddForce(Vector3.up * upwardForce, ForceMode.Impulse); // Optional upward arc

        // Muzzle flash
        if (muzzleFlash != null)
            Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);

        // Recoil
        playerRb.AddForce(-direction * recoilForce, ForceMode.Impulse);

        bulletsShot++;

        // Burst logic
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

