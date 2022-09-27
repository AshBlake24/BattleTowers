using UnityEngine;

public class CelebrationState : State
{
    private void OnEnable()
    {
        Debug.Log("Celebration");
    }

    private void OnDisable()
    {
        Debug.Log("NOT Celebration");
    }
}