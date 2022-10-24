public class PlayerDyingTransition : Transition
{
    private void Update()
    {
        if (Player.IsAlive == false)
            ReadyToTransition = true;
    }
}