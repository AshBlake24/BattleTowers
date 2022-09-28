using UnityEngine;
using UnityEngine.EventSystems;

public class TowerPlaceholder : MonoBehaviour
{
    [SerializeField] private Color _hoverColor;
    [SerializeField] private TowerSelector _selector;

    private Renderer _renderer;

    private Color _originColor;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _originColor = _renderer.material.color;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            TryOpenTowerSelectorUI(ray);
        }
    }

    private void OnMouseEnter()
    {
        _renderer.material.color = _hoverColor;
    }

    private void OnMouseExit()
    {
        _renderer.material.color = _originColor;
    }

    private void TryOpenTowerSelectorUI(Ray ray)
    {
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 200))
        {
            if (hit.collider.TryGetComponent(out TowerPlaceholder placeholder))
            {
                _selector.transform.position = Input.mousePosition;

                if (_selector.gameObject.activeSelf)
                    _selector.OpenUI();
                else
                    _selector.gameObject.SetActive(true);

            }
            else if (_selector.gameObject.activeSelf)
            {
                _selector.gameObject.SetActive(false);
            }
        }
    }
}