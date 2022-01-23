using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StoneElement : Element
{
    [field: SerializeField]
    private UnityEvent OnFire{get;set;}
    [field: SerializeField]
    private UnityEvent OnWater{get;set;}
    public override void BulletSkill(Conditions conditions,GameObject dealer)
    {
        switch(conditions)
        {
            case Conditions.Fire:
                hittable?.GetHit(GameManager.Instance.PlayerInfo.atk,dealer);
                OnFire?.Invoke();
                break;
            case Conditions.Water:
                hittable?.GetHit(GameManager.Instance.PlayerInfo.atk,dealer);
                OnWater?.Invoke();
                break;
            default:
                hittable?.GetHit(0,dealer);
                break;
        }
    }

    public override void Bullet()
    {
        
    }
}
