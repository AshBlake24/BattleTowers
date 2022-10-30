using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class TowerViewer : MonoBehaviour
{
    [Header("Tower Settings")]
    [SerializeField] private Tower _towerPrefab;
    [SerializeField] private Sprite _towerActiveIcon;
    [SerializeField] private Sprite _towerInactiveIcon;

    [Header("Viewer Settings")]
    [SerializeField] private Image _towerIcon;
    [SerializeField] private TMP_Text _price;

    public event Action ButtonClick;
    public event Action<Tower, TowerPlace> ViewerClicked;
    
    private Button _button;
    private TowerPlace _towerPlace;
    private int _towerPrice;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _towerPrice = _towerPrefab.Price;
    }

    private void OnEnable() => _button.onClick.AddListener(OnButtonClick);

    private void OnDisable() => _button.onClick.RemoveListener(OnButtonClick);

    public void Init(TowerPlace towerPlace, int playerMoney)
    {
        _towerPlace = towerPlace;

        if (playerMoney >= _towerPrefab.Price)
            ActivateViewer();
        else
            DeactivateViewer();
    }   

    public void Clear() => _towerPlace = null;

    private void OnButtonClick()
    {
        ViewerClicked?.Invoke(_towerPrefab, _towerPlace);
        ButtonClick?.Invoke();
    }

    private void ActivateViewer()
    {
        _towerIcon.sprite = _towerActiveIcon;
        _button.interactable = true;
        _price.text = $"{_towerPrice} $";
    }

    private void DeactivateViewer()
    {
        _towerIcon.sprite = _towerInactiveIcon;
        _button.interactable = false;
    }
}