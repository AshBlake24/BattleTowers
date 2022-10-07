using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private State _startState;

    protected Enemy Self;
    private State _currentState;
    private Gates _target;

    private void Start()
    {
        Self = GetComponent<Enemy>();
        _target = Self.Target;
        Reset(_startState);
    }

    private void Update()
    {
        if (_currentState == null)
            return;

        State nextState = _currentState.GetNextState();

        if (nextState != null)
            ChangeState(nextState);
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
            _currentState.Enter(_target, Self);
    }
}