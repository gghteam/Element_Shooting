using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Bullet : PoolableMono
{
    private void Update() {
        CheckLimit();    
    }
    private void CheckLimit()
    {
        if(transform.position.x > GameManager.Instance.maxPosition.x||transform.position.x < GameManager.Instance.minPosition.x)
        {
            Despaw();
        }
        if(transform.position.y > GameManager.Instance.maxPosition.y||transform.position.y < GameManager.Instance.minPosition.y)
        {
            Despaw();
        }
    }
    protected void Despaw()
    {
        PoolManager.Instance.Despawn(gameObject);
    }
}
