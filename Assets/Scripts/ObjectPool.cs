using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPool : Singleton<ObjectPool>
{
    public List<PooledObject> objectPool = new List<PooledObject>();

    private void Awake()
    {
        for (int i = 0; i < objectPool.Count; i++)
        {
            objectPool[i].Initialize(this.transform);
        }
    }

    public bool PushToPool(string itemName, GameObject item, Transform parent = null)
    {
        PooledObject pool = GetPoolItem(itemName);
        if (pool == null)
        {
            return false;
        }
        pool.PushToPool(item, parent == null ? this.transform : parent);
        return true;
    }

    public GameObject PopFromPool(string itemName, Transform parent = null)
    {
        PooledObject pool = GetPoolItem(itemName);
        if (pool == null)
        {
            return null;
        }
        return pool.PopFromPool(parent);
    }

    PooledObject GetPoolItem(string itemName)
    {
        for (int i = 0; i < objectPool.Count; i++)
        {
            if (objectPool[i].poolItemName.Equals(itemName))
                return objectPool[i];
        }

        Debug.LogWarning("풀 리스트가 존재하지 않습니다.");
        return null;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
