using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetOnKey : MonoBehaviour
{
    void Update()
    {
        // Press R to reset all progress
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();

            Debug.Log("✨ All progress has been reset! ✨");

            // 🔄 Reload scene so the UI updates
            SceneManager.LoadScene("levels");
        }
    }
}
