using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    [SerializeField] protected float FiringRate;
    [SerializeField] protected int EnemiesLayerMask;
    [SerializeField] private int _price;

    protected float LastShootTime;

    public float Price => _price;
}

public enum TowerType
{
    Archery,
    Fire,
    Ice
}