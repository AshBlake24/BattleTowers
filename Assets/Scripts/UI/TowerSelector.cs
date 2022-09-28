using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class TowerSelector : MonoBehaviour
{
    private const float OpenSize = 1;
    private const float CloseSize = 0;

    [SerializeField] private Button _tower1;
    [SerializeField] private Button _tower2;
    [SerializeField] private Button _tower3;
    [SerializeField] private float _popUpDuration;

    private RectTransform _rectTransform;

    private void Awake() => _rectTransform = GetComponent<RectTransform>();

    private void OnEnable() => OpenUI();

    private void OnDisable() => CloseUI();

    public void OpenUI()
    {
        _rectTransform.DOScale(OpenSize, _popUpDuration).From(Vector3.zero)
                      .SetEase(Ease.OutBounce);
    }

    private void CloseUI()
    {
        _rectTransform.DOScale(CloseSize, _popUpDuration)
                      .SetEase(Ease.InOutQuad);
    }
}