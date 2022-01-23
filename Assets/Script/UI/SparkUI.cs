using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkUI : MonoBehaviour
{
    void Start()
    {
        Invoke("DeSpark", 2f);
    }

    void DeSpark()
    {
        PoolManager.Instance.Despawn(gameObject);
    }
}
