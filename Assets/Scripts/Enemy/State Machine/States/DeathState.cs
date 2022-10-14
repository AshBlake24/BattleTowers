using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DeathState : State
{
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