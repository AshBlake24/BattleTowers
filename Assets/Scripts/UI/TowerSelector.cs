using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

[RequireComponent(typeof(RectTransform))]
public class TowerSelector : MonoBehaviour
{
    private const float OpenSize = 1;
    private const float CloseSize = 0;

    [SerializeField] private List<TowerViewer> _towerViewers;
    [SerializeField] private float _openDuration;
    [SerializeField] private float _closeDuration;

    private RectTransform _rectTransform;

    private void Awake() => _rectTransform = GetComponent<RectTransform>();

    private void OnEnable()
    {
        foreach (var viewer in _towerViewers)
            viewer.ViewerClicked += OnViewerClicked;
    }

    private void OnDisable()
    {
        foreach (var viewer in _towerViewers)
            viewer.ViewerClicked -= OnViewerClicked;
    }

    public void OpenUI(TowerPlaceholder placeholder)
    {
        foreach (var viewer in _towerViewers)
            viewer.Init(placeholder);

        _rectTransform.DOScale(OpenSize, _openDuration).From(Vector3.zero)
                      .SetEase(Ease.OutBounce);
    }

    public void CloseUI()
    {
        foreach (var viewer in _towerViewers)
            viewer.Clear();

        _rectTransform.DOScale(CloseSize, _closeDuration)
                      .SetEase(Ease.InOutQuad);

        gameObject.SetActive(false);
    }

    private void OnViewerClicked(Tower tower, TowerPlaceholder placeholder)
    {
        CloseUI();
    }
}