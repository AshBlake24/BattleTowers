using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private bool _isEndpoint;

    public bool IsEndpoint => _isEndpoint;
}