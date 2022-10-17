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
}