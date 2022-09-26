using UnityEngine;

public abstract class Transition : MonoBehaviour
{
    [SerializeField] private State _targetState;

    public State TargetState => _targetState;
    public bool NeedTransition { get; protected set; }
    protected GameObject Target { get; private set; }

    public void Init(GameObject target)
    {
        Target = target;
    }

    private void OnEnable()
    {
        NeedTransition = false;
    }
}