using System.Collections;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    private const float Frequency = 1.0f;

    private int _framesPerSec;
    private string _fps;
    private GUIStyle _style;

    private void Start()
    {
        _style = new GUIStyle();
        _style.fontSize = 40;
        _style.normal.textColor = Color.white;

        StartCoroutine(CountFPS());   
    }

    private IEnumerator CountFPS()
    {
        for (;;)
        {
            _framesPerSec = Time.frameCount;
            float lastTime = Time.realtimeSinceStartup;

            yield return Helpers.GetTime(Frequency);

            float timeSpan = Time.realtimeSinceStartup - lastTime;
            _framesPerSec = Time.frameCount - _framesPerSec;

            _fps = string.Format("FPS: {0}", Mathf.RoundToInt(_framesPerSec / timeSpan));
        }
    }

    private void OnGUI()
    {
        GUI.color = Color.white;
        GUI.Label(new Rect(UnityEngine.Screen.width - 200, 10, 300, 40), _fps, _style);
    }
}