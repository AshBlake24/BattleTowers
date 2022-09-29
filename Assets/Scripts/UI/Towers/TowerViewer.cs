using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class TowerViewer : MonoBehaviour
{
    [SerializeField] private Tower _tower;

    public event Action<Tower, TowerPlaceholder> ViewerClicked;

    private Button _button;
    private TowerPlaceholder _towerPlaceholder;

    private void Awake() => _button = GetComponent<Button>();

    private void OnEnable() => _button.onClick.AddListener(OnButtonClick);

    private void OnDisable() => _button.onClick.RemoveListener(OnButtonClick);

    public void Init(TowerPlaceholder placeholder) => _towerPlaceholder = placeholder;

    public void Clear() => _towerPlaceholder = null;

    private void OnButtonClick() => ViewerClicked?.Invoke(_tower, _towerPlaceholder);
}