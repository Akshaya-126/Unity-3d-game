using UnityEngine;
using UnityEngine.SceneManagement;

public class scenenav : MonoBehaviour
{
    // Generic method to load any scene
    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
