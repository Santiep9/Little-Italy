using UnityEngine;

public abstract class Potenciador : MonoBehaviour
{

    public float duration = 5f; // tiempo que dura el efecto
    public abstract void ApplyEffect(Player player); //clase abstracta para hacer override en todos los powers

    //TODO SPAWN

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //
    }

    // Update is called once per frame
    void Update()
    {
        //
    }
}
