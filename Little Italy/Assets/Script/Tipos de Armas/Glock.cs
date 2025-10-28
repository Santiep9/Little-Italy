using UnityEngine;

public class Glock : Proyectil
{
    private void Awake()
    {
        shootForce = 30f;
        timeBetweenShooting = 0.1f;
        allowButtonHold = false;
    }
}
