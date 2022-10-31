using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TabGroup _tabGroup;

    private Image _image;

    public UnityEvent OnTabSelected;
    public UnityEvent OnTabDeselected;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        _tabGroup.Subscribe(this);
    }

    private void OnDisable()
    {
        _tabGroup.Unsubscribe(this);
    }

    public void ChangeSprite(Sprite sprite)
    {
        _image.sprite = sprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _tabGroup.OnTabSelected(this);
    }

    public void Select()
    {
        if (OnTabSelected != null)
            OnTabSelected?.Invoke();
    }

    public void Deselect()
    {
        if (OnTabDeselected != null)
            OnTabDeselected?.Invoke();
    }
}