using System;
using System.Collections.Generic;
using UnityEngine;
using static Racer.Utilities.SingletonPattern;

[Serializable]
struct Pool
{
    public GameObject objPrefab;

    public int amount;
}

public class ObjectPool : Singleton<ObjectPool>
{
    [SerializeField]
    Pool pool;

    Vector3 initialPos;

    readonly Queue<GameObject> container = new Queue<GameObject>();

    public void Initialize()
    {
        var count = pool.amount;

        var prefab = pool.objPrefab;

        initialPos = prefab.transform.position;

        prefab.SetActive(false);

        for (int i = 0; i < count; i++)
        {
            var clone = Instantiate(prefab, transform);

            container.Enqueue(clone);
        }
    }

    public GameObject GetObject()
    {
        if (container.Count > 0)
        {
            var cache = container.Dequeue();

            cache.SetActive(true);

            return cache;
        }

        return null;
    }

    public void AddObject(GameObject obj)
    {
        obj.SetActive(false);

        obj.transform.position = initialPos;

        container.Enqueue(obj);
    }

    protected override void Awake()
    {
        base.Awake();

        Initialize();
    }
}