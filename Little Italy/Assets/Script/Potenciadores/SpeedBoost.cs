using UnityEngine;
using System.Collections;
public class SpeedBoost : Potenciador
{

    //Normal
    /*
    private bool activated = false;
    private float timer = 0f;
    public float speedIncrease = 3f;

    private Player player;

    private void Update()
    {
        if (activated)
        {
            timer += Time.deltaTime;
            if (timer >= duration)
            {
                player.ResetSpeed();
                Destroy(gameObject);
            }
        }
    }

    public override void ApplyEffect(Player p)
    {
        player = p;
        player.AddSpeed(speedIncrease);

        activated = true;
        timer = 0f;
    }

    */

    //Coroutina test
    public float speedIncrease = 3f;

    public override void ApplyEffect(Player player)
    {
        StartCoroutine(ApplyPower(player));
    }

    private IEnumerator ApplyPower(Player player)
    {
        // Aplicar efecto
        player.AddSpeed(speedIncrease);

        // Duracion sleep
        yield return new WaitForSeconds(duration);

        // Quitar efecto
        player.ResetSpeed();
        Debug.Log("EFFECT OUT");

        // Destruir powerup despues de cogerlo
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Algo entro al trigger: " + other.name);
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                Debug.Log("EFFECT");
                ApplyEffect(player);
                gameObject.GetComponent<SpriteRenderer>().enabled = false; // ocultar booster
                gameObject.GetComponent<Collider2D>().enabled = false;     // evitar otro trigger NO QUITAR PQ SI NO LO COGES 2 VECES O MAS EL MISMO POWER  
            }
        }
    }
}
