using UnityEngine;

public abstract class Transition : MonoBehaviour
{
    [SerializeField] private State _targetState;

    public State TargetState => _targetState;
    public bool ReadyToTransition { get; protected set; }
    protected Gate Target { get; private set; }
    protected Enemy Self { get; private set; }

    public void Init(Gate target, Enemy self)
    {
        Target = target;
        Self = self;
    }

    private void OnEnable()
    {
        ReadyToTransition = false;
    }
}