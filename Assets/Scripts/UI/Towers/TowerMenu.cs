using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerMenu : MonoBehaviour
{
    [SerializeField] private Button _sellButton;
    [SerializeField] private TMP_Text _price;

    public event Action ButtonClick;
    public event Action<TowerPlace> SellButtonClick;

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
        _price.text = $"{towerPlace.Tower.SellPrice} $"; 
    }

    private void OnSellButtonClick()
    {
        ButtonClick?.Invoke();
        SellButtonClick?.Invoke(_selectedTowerPlace);
    }
}