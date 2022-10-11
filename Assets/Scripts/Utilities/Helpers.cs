using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helpers
{
    #region Camera

    private static Camera _camera;

    public static Camera Camera
    {
        get
        {
            if (_camera == null)
                _camera = Camera.main;

            return _camera;
        }
    }

    #endregion

    #region Time

    private static readonly Dictionary<float, WaitForSeconds> WaitDictionary = new Dictionary<float, WaitForSeconds>();

    public static WaitForSeconds GetTime(float timeInSeconds)
    {
        if (WaitDictionary.TryGetValue(timeInSeconds, out WaitForSeconds wait))
            return wait;

        WaitDictionary[timeInSeconds] = new WaitForSeconds(timeInSeconds);

        return WaitDictionary[timeInSeconds];
    }

    #endregion Time

    #region Pool

    public static ParticleSystem GetEffectFromPool(ObjectsPool<ParticleSystem> pool, Vector3 position, Quaternion rotation)
    {
        ParticleSystem effect = pool.GetInstanceFromPool();

        effect.gameObject.SetActive(true);
        effect.transform.SetPositionAndRotation(position, rotation);

        return effect;
    }

    public static IEnumerator DeactivateEffectWithDelay(ParticleSystem effect)
    {
        yield return GetTime(effect.main.duration);

        effect.gameObject.SetActive(false);
    }

    public static IEnumerator DeactivateObjectWithDelay(GameObject instance, float delay)
    {
        yield return GetTime(delay);

        instance.SetActive(false);
    }

    #endregion
}
