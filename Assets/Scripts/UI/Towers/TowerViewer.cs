using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class TowerViewer : MonoBehaviour
{
    [Header("Tower Settings")]
    [SerializeField] private Tower _towerPrefab;
    [SerializeField] private Sprite _towerIcon;

    [Header("Viewer Settings")]
    [SerializeField] private Image _icon;
    [SerializeField] private Image _frame;

    public event Action<Tower, TowerPlaceholder> ViewerClicked;

    
    private Button _button;
    private TowerPlaceholder _towerPlaceholder;

    private void Awake()
    {
        _button = GetComponent<Button>();

        _icon.sprite = _towerIcon;
    }

    private void OnEnable() => _button.onClick.AddListener(OnButtonClick);

    private void OnDisable() => _button.onClick.RemoveListener(OnButtonClick);

    public void Init(TowerPlaceholder placeholder) => _towerPlaceholder = placeholder;

    public void Clear() => _towerPlaceholder = null;

    private void OnButtonClick() => ViewerClicked?.Invoke(_towerPrefab, _towerPlaceholder);
}