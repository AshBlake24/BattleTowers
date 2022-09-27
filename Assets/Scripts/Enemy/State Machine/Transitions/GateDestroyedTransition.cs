using UnityEngine;

public class GateDestroyedTransition : Transition
{
    private void Update()
    {
        if (Target.IsDestroyed == true)
            ReadyToTransition = true;
    }
}