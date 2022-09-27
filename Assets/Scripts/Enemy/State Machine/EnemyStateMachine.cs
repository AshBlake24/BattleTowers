using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private State _startState;

    private State _currentState;
    private Gate _target;

    private void Start()
    {
        _target = GetComponent<Enemy>().Target;
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
            _currentState.Enter(_target);
    }
}