using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaterElement : Element
{ 
    [field: SerializeField]
    private UnityEvent OnWind{get;set;}    
    public override void BulletSkill(Conditions conditions,GameObject dealer)
    {
        switch(conditions)
        {
            case Conditions.Fire:
                hittable?.GetHit(GameManager.Instance.PlayerInfo.atk*2,dealer);
                break;
            case Conditions.Water:
                hittable?.GetHit(-GameManager.Instance.PlayerInfo.atk,dealer);
                break;
            case Conditions.Wind:
                hittable?.GetHit(GameManager.Instance.PlayerInfo.atk,dealer);
                OnWind?.Invoke();
                break;
            default:
                hittable?.GetHit(GameManager.Instance.PlayerInfo.atk,dealer);
                break;
        }
    }

    public override void Bullet()
    {
        
    }
}
