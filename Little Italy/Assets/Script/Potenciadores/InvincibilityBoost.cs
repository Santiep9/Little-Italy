using UnityEngine;
using System.Collections;

public class InvincibilityBoost : Potenciador
{
    public override void ApplyEffect(Player player)
    {
        StartCoroutine(ApplyPower(player));
    }

    private IEnumerator ApplyPower(Player player)
    {
        player.SetInvincible(true);
        Debug.Log("INVINCIBLE");

        yield return new WaitForSeconds(duration);

        player.SetInvincible(false);
        Debug.Log("INVINCIBLE OFF");

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

