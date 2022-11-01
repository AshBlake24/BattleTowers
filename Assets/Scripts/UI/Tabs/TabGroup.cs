using System.Collections.Generic;
using UnityEngine;

public class TabGroup : MonoBehaviour
{
    [SerializeField] private Sprite _tabIdle;
    [SerializeField] private Sprite _tabSelected;

    private List<TabButton> _buttons;
    private TabButton _selectedTab;

    public void Subscribe(TabButton button)
    {
        if (_buttons == null)
            _buttons = new List<TabButton>();

        _buttons.Add(button);
    }

    public void Unsubscribe(TabButton button)
    {
        if (_buttons.Contains(button))
            _buttons.Remove(button);
    }

    public void OnTabSelected(TabButton button)
    {
        if (_selectedTab != null)
            _selectedTab.Deselect();

        _selectedTab = button;
        _selectedTab.Select();

        ResetTabs();

        _selectedTab.ChangeSprite(_tabSelected);
    }

    private void ResetTabs()
    {
        foreach (var button in _buttons)
            button.ChangeSprite(_tabIdle);
    }
}