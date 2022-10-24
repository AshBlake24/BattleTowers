using UnityEngine;

public abstract class State : MonoBehaviour
{
    [SerializeField] private Transition[] _transitions;

    protected Enemy Self { get; private set; }
    protected Player Player { get; private set; }

    public void Enter(Player player, Enemy self)
    {
        if (enabled == false)
        {
            enabled = true;

            Player = player;
            Self = self;

            foreach (var transition in _transitions)
            {
                transition.enabled = true;
                transition.Init(Player, Self);
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