using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneButton : MonoBehaviour
{
    [SerializeField] private bool _loadWithDelay;

    public void ChangeScene(string sceneName)
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneLoader.Instance.ChangeScene(sceneName, scene.name, _loadWithDelay);
    }
}