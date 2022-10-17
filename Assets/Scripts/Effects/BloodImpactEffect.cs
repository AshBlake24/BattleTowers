public class BloodImpactEffect : Effect
{
    protected override void OnParticleSystemStopped()
    {
        Arrow.EffectPool.AddInstance(ParticleSystem);
    }
}