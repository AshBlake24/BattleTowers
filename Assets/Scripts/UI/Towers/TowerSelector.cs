using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using DG.Tweening;

public class TowerSelector : MonoBehaviour
{
    private const float OpenSize = 1;
    private const float CloseSize = 0;

    [SerializeField] private GameObject _selectionMenu;
    [SerializeField] private List<TowerViewer> _towerViewers;
    [SerializeField] private float _openDuration;
    [SerializeField] private float _closeDuration;

    private void OnEnable()
    {
        foreach (var viewer in _towerViewers)
            viewer.ViewerClicked += OnViewerClicked;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            TryOpenSelectionMenu(ray);
        }
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

        _selectionMenu.transform.DOScale(OpenSize, _openDuration).From(Vector3.zero)
                      .SetEase(Ease.OutBounce);
    }

    public void CloseUI()
    {
        foreach (var viewer in _towerViewers)
            viewer.Clear();

        _selectionMenu.transform.DOScale(CloseSize, _closeDuration)
                      .SetEase(Ease.InOutQuad);

        _selectionMenu.SetActive(false);
    }

    private void TryOpenSelectionMenu(Ray ray)
    {
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 200))
        {
            if (hit.collider.TryGetComponent(out TowerPlaceholder placeholder))
            {
                _selectionMenu.transform.position = Input.mousePosition;

                if (_selectionMenu.activeSelf)
                {
                    OpenUI(placeholder);
                }
                else
                {
                    _selectionMenu.SetActive(true);
                    OpenUI(placeholder);
                }

            }
            else if (_selectionMenu.activeSelf)
            {
                CloseUI();
            }
        }
        else if (_selectionMenu.activeSelf)
        {
            CloseUI();
        }
    }

    private void OnViewerClicked(Tower tower, TowerPlaceholder placeholder)
    {
        CloseUI();
    }
}