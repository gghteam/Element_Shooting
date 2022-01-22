using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;
    private List<GameObject> pooledObjects;
    [SerializeField]
    private GameObject objectToPool;
    [SerializeField]
    private int amountToPool;
    private int amountCount = 0;

    private void Awake() {
        SharedInstance = this;
    }
    private void Start() {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for(int i=0;i<amountToPool;i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }
    public GameObject GetPooledObject()
    {
        if(amountCount>=amountToPool)amountCount = 0;
        if(!pooledObjects[amountCount].activeInHierarchy)
        {
            return pooledObjects[amountCount];
        }
        amountCount++;
        return null;    
    }
}
