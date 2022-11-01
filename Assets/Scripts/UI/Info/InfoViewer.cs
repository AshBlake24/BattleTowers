using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class InfoViewer : MonoBehaviour
{
    [SerializeField] protected Button NextPageButton;
    [SerializeField] protected Button PreviousPageButton;
    [SerializeField] protected Image Image;
    [SerializeField] protected TMP_Text Info;
    [SerializeField] protected TMP_Text Label;

    protected int CurrentPage;

    private void OnEnable()
    {
        CurrentPage = 0;
        NextPageButton.onClick.AddListener(OnNextPageButtonClick);
        PreviousPageButton.onClick.AddListener(OnPreviousPageButtonClick);

        UpdateInfoViewer();
        UpdateButtonsOnPage();
    }

    private void OnDisable()
    {
        NextPageButton.onClick.RemoveListener(OnNextPageButtonClick);
        PreviousPageButton.onClick.RemoveListener(OnPreviousPageButtonClick);
    }

    protected abstract void OnNextPageButtonClick();

    protected abstract void OnPreviousPageButtonClick();

    protected abstract void UpdateInfoViewer();

    protected virtual void UpdateButtonsOnPage()
    {
        EnableButton(NextPageButton);
        EnableButton(PreviousPageButton);

        if (CurrentPage <= 0)
            DisableButton(PreviousPageButton);
    }

    protected void EnableButton(Button button)
    {
        button.gameObject.SetActive(true);
        button.interactable = true;
    }

    protected void DisableButton(Button button)
    {
        button.interactable = false;
        button.gameObject.SetActive(false);
    }
}
