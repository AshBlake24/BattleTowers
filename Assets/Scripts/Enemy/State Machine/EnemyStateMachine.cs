using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private State _startState;

    private Enemy _self;
    private Player _player;
    private State _currentState;

    private State[] _states;
    private Transition[] _transitions;

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
        if (_states != null)
        {
            foreach (var state in _states)
            {
                state.enabled = false;
            }
        }

        if (_transitions != null)
        {
            foreach (var transition in _transitions)
            {
                transition.enabled = false;
            }
        }
    }

    public void Init(Player player, Enemy enemy)
    {
        if (_states == null)
            _states = GetComponents<State>();

        if (_transitions == null)
            _transitions = GetComponents<Transition>();

        _self = enemy;
        _player = player;
        _currentState = null;

        Reset(_startState);
    }

    private void Reset(State startState)
    {
        ChangeState(startState);
    }

    private void ChangeState(State state)
    {
        if (_currentState != null)
            _currentState.Exit();

        _currentState = state;

        if (_currentState != null)
            _currentState.Enter(_player, _self);
    }
}