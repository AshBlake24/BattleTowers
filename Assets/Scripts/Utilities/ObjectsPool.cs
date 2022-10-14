using System.Collections.Generic;
using UnityEngine;

public class ObjectsPool<T> where T : Component
{
    private GameObject _prefab;
    private Transform _poolContainer;
    private Queue<T> _poolQueue = new Queue<T>();

    public ObjectsPool(GameObject prefab)
    {
        if (prefab == null)
            Debug.LogError("Prefab is null!");

        if (prefab.GetComponent<T>() == null)
            Debug.LogError($"Prefab has no: {typeof(T)} component!");

        _prefab = prefab;

        _poolContainer = new GameObject($"Pool - {_prefab.name}").transform;
    }

    public T GetInstance()
    {
        if (_poolQueue.Count <= 0)
            return CreateNewInstance();

        return _poolQueue.Dequeue();
    }

    public void AddInstance(T instance)
    {
        _poolQueue.Enqueue(instance);

        instance.gameObject.SetActive(false);
    }

    private T CreateNewInstance()
    {
        GameObject item = Object.Instantiate(_prefab, _poolContainer);

        T component = item.GetComponent<T>();

        return component;
    }
}