using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaterElement : Element
{ 
    [field: SerializeField]
    private UnityEvent OnWind{get;set;}
    [field:SerializeField]
    private UnityEvent OnStone{get;set;}
    public override void BulletSkill(Conditions conditions,GameObject dealer)
    {
        switch(conditions)
        {
            case Conditions.Fire:
                hittable?.GetHit(GameManager.Instance.PlayerATK * 2,dealer);
                break;
            case Conditions.Water:
                hittable?.GetHit(-GameManager.Instance.PlayerATK, dealer);
                break;
            case Conditions.Wind:
                hittable?.GetHit(GameManager.Instance.PlayerATK, dealer);
                OnWind?.Invoke();
                break;
            case Conditions.Stone:
                hittable?.GetHit(GameManager.Instance.PlayerATK, dealer);
                OnStone?.Invoke();
                break;
            default:
                hittable?.GetHit(GameManager.Instance.PlayerATK, dealer);
                break;
        }
    }

    public override void Bullet()
    {
        
    }
}
