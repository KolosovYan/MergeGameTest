using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    public T prefab { get; }
    public bool autoExpand { get; set; }
    public Transform container { get; }

    private List<T> pool;

    public ObjectPool(T prefab, int count)
    {
        this.prefab = prefab;
        this.container = null;

        this.CreatePool(count);
    }

    public ObjectPool(T prefab, int count, Transform container)
    {
        this.prefab = prefab;
        this.container = container;

        this.CreatePool(count);
    }

    private void CreatePool(int count)
    {
        this.pool = new List<T>();

        for (int i = 0; i < count; i++)
        {
            this.CreateObject();
        }
    }

    private T CreateObject(bool IsActiveByDefault = false)
    {
        var createdObject = UnityEngine.Object.Instantiate(this.prefab, this.container);
        createdObject.gameObject.SetActive(IsActiveByDefault);
        this.pool.Add(createdObject);
        return createdObject;
    }

    public bool HasFreeElement(out T element)
    {
        foreach (var mono in pool)
        {
            if (!mono.gameObject.activeInHierarchy)
            {
                element = mono;
                mono.gameObject.SetActive(true);
                return true;
            }
        }

        element = null;
        return false;
    }

    public T GetFreeElement()
    {
        if (this.HasFreeElement(out var element))
            return element;
        if (this.autoExpand)
            return this.CreateObject(true);
        throw new Exception($"There is not GetFreeElement elements in pool of type{typeof(T)}");
    }
}
