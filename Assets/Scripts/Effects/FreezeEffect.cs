public class FreezeEffect : Effect
{
    protected override void OnParticleSystemStopped()
    {
        Enemy.FreezeEffectPool.AddInstance(ParticleSystem);
    }
}