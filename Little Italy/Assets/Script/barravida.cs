using UnityEngine;

using UnityEngine.UI; 

public class BarraVida : MonoBehaviour
{
    public Player player;          
    public Slider healthBar;     

    void Start()
    {
        
        healthBar.maxValue = player.Health; 
        healthBar.value = player.Health;    
    }

    void Update()
    {
        
        healthBar.value = player.Health;
    }
}
