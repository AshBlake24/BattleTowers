using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    [SerializeField] protected float FiringRate;
    [SerializeField] protected float FireRange;
    [SerializeField] private int _price;

    protected float LastShootTime;

    public float Price => _price;
}