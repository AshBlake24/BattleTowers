using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneButton : UnityEngine.UI.Extensions.Button
{
    [SerializeField] private bool _loadWithDelay;
    [SerializeField] private string _nextSceneName;

    protected override void OnButtonClick()
    {
        ChangeScene();
    }

    private void ChangeScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneLoader.Instance.ChangeScene(_nextSceneName, scene.name, _loadWithDelay);
    }
}