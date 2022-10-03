using UnityEngine;

public abstract class State : MonoBehaviour
{
    [SerializeField] private Transition[] _transitions;

    protected Gate Target { get; set; }
    protected Enemy Self { get; private set; }

    public void Enter(Gate target, Enemy self)
    {
        if (enabled == false)
        {
            enabled = true;

            Target = target;
            Self = self;

            foreach (var transition in _transitions)
            {
                transition.enabled = true;
                transition.Init(target, Self);
            }
        }
    }

    public void Exit()
    {
        if (enabled)
        {
            foreach (var transition in _transitions)
                transition.enabled = false;

            enabled = false;
        }
    }

    public State GetNextState()
    {
        if (_transitions.Length > 0)
        {
            foreach (var transition in _transitions)
            {
                if (transition.ReadyToTransition)
                    return transition.TargetState;
            }
        }

        return null;
    }
}