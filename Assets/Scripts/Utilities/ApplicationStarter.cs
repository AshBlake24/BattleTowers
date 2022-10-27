using UnityEngine;

public class ApplicationStarter : MonoBehaviour
{
    [SerializeField] private string _startScene;

    private void Start()
    {
        SceneLoader.Instance.LoadScene(_startScene);
    }
}