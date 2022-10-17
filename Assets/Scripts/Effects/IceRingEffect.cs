public class IceRingEffect : Effect
{
    protected override void OnParticleSystemStopped()
    {
        IceTower.EffectPool.AddInstance(ParticleSystem);
    }
}