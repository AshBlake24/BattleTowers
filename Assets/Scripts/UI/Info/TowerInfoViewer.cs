using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class TowerInfoViewer : InfoViewer
{
    [SerializeField] private TMP_Text _damage;
    [SerializeField] private TMP_Text _attackRate;
    [SerializeField] private Tower[] _towers;

    public void Init(Tower tower)
    {
        Image.sprite = tower.Icon;
        Label.text = tower.Name;
        Info.text = $"Info: {tower.Info}";
        _attackRate.text = $"Attack Per Second: {tower.GetAttackPerSecond()}";

        if (tower is IceTower)
        {
            IceTower iceTower = tower as IceTower;
            _damage.text = $"Duration: {iceTower.Duration}";
        }
        else
        {
            _damage.text = $"Damage: {tower.GetDamage()}";
        }
    }

    protected override void UpdateInfoViewer()
    {
        Init(_towers[CurrentPage]);
    }

    protected override void UpdateButtonsOnPage()
    {
        base.UpdateButtonsOnPage();

        if (CurrentPage >= _towers.Length - 1)
            DisableButton(NextPageButton);
    }

    protected override void OnNextPageButtonClick()
    {
        if (CurrentPage < _towers.Length - 1)
        {
            CurrentPage++;
            UpdateButtonsOnPage();
            UpdateInfoViewer();
        }
    }

    protected override void OnPreviousPageButtonClick()
    {
        if (CurrentPage > 0)
        {
            CurrentPage--;
            UpdateButtonsOnPage();
            UpdateInfoViewer();
        }
    }
}