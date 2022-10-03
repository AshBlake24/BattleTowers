using UnityEngine;

public class TowerBuilder : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _selectionMenu;

    private TowerViewer[] _viewers;

    private void Awake() => _viewers = _selectionMenu.GetComponentsInChildren<TowerViewer>();

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
        if (_player.Money < tower.Price)
            return;

        _player.Buy(tower.Price);

        Transform towerPlace = placeholder.transform.parent;

        placeholder.gameObject.SetActive(false);

        Instantiate(tower, towerPlace.transform);
    }
}