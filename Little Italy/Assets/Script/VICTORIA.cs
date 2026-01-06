using UnityEngine;
using UnityEngine.SceneManagement;

public class VICTORIA : MonoBehaviour
{
    public string sceneToLoad = "VICTORIA"; 

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
