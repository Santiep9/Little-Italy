using UnityEngine;
using UnityEngine.SceneManagement;

public class derrota : MonoBehaviour
{
    public void Retry()
    {
        SceneManager.LoadScene("Mapping");
    }

    public void ExitGamederrota()
    {
        Application.Quit();
        Debug.Log("Saliendo del juego");
    }
    public void Menu()
    {
        SceneManager.LoadScene("Main manu");
    }
}