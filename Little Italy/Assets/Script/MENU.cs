using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Mapping");
        
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Saliendo del juego");
    }
}
