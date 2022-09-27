using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public event Action<Enemy> Died;

    public Gate Target { get; private set; }

    public void Init(Gate target)
    {
        Target = target;
    }
}