using UnityEngine;

public class Sniper : Proyectil
{
    private void Awake()
    {
        shootForce = 50f;
        timeBetweenShooting = 1.5f;
        allowButtonHold = false;
    }
}
