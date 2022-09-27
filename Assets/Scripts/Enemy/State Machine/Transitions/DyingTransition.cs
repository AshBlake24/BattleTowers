using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class DyingTransition : Transition
{
    private Enemy _self;

    private void OnEnable()
    {
        _self = GetComponent<Enemy>();
    }

    private void Update()
    {
        if (_self.IsAlive == false)
            ReadyToTransition = true;
    }
}