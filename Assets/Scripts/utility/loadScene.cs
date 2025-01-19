using UnityEngine;
using UnityEngine.SceneManagement;

public class loadScene : MonoBehaviour
{
    [SerializeField] public string sceneName; // Name of the scene to load

    public void LoadGameScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            // Load the new scene
            SceneManager.LoadScene(sceneName);
        }
    }
}
