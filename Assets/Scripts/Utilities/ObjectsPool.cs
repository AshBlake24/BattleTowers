using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectsPool<T> where T : Component
{
    private int _capacity;
    private GameObject _prefab;
    private Transform _poolContainer;
    private List<T> _pool = new List<T>();

    public ObjectsPool(GameObject prefab, int capacity)
    {
        if (prefab == null)
            Debug.LogError("Prefab is null!");

        if (prefab.GetComponent<T>() == null)
            Debug.LogError($"Prefab has no: {typeof(T)} component!");

        _prefab = prefab;
        _capacity = capacity;

        CreatePool();
    }

    public T GetInstanceFromPool()
    {
        T item = _pool.FirstOrDefault(item => item.gameObject.activeSelf == false);

        if (item != null)
            return item;
        else
            return CreateNewInstance();
    }

    private T CreateNewInstance()
    {
        GameObject item = Object.Instantiate(_prefab, _poolContainer);

        T component = item.GetComponent<T>();

        _pool.Add(component);

        item.SetActive(false);

        return component;
    }

    private void CreatePool()
    {
        _poolContainer = new GameObject($"Pool - {_prefab.name}").transform;

        Object.DontDestroyOnLoad(_poolContainer);

        for (int i = 0; i < _capacity; i++)
        {
            CreateNewInstance();
        }
    }
}