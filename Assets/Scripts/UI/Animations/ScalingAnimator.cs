using UnityEngine;
using DG.Tweening;

public class ScalingAnimator : MonoBehaviour
{
    [SerializeField] private float _scaleModifier;
    [SerializeField] private float _duration;

    private Vector3 _defaultScale;
    private Tween _tween;

    private void Awake()
    {
        _defaultScale = transform.localScale;
    }

    private void OnEnable()
    {
        transform.localScale = _defaultScale;

        _tween = transform.DOScale(_scaleModifier, _duration)
                          .SetLoops(-1, LoopType.Yoyo)
                          .SetEase(Ease.InOutQuad);
    }

    private void OnDisable()
    {
        _tween.Kill();
    }
}