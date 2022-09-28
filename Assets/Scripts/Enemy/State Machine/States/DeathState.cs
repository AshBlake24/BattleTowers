using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DeathState : State
{
    private const string Died = "Died";

    [SerializeField] private int DelayBeforeDestroy;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _animator.SetTrigger(Died);

        Destroy(gameObject, DelayBeforeDestroy);
    }
}