using UnityEngine;

public abstract class State : MonoBehaviour
{
    [SerializeField] private Transition[] _transitions;

    protected GameObject Target { get; set; }

    public void Enter(GameObject target)
    {
        if (enabled == false)
        {
            enabled = true;

            Target = target;

            foreach (var transition in _transitions)
            {
                transition.enabled = true;
                transition.Init(target);
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
                if (transition.NeedTransition)
                    return transition.TargetState;
            }
        }

        return null;
    }
}