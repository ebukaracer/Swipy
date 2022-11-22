using System;
using System.Collections.Generic;
using Racer.Utilities;
using UnityEngine;
using static Racer.Utilities.SingletonPattern;

[Serializable]
internal struct Pool
{
    public GameObject objPrefab;

    public int amount;
}

internal class ObjectPool : Singleton<ObjectPool>
{
    [SerializeField] private Pool pool;

    private Vector3 _initialPos;

    private readonly Queue<GameObject> _container = new Queue<GameObject>();

    
    protected override void Awake()
    {
        base.Awake();

        Initialize();
    }

    public void Initialize()
    {
        var count = pool.amount;

        var prefab = pool.objPrefab;

        _initialPos = prefab.transform.position;

        prefab.ToggleActive(false);

        for (int i = 0; i < count; i++)
        {
            var clone = Instantiate(prefab, transform);

            _container.Enqueue(clone);
        }
    }

    public GameObject GetObject()
    {
        if (_container.Count > 0)
        {
            var cache = _container.Dequeue();

            cache.ToggleActive(true);

            return cache;
        }

        return null;
    }

    public void AddObject(GameObject obj)
    {
        obj.ToggleActive(false);

        obj.transform.position = _initialPos;

        _container.Enqueue(obj);
    }

}