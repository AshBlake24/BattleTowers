public class SmokeEffect : Effect
{
    protected override void OnParticleSystemStopped()
    {
        CannonTower.EffectPool.AddInstance(ParticleSystem);
    }
}