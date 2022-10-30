using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CelebrationState : State
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _animator.SetBool(AnimatorEnemyController.States.Celebrate, true);
    }

    private void OnDisable()
    {
        _animator.SetBool(AnimatorEnemyController.States.Celebrate, false);
    }
}