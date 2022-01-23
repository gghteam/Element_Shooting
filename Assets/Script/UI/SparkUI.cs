using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkUI : PoolableMono
{
    void Start()
    {
        Invoke("DeSpark", 2f);
    }

    void DeSpark()
    {
        PoolManager.Instance.Despawn(gameObject);
    }
    public override void Reset()
    {
        //nothing
    }
}
