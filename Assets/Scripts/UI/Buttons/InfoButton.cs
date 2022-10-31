using UnityEngine;

public class InfoButton : UnityEngine.UI.Extensions.Button
{
    [SerializeField] private InfoMenu _infoMenu;

    protected override void OnButtonClick()
    {
        _infoMenu.gameObject.SetActive(true);
    }
}
