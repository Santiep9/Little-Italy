using UnityEngine;

public class RandomWeapon : MonoBehaviour
{
    [SerializeField] private GameObject[] randomWeapons;

    void Start()
    {
        int randomIndex = Random.Range(0, randomWeapons.Length);

        for (int i = 0; i < randomWeapons.Length; i++)
        {
            randomWeapons[i].SetActive(i == randomIndex);
        }
    }
}