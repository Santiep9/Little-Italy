using UnityEngine;

public class Randomizer : MonoBehaviour
{
    public GameObject[] powerUps;

    [Range(0f, 1f)]
    public float dropChance = 0.5f;

    public void SpawnPowerUp()
    {
        if (Random.value > dropChance) return;

        if (powerUps.Length == 0) return;

        int index = Random.Range(0, powerUps.Length);
        Instantiate(powerUps[index], transform.position, Quaternion.identity);
    }
}