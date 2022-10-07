using UnityEngine;

public class DistanceTransition : Transition
{
    [SerializeField] private float _distanceToTarget;
    [SerializeField] private float _spreadDistance;

    private void OnEnable()
    {
        _distanceToTarget += Random.Range(-_spreadDistance, _spreadDistance);
    }

    private void Update()
    {
        var distance = Vector3.Distance(transform.position, Target.transform.position);

        if (distance <= _distanceToTarget)
            ReadyToTransition = true;
    }
}