using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Component
{
    private GameObject _prefab;
    private Transform _container;
    private Queue<T> _pool = new Queue<T>();

    public ObjectPool(GameObject prefab)
    {
        if (prefab == null)
        {
            Debug.Log("Prefab is null!");
            return;
        }

        if (prefab.GetComponent<T>() == null)
        {
            Debug.Log($"Prefab has no: {typeof(T)} instanceComponent");
            return;
        }

        _prefab = prefab;

        if (_container == null)
            _container = new GameObject($"Pool - {_prefab.name}").transform;
    }

    public void AddInstance(T instance)
    {
        if (instance.transform.parent != _container)
            instance.transform.SetParent(_container);

        _pool.Enqueue(instance);
        instance.gameObject.SetActive(false);
    }

    public T GetInstance()
    {
        if (_pool.Count <= 0)
            return CreateInstance();

        return _pool.Dequeue();
    }

    private T CreateInstance()
    {
        GameObject instance = Object.Instantiate(_prefab, _container);
        T instanceComponent = instance.GetComponent<T>();
        return instanceComponent;
    }
}