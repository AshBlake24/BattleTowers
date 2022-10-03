using UnityEngine;

public class DyingTransition : Transition
{
    private void Update()
    {
        if (Self.IsAlive == false)
            ReadyToTransition = true;
    }
}