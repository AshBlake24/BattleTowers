using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using DG.Tweening;
using System;

public class SelectionMenu : MonoBehaviour
{
    private const float OpenSize = 1;
    private const float CloseSize = 0;

    [SerializeField] private Player _player;
    [SerializeField] private TowerMenu _towerMenu;
    [SerializeField] private GameObject _selectorMenu;
    [SerializeField] private List<TowerViewer> _towerViewers;
    [SerializeField] private float _openDuration;
    [SerializeField] private float _closeDuration;
    [SerializeField] private float _raycastDistance;

    private void OnEnable()
    {
        foreach (var viewer in _towerViewers)
            viewer.ButtonClick += OnButtonClick;

        _towerMenu.ButtonClick += OnButtonClick;
    }

    private void Update()
    {
        if (Input.touchCount > 0 && EventSystem.current.IsPointerOverGameObject() == false)
        {
            Ray ray = Helpers.Camera.ScreenPointToRay(Input.touches[0].position);

            TryOpenSelectionMenu(ray);
        }
    }

    private void OnDisable()
    {
        foreach (var viewer in _towerViewers)
            viewer.ButtonClick -= OnButtonClick;

        _towerMenu.ButtonClick -= OnButtonClick;
    }

    public void OpenSelectorMenu(TowerPlace towerPlace)
    {
        if (_selectorMenu.activeSelf == false)
            _selectorMenu.SetActive(true);

        foreach (var viewer in _towerViewers)
            viewer.Init(towerPlace, _player.Money);

        _selectorMenu.transform.DOScale(OpenSize, _openDuration).From(Vector3.zero)
                      .SetEase(Ease.OutBounce);
    }

    public void CloseSelectorMenu()
    {
        if (_selectorMenu.activeSelf == false)
            return;

        foreach (var viewer in _towerViewers)
            viewer.Clear();

        _selectorMenu.transform.DOScale(CloseSize, _closeDuration)
                      .SetEase(Ease.InOutQuad);

        _selectorMenu.SetActive(false);
    }

    public void OpenTowerMenu(TowerPlace towerPlace)
    {
        if (_towerMenu.gameObject.activeSelf == false)
            _towerMenu.gameObject.SetActive(true);

        _towerMenu.Init(towerPlace);
    }

    public void CloseTowerMenu()
    {
        if (_towerMenu.gameObject.activeSelf == false)
            return;

        _towerMenu.gameObject.SetActive(false);
    }

    private void TryOpenSelectionMenu(Ray ray)
    {
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _raycastDistance))
        {
            if (hit.collider.TryGetComponent(out TowerPlace towerPlace))
            {
                if (towerPlace.HasTower == false)
                {
                    CloseTowerMenu();

                    _selectorMenu.transform.position = Input.touches[0].position;

                    OpenSelectorMenu(towerPlace);
                }
                else
                {
                    CloseSelectorMenu();

                    _towerMenu.transform.position = Helpers.Camera.WorldToScreenPoint(towerPlace.transform.position);

                    OpenTowerMenu(towerPlace);
                }
            }
            else
            {
                CloseAllMenues();
            }
        }
        else
        {
            CloseAllMenues();
        }
    }

    private void CloseAllMenues()
    {
        CloseSelectorMenu();
        CloseTowerMenu();
    }

    private void OnButtonClick() => CloseAllMenues();
}