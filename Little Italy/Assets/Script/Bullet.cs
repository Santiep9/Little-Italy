using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{

    public AudioClip hitSound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            //Audio hit
            if (audioSource != null && hitSound != null)
            {
                audioSource.PlayOneShot(hitSound);
            }

            // Change its color to red (must have a Renderer)
            Renderer renderer = collision.gameObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = Color.red;

            }

            Destroy( collision.gameObject, 0.3f);
        
        }

        if (collision.gameObject != null)
        {
            Destroy(gameObject, 1f);
        }

        // Optional: Destroy bullet on impact
        Destroy(gameObject, 0.6f);
    }
}
