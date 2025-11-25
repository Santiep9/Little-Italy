using Unity.VisualScripting;
using UnityEngine;

//!No tocar, conntrola la vida del enemigo y el daño que recibe con las distintas balas
//Cuando se añada el otro arma se añadira otro tag para ese arma y se implementara lo que hay debajo 
public class Enemy : MonoBehaviour
{
    private int maxHealth = 100;
    [SerializeField]private int health = 100;
    public int Health
    {
        get { return health; }
        set { health = Mathf.Clamp(value,0,maxHealth); }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("BulletSniper"))//Este hace que el enemigo reciba 100 de daño al ser impactado por la bala que tiene el tag de BulletSniper
        {
            Debug.Log("Hit by Sniper Bullet");
            Health -= 100;
            Debug.Log("Enemy Health: " + Health);
            if(Health <= 0)
            {
                Destroy(gameObject);
            }
        }


        if(collision.gameObject.CompareTag("BulletGlock"))//Este hace 10 de daño al ser impactado por la bala que tiene el tag de BulletGlock, "añadir posteriormente o una bala para el arma glock o cambiar el tag al cambiar de arma, se tendra que decidir" 
        {
            Debug.Log("Hit by Glock Bullet");
            Health -= 10;
            Debug.Log("Enemy Health: " + Health);
            if(Health <= 0)
            {
                Destroy(gameObject);
            }
        }

        if (collision.gameObject.CompareTag("Player"))
        {
                Debug.Log("GAME OVER");
            // Detener el tiempo para la memoria
            Time.timeScale = 0f;
        }
    }
}
