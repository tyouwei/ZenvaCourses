using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject[] objectPrefabs;
    public int poolSize = 20;
    private List<GameObject> pooledObjects = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        CreatePool();
    }

    void CreatePool()
    {
        for(int i=0; i < poolSize; i++)
        {
            GameObject objectToSpawn = Instantiate(objectPrefabs[i % objectPrefabs.Length]);
            objectToSpawn.SetActive(false);
            pooledObjects.Add(objectToSpawn);
        }
    }
    public GameObject GetPooledObject()
    {
        GameObject objectToSend = pooledObjects.Find(x => !x.activeInHierarchy);
        if (!objectToSend)
            Debug.Log("No more available objects in the pool! Increase Its size.");
        else
            objectToSend.SetActive(true);
        return objectToSend;
    }
}
