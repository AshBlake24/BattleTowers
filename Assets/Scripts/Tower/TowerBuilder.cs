using UnityEngine;

public class TowerBuilder : MonoBehaviour
{
    [SerializeField] private TowerSelector _selector;

    private TowerViewer[] _viewers;

    private void Awake() => _viewers = _selector.GetComponentsInChildren<TowerViewer>();

    private void OnEnable() 
    { 
        if (_viewers != null && _viewers.Length > 0)
        {
            foreach (var viewer in _viewers)
                viewer.ViewerClicked += OnViewerClicked;
        }
    }

    private void OnDisable() 
    {
        if (_viewers != null && _viewers.Length > 0)
        {
            foreach (var viewer in _viewers)
                viewer.ViewerClicked -= OnViewerClicked;
        }
    }

    private void OnViewerClicked(Tower tower, TowerPlaceholder placeholder)
    {
        //реалиовать покупку за очки

        Transform towerPlace = placeholder.transform.parent;

        placeholder.gameObject.SetActive(false);

        Instantiate(tower, towerPlace.transform);
    }
}