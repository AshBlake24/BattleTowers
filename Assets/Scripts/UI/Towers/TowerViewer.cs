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
    [SerializeField] private Image _icon;
    [SerializeField] private Image _frame;
    [SerializeField] private Color _activeFrameColor;
    [SerializeField] private Color _inactiveFrameColor;

    public event Action<Tower, TowerPlaceholder> ViewerClicked;
    
    private Button _button;
    private TowerPlaceholder _towerPlaceholder;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable() => _button.onClick.AddListener(OnButtonClick);

    private void OnDisable() => _button.onClick.RemoveListener(OnButtonClick);

    public void Init(TowerPlaceholder placeholder, int playerMoney)
    {
        _towerPlaceholder = placeholder;

        if (playerMoney >= _towerPrefab.Price)
            ActivateViewer();
        else
            DeactivateViewer();
    }
   

    public void Clear() => _towerPlaceholder = null;

    private void OnButtonClick() => ViewerClicked?.Invoke(_towerPrefab, _towerPlaceholder);

    private void ActivateViewer()
    {
        _icon.sprite = _towerActiveIcon;
        _frame.color = _activeFrameColor;
        _button.interactable = true;
    }

    private void DeactivateViewer()
    {
        _icon.sprite = _towerInactiveIcon;
        _frame.color = _inactiveFrameColor;
        _button.interactable = false;
    }
}