using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    protected const string CheckTargetsMethod = "CheckTargets";
    protected const float UpdateTargetsPerFrame = 2;

    [SerializeField] private int _price;

    [Header("Tower Settings")]
    [SerializeField] protected LayerMask EnemiesLayerMask;
    [SerializeField] protected Transform FirePoint;
    [SerializeField] protected float FiringRate;
    [SerializeField] protected float FireRange;

    protected float LastShootTime;

    public int Price => _price;

    private void OnEnable() => LastShootTime = FiringRate;

    protected abstract void Shot();

    protected abstract void CheckTargets();

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, FireRange);
    }
}