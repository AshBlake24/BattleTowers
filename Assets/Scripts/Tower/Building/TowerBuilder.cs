using UnityEngine;

public class TowerBuilder : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TowerMenu _towerMenu;
    [SerializeField] private GameObject _selectorMenu;

    private TowerViewer[] _viewers;

    private void Awake()
    {
        _viewers = _selectorMenu.GetComponentsInChildren<TowerViewer>();
    }

    private void OnEnable() 
    { 
        if (_viewers != null && _viewers.Length > 0)
        {
            foreach (var viewer in _viewers)
                viewer.ViewerClicked += OnViewerClicked;
        }

        _towerMenu.SellButtonClick += OnSellButtonClick;
    }

    private void OnDisable() 
    {
        if (_viewers != null && _viewers.Length > 0)
        {
            foreach (var viewer in _viewers)
                viewer.ViewerClicked -= OnViewerClicked;
        }

        _towerMenu.SellButtonClick -= OnSellButtonClick;
    }

    private void OnViewerClicked(Tower tower, TowerPlace towerPlace)
    {
        if (_player.Money < tower.Price)
            return;

        _player.TakeMoney(tower.Price);

        towerPlace.SetTower(tower);
    }

    private void OnSellButtonClick(TowerPlace towerPlace)
    {
        _player.AddMoney(towerPlace.Tower.SellPrice);

        towerPlace.Clear();
    }
}