using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneButton : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneLoader.Instance.ChangeScene(sceneName, scene.name);
    }
}