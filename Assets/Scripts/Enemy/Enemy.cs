using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject _target;

    public GameObject Target => _target;
}
