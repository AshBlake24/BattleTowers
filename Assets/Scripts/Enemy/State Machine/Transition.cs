using UnityEngine;

public abstract class Transition : MonoBehaviour
{
    [SerializeField] private State _targetState;

    public State TargetState => _targetState;
    public bool ReadyToTransition { get; protected set; }
    protected Gate Target { get; private set; }

    public void Init(Gate target)
    {
        Target = target;
    }

    private void OnEnable()
    {
        ReadyToTransition = false;
    }
}