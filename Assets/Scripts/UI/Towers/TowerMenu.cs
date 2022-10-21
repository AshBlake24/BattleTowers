using System;
using UnityEngine;
using UnityEngine.UI;

public class TowerMenu : MonoBehaviour
{
    [SerializeField] private Button _sellButton;

    public event Action ButtonClick;
    public event Action<TowerPlace, int> SellButtonClick;

    private TowerPlace _selectedTowerPlace;

    private void OnEnable()
    {
        _sellButton.onClick.AddListener(OnSellButtonClick);
    }

    private void OnDisable()
    {
        _sellButton.onClick.RemoveListener(OnSellButtonClick);
    }

    public void Init(TowerPlace towerPlace)
    {
        _selectedTowerPlace = towerPlace;
    }

    private void OnSellButtonClick()
    {
        ButtonClick?.Invoke();
        SellButtonClick?.Invoke(_selectedTowerPlace, _selectedTowerPlace.Tower.Price);
    }
}