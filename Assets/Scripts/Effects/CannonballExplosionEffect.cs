public class CannonballExplosionEffect : Effect
{
    protected override void OnParticleSystemStopped()
    {
        Cannonball.EffectPool.AddInstance(ParticleSystem);
    }
}