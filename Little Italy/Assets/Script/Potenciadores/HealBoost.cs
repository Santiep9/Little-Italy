using UnityEngine;

public class HealBoost : Potenciador
{
    public int healAmount = 25;

    public override void ApplyEffect(Player player)
    {
        player.Heal(healAmount);
        Debug.Log("HEAL +" + healAmount);

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                ApplyEffect(player);
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<Collider2D>().enabled = false;
            }
        }
    }
}

