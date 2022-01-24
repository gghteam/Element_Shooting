using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Element : MonoBehaviour
{    
    private Enemy enemy;
    protected IHittable hittable;
    [field : SerializeField]
    public Conditions Condition { get; private set; }
    private void Awake() {
        enemy = GetComponent<Enemy>();
        hittable = enemy.GetComponent<IHittable>();
    }
    public abstract void BulletSkill(Conditions conditions,GameObject dealer);
    public virtual void Bullet()
    {
        //nothing
    }    
}
