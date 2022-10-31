using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    protected const string CheckTargetsMethod = "CheckTargets";
    protected const float UpdateTargetsPerFrame = 2;

    [Header("Tower InfoViewer")]
    [SerializeField] private string _name;
    [SerializeField] private int _buyPrice;
    [SerializeField] private int _sellPrice;
    [SerializeField] private Sprite _towerIcon;
    [SerializeField, TextArea(1,4)] private string _info;

    [Header("Tower Settings")]
    [SerializeField] protected LayerMask EnemiesLayerMask;
    [SerializeField] protected Transform FirePoint;
    [SerializeField] protected float AttackPerSecond;
    [SerializeField] protected float FireRange;

    protected float LastShootTime;

    public int Damage { get; protected set; }
    public int Price => _buyPrice;
    public int SellPrice => _sellPrice;
    public string Name => _name;
    public string Info => _info;
    public Sprite Icon => _towerIcon;

    private void OnEnable()
    {
        LastShootTime = AttackPerSecond;
    }

    public float GetAttackPerSecond() => AttackPerSecond;

    public abstract int GetDamage();

    protected abstract void Shot();

    protected abstract void CheckTargets();

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, FireRange);
    }
}