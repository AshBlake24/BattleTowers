using UnityEngine;

public abstract class Transition : MonoBehaviour
{
    [SerializeField] private State _targetState;

    public bool ReadyToTransition { get; protected set; }
    protected Enemy Self { get; private set; }
    protected Player Player { get; private set; }
    public State TargetState => _targetState;

    public void Init(Player player, Enemy self)
    {
        Player = player;
        Self = self;
    }

    private void OnEnable()
    {
        ReadyToTransition = false;
    }
}