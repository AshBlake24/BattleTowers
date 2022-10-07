using UnityEngine;

public class TowerPlace : MonoBehaviour
{
    private Placeholder _placeholder;
    private Tower _tower;

    public bool HasTower => _tower != null;
    public Tower Tower => _tower;

    private void Start()
    {
        _placeholder = GetComponentInChildren<Placeholder>();
    }

    public void Clear()
    {
        if (_tower == null)
            return;

        Destroy(_tower.gameObject);
        ActivatePlaceholder();
    }

    public void SetTower(Tower tower)
    {
        if (_tower != null)
            return;

        DeactivatePlaceholder();

        _tower = Instantiate(tower, transform.position, transform.rotation, transform);
    }

    private void ActivatePlaceholder() 
    {
        if (_placeholder.gameObject.activeSelf)
            return;

        _placeholder.gameObject.SetActive(true);
    }

    private void DeactivatePlaceholder()
    {
        if (_placeholder.gameObject.activeSelf == false)
            return;

        _placeholder.gameObject.SetActive(false);
    }
}