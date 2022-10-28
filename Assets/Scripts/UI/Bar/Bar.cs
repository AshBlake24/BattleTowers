using UnityEngine;
using UnityEngine.UI;

public abstract class Bar : MonoBehaviour
{
    [SerializeField] private Image _fillerMask;

    protected virtual void OnValueChanged(int value, int maxValue)
    {
        _fillerMask.fillAmount = (float)value / maxValue;
    }
}