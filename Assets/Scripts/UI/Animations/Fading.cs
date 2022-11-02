using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Fading : MonoBehaviour
{
    [SerializeField] private Color _color;
    [SerializeField] private float _duration;
    [SerializeField] private float _minOpacity;

    private Image _image;
    private Tween _tween;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        _image.color = _color;

        _tween = _image.DOFade(_minOpacity, _duration)
                       .SetLoops(-1, LoopType.Yoyo)
                       .SetEase(Ease.Linear);
    }

    private void OnDisable()
    {
        _tween.Kill();
    }
}