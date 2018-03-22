using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsPool : CreateSingletonGameObject<ObjectsPool> 
{
    private List<ObjectPool> Pool = new List<ObjectPool>();

    public GameObject GetObject(GameObject targetObject)
    {
        foreach (var objPool in Pool)
        {
            if (objPool.BaseObject != targetObject)
                continue;

            foreach (var obj in objPool.PooledObjects)
            {
                if(!obj.activeSelf)
                {
                    obj.SetActive(true);
                    return obj;
                }
            }

            GameObject newObj = Instantiate(targetObject);
            objPool.PooledObjects.Add(newObj);
            return newObj;

        }

        ObjectPool newPool = new ObjectPool(targetObject);
        newPool.PooledObjects.Add(Instantiate(targetObject));
        Pool.Add(newPool);
        return newPool.PooledObjects[0];
    }

}

public class ObjectPool
{
    public GameObject BaseObject;
    public List<GameObject> PooledObjects = new List<GameObject>();

    public ObjectPool(GameObject baseObject)
    {
        BaseObject = baseObject;
    }
}
