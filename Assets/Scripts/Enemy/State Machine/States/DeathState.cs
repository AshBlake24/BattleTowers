using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DeathState : State
{
    [SerializeField] private int DelayBeforeDestroy;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _animator.SetTrigger(AnimatorEnemyController.Triggers.Die);
    }
}