using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    [SerializeField] private int _price;
    [SerializeField] protected float FiringRate;
    [SerializeField] protected float FireRange;
    [SerializeField] protected LayerMask EnemiesLayerMask;

    protected float LastShootTime;

    public int Price => _price;
}