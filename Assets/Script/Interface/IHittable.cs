using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHittable
{
    public bool IsEnemy { get; }
    Vector3 _hitPoint { get; }
    public void GetHit(int damage, GameObject damageDealer);
}
