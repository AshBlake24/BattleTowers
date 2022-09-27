using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Gate _target;

    public Gate Target => _target;
}
