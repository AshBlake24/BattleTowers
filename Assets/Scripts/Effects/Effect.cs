using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public abstract class Effect : MonoBehaviour
{
    protected ParticleSystem ParticleSystem;

    private void OnEnable()
    {
        if (ParticleSystem == null)
            ParticleSystem = GetComponent<ParticleSystem>();
    }

    protected abstract void OnParticleSystemStopped();
}