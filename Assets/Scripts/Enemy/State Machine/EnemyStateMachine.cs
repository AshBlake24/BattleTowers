using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private State _startState;
    
    protected Enemy Self;
    private State _currentState;
    private Gates _target;
    private State[] _states;
    private Transition[] _transitions;

    private void Start()
    {
        _states = GetComponents<State>();
        _transitions = GetComponents<Transition>();
    }

    private void Update()
    {
        if (_currentState == null)
            return;

        State nextState = _currentState.GetNextState();

        if (nextState != null)
            ChangeState(nextState);
    }

    private void OnDisable()
    {
        ResetToDefault();
    }

    public void Init(Gates target, Enemy self)
    {
        Self = self;
        _target = target;

        ResetToDefault();
    }

    private void ChangeState(State state)
    {
        if (_currentState != null)
            _currentState.Exit();

        _currentState = state;

        if (_currentState != null)
            _currentState.Enter(_target, Self);
    }

    private void ResetToDefault()
    {
        if (_states != null)
        {
            foreach (var state in _states)
                state.enabled = false;
        }

        if (_transitions != null)
        {
            foreach (var transition in _transitions)
                transition.enabled = false;
        }

        ChangeState(_startState);
    }
}