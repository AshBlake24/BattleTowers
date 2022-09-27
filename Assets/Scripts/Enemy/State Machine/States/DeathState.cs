using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DeathState : State
{
    private const string Died = "Died";
    private const int DelayBeforeDestroy = 3;

    private Animator _animator;

    private static WaitForSeconds _delay = new WaitForSeconds(DelayBeforeDestroy);

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _animator.SetTrigger(Died);

        StartCoroutine(DestroyDelay());
    }

    private IEnumerator DestroyDelay()
    {
        yield return _delay;

        Destroy(gameObject);
    }
}