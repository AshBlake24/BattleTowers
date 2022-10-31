using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class EnemyInfoViewer : InfoViewer
{
    [SerializeField] private TMP_Text _health;
    [SerializeField] private Enemy[] _enemies;

    public void Init(Enemy enemy)
    {
        Image.sprite = enemy.Icon;
        Label.text = enemy.Name;
        Info.text = $"Info: {enemy.Info}";
        _health.text = $"HP: {enemy.Health}";
    }

    protected override void UpdateInfoViewer()
    {
        Init(_enemies[CurrentPage]);
    }

    protected override void UpdateButtonsOnPage()
    {
        if (CurrentPage <= 0)
        {
            DisableButton(PreviousPageButton);
            EnableButton(NextPageButton);
        }
        else if (CurrentPage >= _enemies.Length - 1)
        {
            DisableButton(NextPageButton);
            EnableButton(PreviousPageButton);
        }
        else
        {
            EnableButton(NextPageButton);
            EnableButton(PreviousPageButton);
        }
    }

    protected override void OnNextPageButtonClick()
    {
        if (CurrentPage < _enemies.Length - 1)
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